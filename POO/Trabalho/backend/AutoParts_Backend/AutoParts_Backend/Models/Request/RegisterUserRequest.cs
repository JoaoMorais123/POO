namespace AutoParts_Backend.Models.Request;

public class RegisterUserRequest
{
    #region public
    
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    #endregion
    
    #region constructors

    public RegisterUserRequest(string name, string email, string password)
    {
        Name = name;
        Email = email;
        Password = password;
    }
    #endregion
}