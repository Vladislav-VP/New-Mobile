﻿using System.ComponentModel.DataAnnotations;

namespace ViewModels.UI.User
{
    public class RequestChangeNameUserView
    {
        [Required]
        public string Name { get; set; }
    }
}
