using System.Runtime.InteropServices.JavaScript;

namespace AutoParts_Backend.Models.Request;

public class OrderRequest
{
    #region public
    
    public string shippingAddress { get; set; }
    
    public string orderStatus { get; set; }
    
    public double totalAmount { get; set; }
    
    public string DisplayInfo { get; set; }
    
    public DateTime CreateDate { get; set; }
    #endregion
    
}