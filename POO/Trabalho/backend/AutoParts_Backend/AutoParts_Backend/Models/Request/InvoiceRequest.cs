namespace AutoParts_Backend.Models.Request;

public class InvoiceRequest
{
    #region public
    
    public DateTime InvoiceDate { get; set; }
    
    public double TotalAmount { get; set; }
    
    public double ProcessPayment { get; set; }
    
    public int IDOrderLine { get; set; }
    
    #endregion
}