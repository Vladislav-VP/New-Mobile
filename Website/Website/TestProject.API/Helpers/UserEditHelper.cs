using Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace TestProject.API.Helpers
{
    public class UserEditHelper
    {
        private readonly UsersService _usersService;

        public UserEditHelper(UsersService usersService)
        {
            _usersService = usersService;
        }

        public void ReplaceProfilePhoto(IHostingEnvironment hostingEnvironment, User user)
        {
            if (user.ImageBytes != null)
            {
                UploadProfilePhoto(hostingEnvironment, user);
            }
            User userToModify = _usersService.FindById(user.Id);
            userToModify.ImageUrl = user.ImageUrl;
            _usersService.Update(userToModify);
        }

        private void UploadProfilePhoto(IHostingEnvironment hostingEnvironment, User user)
        {
            using (var imageStream = new MemoryStream(user.ImageBytes))
            {
                string imageUrl = $"{hostingEnvironment.WebRootPath}\\ProfileImages\\{Guid.NewGuid().ToString()}.png";
                using (var imageFileStream = new FileStream(imageUrl, FileMode.Create))
                {
                    imageStream.CopyTo(imageFileStream);
                }
                user.ImageUrl = imageUrl;
            }            
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
    }
}
