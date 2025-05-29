﻿using miso_greenshop_api.Domain.Validations;
using System.ComponentModel.DataAnnotations;

namespace miso_greenshop_api.Dtos.Users
{
    public class RegisterDto
    {
        [Required]
        [UsernameIsValid]
        public string? Name { get; set; }
        [Required]
        [EmailIsValid]
        public string? Email { get; set; }
        [Required]
        [PasswordIsValid]
        public string? Password { get; set; }
        [Required]
        public bool IsSubscribed { get; set; } = false;
    }
}
