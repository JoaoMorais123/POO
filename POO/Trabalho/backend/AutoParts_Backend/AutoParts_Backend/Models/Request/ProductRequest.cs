namespace AutoParts_Backend.Models.Request;

public class ProductRequest
{
    #region MyRegion

    public string name { get; set; }
    
    public int stockQuantity { get; set; }
    
    public double weight { get; set; }
    
    public double unitPrice { get; set; }
    
    public int IDCarAccessory { get; set; }

    #endregion
}