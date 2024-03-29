﻿using System.ComponentModel.DataAnnotations;

namespace Contracts.Auth
{
    public class AuthModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
