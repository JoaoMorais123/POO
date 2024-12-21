namespace AuthLib.Models
{
    public interface IAuthenticationService
    { 
        Person Login(string email, string password);
        bool Logout();
        Person Register(string email, string name, string password);
    }
}