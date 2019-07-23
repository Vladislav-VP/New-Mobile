using System;
using System.Collections.Generic;
using System.Text;
using TestProject.Entities;
using Acr.UserDialogs;
using TestProject.Services.Resources;
using System.Threading.Tasks;
using TestProject.Configurations;
using System.Text.RegularExpressions;
using TestProject.Services.Repositories;

namespace TestProject.Services.Helpers
{
    public static class UserValidationHelper
    {
        public static async Task<bool> UserInfoIsValid(User user)
        {
            if (string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Password))
            {
                UserDialogsHelper.ToastErrorMessage(Strings.UserDataNullOrEmptyMessage);
                return false;
            }

            Regex regex = new Regex(Constants.InvalidPasswordCharacterPattern);
            if (regex.IsMatch(user.Password))
            {
                UserDialogsHelper.ToastErrorMessage(Strings.InvalidPasswordFormatMessage);
                return false;
            }

            if (await new UserRepository().UserExists(user.Name))
            {
                UserDialogsHelper.ToastErrorMessage(Strings.UserAlreadyExistsMessage);
                return false;
            }

            if (user.Password.Length < Constants.MinPasswordLength)
            {
                UserDialogsHelper.ToastErrorMessage(Strings.TooShortPasswordMessage);
                return false;
            }

            return true;
        }

        public static async Task<bool> LoginDataIsValid(User enteredUser)
        {
            User userFromDatabase = await new UserRepository().FindUser(enteredUser.Name);

            if (!enteredUser.Equals(userFromDatabase))
            {
                UserDialogsHelper.ToastErrorMessage(Strings.LoginErrorMessage);
                return false;
            }

            return true;
        }
    }
}
