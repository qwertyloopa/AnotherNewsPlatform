using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AnotherNewsPlatform.MVC.Models.User
{
    public class ChangeUserModel(long id)
    {
        public long Id = id;
        public string Username { get; set; }

        [EmailAddress]
        [Remote(action: "VerifyEmail", controller: "User", ErrorMessage = $"This email is already in use")]
        public string Email { get; set; }

        [PasswordPropertyText]
        public string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
        public string ConfirmPassword { get; set; }
    }
}
