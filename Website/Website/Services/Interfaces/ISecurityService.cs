﻿using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

using ViewModels.Api;
using ViewModels.Api.User;

namespace Services.Interfaces
{
    public interface ISecurityService
    {
        TokenPair GenerateTokens(string email, IdentityUser user);

        Task<ResponseRefreshAccessTokenUserApiView> RefreshToken(RequestRefreshAccessTokenUserApiView tokens);
    }
}
