namespace OrderLib.Models
{
    /// <summary>
    /// Represents a line item within an order.
    /// </summary>
    public class OrderLine
    {
        private Product _product;
        private int _quantity;
        private decimal _unitPrice;

        public int Quantity => _quantity;
        public decimal UnitPrice => _unitPrice;
        public double TotalWeight => _product.Weight * _quantity;
        public decimal TotalPrice => _unitPrice * _quantity;

        public OrderLine(Product product, int quantity)
        {
            _product = product;
            _quantity = quantity;
            _unitPrice = product.UnitPrice;
        }
    }
}