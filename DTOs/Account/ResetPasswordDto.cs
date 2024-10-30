﻿using System.ComponentModel.DataAnnotations;
using WebApplication71.Services;

namespace WebApplication71.DTOs.Account
{
    public class ResetPasswordDto
    {
        public string Email { get; set; }
        public string Token { get; set; }



        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(10, ErrorMessage = "Hasło musi mieć co najmniej 10 znaków")]
        [DataType(DataType.Password)]
        [PasswordRequirements]
        public string Password { get; set; }

        [Required(ErrorMessage = "To pole jest wymagane")]
        [MinLength(10, ErrorMessage = "Hasło musi mieć co najmniej 10 znaków")]
        [DataType(DataType.Password)]
        [PasswordRequirements]
        public string ConfirmPassword { get; set; }
    }
}
