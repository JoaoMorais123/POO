namespace AutoParts_Backend.Models.Request;

public class CarAccessoryRequest
{
    #region public
    
    public int stockQuantity { get; set; }

    public string accessoryCategory { get; set; }
    
    #endregion
}