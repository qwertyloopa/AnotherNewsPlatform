using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace AnotherNewsPlatform.MVC.Models.User;

public class RegisterModel
{
    [Required]
    public string Username { get; set; }

    [Required]
    [EmailAddress]
    [Remote(action: "VerifyEmail", controller: "User", ErrorMessage = $"This email is already in use")]
    public string Email { get; set; }
    
    [Required]
    [PasswordPropertyText]
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; }
}
