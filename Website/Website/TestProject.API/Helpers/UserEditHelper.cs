using Microsoft.AspNetCore.Hosting;
using Services;
using System;
using System.IO;

using Entities;

namespace TestProject.API.Helpers
{
    public class UserEditHelper
    {
        private readonly UsersService _usersService;
        
        public UserEditHelper(UsersService usersService)
        {
            _usersService = usersService;
        }

        public void ReplaceProfilePhoto(IWebHostEnvironment hostEnvironment, User user)
        {
            if (user.ImageBytes != null)
            {
                UploadProfilePhoto(hostEnvironment, user);
            }
            User userToModify = _usersService.FindById(user.Id);
            if (string.IsNullOrEmpty(user.ImageUrl) && File.Exists(userToModify.ImageUrl))
            {
                File.Delete(userToModify.ImageUrl);
            }
            userToModify.ImageUrl = user.ImageUrl;
            _usersService.Update(userToModify);
        }

        public User GetUserWithPhoto(int id)
        {
            User user = _usersService.FindById(id);
            using (var imageFileStream = new FileStream(user.ImageUrl, FileMode.Open, FileAccess.ReadWrite))
            {
                using (var imageMemoryStream = new MemoryStream())
                {
                    imageFileStream.CopyTo(imageMemoryStream);
                    user.ImageBytes = imageMemoryStream.ToArray();
                }
            }
            return user;
        }

        private void UploadProfilePhoto(IWebHostEnvironment hostEnvironment, User user)
        {
            using (var imageStream = new MemoryStream(user.ImageBytes))
            {
                string imageUrl = $"{hostEnvironment.WebRootPath}\\ProfileImages\\{Guid.NewGuid().ToString()}.png";
                using (var imageFileStream = new FileStream(imageUrl, FileMode.Create))
                {
                    imageStream.CopyTo(imageFileStream);
                }
                user.ImageUrl = imageUrl;
            }            
        }
    }
}
