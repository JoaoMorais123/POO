namespace AutoParts_Backend.Models.Request;

public class CarrierRequest
{
    #region public

    public string name { get; set; }
    
    public string contactInfo { get; set; }
    
    public double shippingCost { get; set; }
    
    public double calculateWeight { get; set; }
    
    public int estimatedDeliveryTime { get; set; }
    
    public string validateTransport { get; set; }
    
    #endregion
}