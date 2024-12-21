namespace AuthLib.Models
{
    /// <summary>
    /// Service for handling authentication and authorization.
    /// </summary>
    public class AuthenticationService : IAuthenticationService
    {
        public Person Login(string email, string password)
        {
            throw new System.NotImplementedException();
        }

        public bool Logout()
        {
            throw new System.NotImplementedException();
        }

        public Person Register(string email, string name, string password)
        {
            throw new System.NotImplementedException();
        }
    }
}