namespace OrderLib.Models
{
    /// <summary>
    /// Represents the status of an order.
    /// </summary>
    public enum OrderStatus
    {
        Pending,
        Shipped,
        Delivered,
        Cancelled
    }
}