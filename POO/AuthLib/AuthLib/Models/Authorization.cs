namespace AuthLib.Models
{
    public class Authorization
    {
        /// <summary>
        /// Specifies the authorization level for a person.
        /// </summary>
        public enum AuthorizationLevel
        {
            Admin,
            User
        }
    }
}