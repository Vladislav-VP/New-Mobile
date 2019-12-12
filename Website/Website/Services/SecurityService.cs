using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

using Entities;
using DataAccess.Repositories.Interfaces;
using Services.Interfaces;
using ViewModels.Api;
using ViewModels.Api.User;

namespace Services
{
    public class SecurityService : ISecurityService
    {
        private readonly IConfiguration _configuration;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly TokenValidationParameters _tokenValidationParameters;
        private readonly UserManager<User> _userManager;

        public SecurityService(IConfiguration configuration, IRefreshTokenRepository refreshTokenRepository,
            TokenValidationParameters tokenValidationParameters, UserManager<User> userManager)
        {
            _configuration = configuration;
            _refreshTokenRepository = refreshTokenRepository;
            _tokenValidationParameters = tokenValidationParameters;
            _userManager = userManager;
        }
        
        public TokenPair GenerateTokens(string email, IdentityUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            //DateTime expires = DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["JwtExpirationTime"]));
            // TODO : Remove hardcode, add normal expiration date
            DateTime expires = DateTime.Now.AddMinutes(15);
            var token = new JwtSecurityToken(
                _configuration["JwtIssuer"],
                _configuration["JwtIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );
            var refreshToken = new RefreshToken
            {
                JwtId = token.Id,
                UserId = user.Id,
                CreationDate = DateTime.Now,
                ExpiryDate = DateTime.Now.AddMonths(1)
            };
            _refreshTokenRepository.Insert(refreshToken);
            var handler = new JwtSecurityTokenHandler();
            string accessToken = handler.WriteToken(token);
            var tokenPair = new TokenPair
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken.Token
            };
            return tokenPair;
        }

        public async Task<ResponseRefreshAccessTokenUserApiView> RefreshToken(RequestRefreshAccessTokenUserApiView tokens)
        {
            var response = new ResponseRefreshAccessTokenUserApiView();
            ClaimsPrincipal principal = GetPrincipalFromToken(tokens.AccessToken);
            response = ValidateTokens(tokens, response);
            if (!response.IsSuccess)
            {
                return response;
            }
            string id = principal.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(id);
            TokenPair tokenPair = GenerateTokens(user.UserName, user);
            response.AccessToken = tokenPair.AccessToken;
            response.RefreshToken = tokenPair.RefreshToken;
            return response;
        }

        private ResponseRefreshAccessTokenUserApiView ValidateTokens(RequestRefreshAccessTokenUserApiView tokens,
            ResponseRefreshAccessTokenUserApiView response)
        {
            ClaimsPrincipal principal = GetPrincipalFromToken(tokens.AccessToken);
            var jti = principal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;
            RefreshToken storedRefreshToken = _refreshTokenRepository.GetToken(tokens.RefreshToken);
            if (storedRefreshToken == null)
            {
                response.Message = "This refresh token does not exist";
                return response;
            }
            if (DateTime.UtcNow > storedRefreshToken.ExpiryDate)
            {
                response.Message = "This refresh token has expired";
                return response;
            }
            if (storedRefreshToken.Invalidated)
            {
                response.Message = "This refresh token has been invalidated";
                return response;
            }
            if (storedRefreshToken.Used)
            {
                response.Message = "This refresh token has been used";
                return response;
            }
            if (storedRefreshToken.JwtId != jti)
            {
                response.Message = "This refresh token does not match this JWT";
                return response;
            }
            response.IsSuccess = true;
            return response;
        }

        private ClaimsPrincipal GetPrincipalFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenValidationParameters = _tokenValidationParameters.Clone();
            tokenValidationParameters.ValidateLifetime = false;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);
            return principal;
        }
    }
}
