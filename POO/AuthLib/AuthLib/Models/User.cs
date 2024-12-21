namespace AuthLib.Models
{
        /// <summary>
        /// Represents a standard user with limited access privileges.
        /// </summary>
        public class User : Person
        {
            /// <summary>
            /// Initializes a new instance of the User class with specified details.
            /// </summary>
            /// <param name="name">Name of the user.</param>
            /// <param name="password">Password for authentication.</param>
            /// <param name="email">Email of the user.</param>
            public User(string name, string password, string email)
                : base(name, password, email, Authorization.AuthorizationLevel.User)
            {
            }
        }
}