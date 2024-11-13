namespace AuthLib.Models
{
    /// <summary>
    /// Represents an admin with full access privileges.
    /// </summary>
    public class Admin : Person
    {
        /// <summary>
        /// Initializes a new instance of the Admin class with specified details.
        /// </summary>
        /// <param name="name">Name of the admin.</param>
        /// <param name="password">Password for authentication.</param>
        /// <param name="email">Email of the admin.</param>
        public Admin(string name, string password, string email) 
            : base(name, password, email, Authorization.AuthorizationLevel.Admin)
        {
        }
    }
}