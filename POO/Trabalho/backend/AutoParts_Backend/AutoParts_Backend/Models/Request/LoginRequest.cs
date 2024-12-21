using AuthLib.Models;
using Microsoft.AspNetCore.Identity.Data;

namespace AutoParts_Backend.Models.Request;

public class LoginRequest
{
    #region public
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    #endregion

    public LoginRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }
    
}