﻿using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI
{
    public class SettingsUserView
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Profile photo")]
        public string ImageUrl { get; set; }
    }
}
