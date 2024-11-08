﻿using System.ComponentModel.DataAnnotations;

namespace SailTracker.WebApi.Models
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; } // Parola zorunlu hale getirildi
    }
}