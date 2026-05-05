using System;
using System.ComponentModel.DataAnnotations;

namespace AnotherNewsPlatform.MVC.Models.User;

public record LoginModel
{
    [Required]
    public string Email { get; set; }
    
    [Required]
    public string Password { get; set; }
}
