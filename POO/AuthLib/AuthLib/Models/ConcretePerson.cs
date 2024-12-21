namespace AuthLib.Models
{
    public class ConcretePerson : Person
    {
        public ConcretePerson(string name, string password, string email, Authorization.AuthorizationLevel authLevel)
            : base(name, password, email, authLevel)
        {
        }
    }

}