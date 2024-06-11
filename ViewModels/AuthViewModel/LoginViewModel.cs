﻿using System.ComponentModel.DataAnnotations;

namespace BlogApp.ViewModels.AuthViewModel;

public class LoginViewModel
{
    [Required(ErrorMessage = "Username is required!")]
    public required string Username { get; set; }

    [Required(ErrorMessage = "Password is required!")]
    [DataType(DataType.Password)]
    public required string Password { get; set; }
}
