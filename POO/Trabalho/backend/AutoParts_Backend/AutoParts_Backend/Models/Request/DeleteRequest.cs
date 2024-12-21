namespace AutoParts_Backend.Models.Request;

public class DeleteRequest
{
    #region public
    public string Email { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    #endregion

    public DeleteRequest(string email, string name)
    {
        Email = email;
        Name = name;
    }
}