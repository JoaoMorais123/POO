namespace AutoParts_Backend.Models.Request;

public class OrderLineRequest
{
    #region public

    
    public int Quantity { get; set; }
    
    
    public int IDOrder { get; set; }
    
    public int IDProduct { get; set; }
    
    public int IDCarrier { get; set; }
    
    

    #endregion
}