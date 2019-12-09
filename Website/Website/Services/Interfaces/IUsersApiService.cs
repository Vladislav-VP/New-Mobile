﻿using System.Security.Claims;
using System.Threading.Tasks;

using Entities;
using ViewModels.Api.User;

namespace Services.Interfaces
{
    public interface IUsersApiService : IBaseApiService<User>
    {
        Task<ResponseRegisterUserApiView> Register(RequestRegisterUserApiView userToRegister);

        Task<ResponseLoginUserApiView> Login(RequestLoginUserApiView userRequest, ClaimsPrincipal principal);

        GetProfileImageUserApiView GetUserWithPhoto(ClaimsPrincipal principal);

        Task<ResponseEditProfileImageUserApiView> ReplaceProfilePhoto(RequestEditProfileImageUserApiView user,
            string imageUrl, ClaimsPrincipal principal);

        ResponseEditNameUserApiView EditUserName(RequestEditNameUserApiView user, ClaimsPrincipal principal);

        string GetUserName(ClaimsPrincipal principal);

        ResponseChangePasswordUserApiView ChangePassword(RequestChangePasswordUserApiView user);

        Task Logout();

        DeleteUserApiView Delete(int id);
    }
}
