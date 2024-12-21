using Microsoft.AspNetCore.Identity.Data;

namespace AutoParts_Backend.Models.Request;

public class Register
{
    #region MyRegion
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Name{ get; set; } = string.Empty;
    #endregion

    public RegisterRequest(string email, string password, string name)
    {
        Email = email;
        Password = password;
        N
    }
    
}