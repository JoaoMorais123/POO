namespace AutoParts_Backend.Models.Request;

public class UpdateRequest
{
    #region public
    
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public int IDUser { get; set; }
    #endregion
}