﻿using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI.Home
{
    public class RequestLoginHomeView
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "Username can not be empty")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password can not be empty")]
        public string Password { get; set; }
    }
}
