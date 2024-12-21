namespace AuthLib.Models
{
        /// <summary>
        /// Represents a person with authentication details.
        /// </summary>
        public abstract class Person
        {
            // Private fields for encapsulation
            private string _name;
            private string _password;
            private string _email;
            private bool _verification;

            /// <summary>
            /// Gets the name of the person.
            /// </summary>
            public string Name => _name;

            /// <summary>
            /// Gets the email of the person.
            /// </summary>
            public string Email => _email;

            /// <summary>
            /// Gets the authorization level of the person.
            /// </summary>
            public Authorization.AuthorizationLevel AuthLevel { get; private set; }

            /// <summary>
            /// Initializes a new instance of the Person class with specified details.
            /// </summary>
            /// <param name="name">Name of the person.</param>
            /// <param name="password">Password for authentication.</param>
            /// <param name="email">Email of the person.</param>
            /// <param name="authLevel">Authorization level of the person.</param>
            protected Person(string name, string password, string email, Authorization.AuthorizationLevel authLevel)
            {
                _name = name;
                _password = password;
                _email = email;
                _verification = false;
                AuthLevel = authLevel;
            }

            /// <summary>
            /// Verifies the password for authentication.
            /// </summary>
            /// <param name="password">Password to check.</param>
            /// <returns>True if authentication is successful; otherwise, false.</returns>
            public bool Authenticate(string password) => _password == password;

            /// <summary>
            /// Marks the person as verified.
            /// </summary>
            public void Verify() => _verification = true;
        }
}