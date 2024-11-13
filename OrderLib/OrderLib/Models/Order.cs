using System;
using System.Collections.Generic;
using System.Linq;

namespace OrderLib.Models
{
    /// <summary>
    /// Represents an order with shipping information and products.
    /// </summary>
    public class Order : Entity
    {
        private DateTime _orderDate;
        private string _shippingAddress;
        private List<OrderLine> _orderLines;
        private OrderStatus _status;

        public DateTime OrderDate => _orderDate;
        public string ShippingAddress => _shippingAddress;
        public OrderStatus Status => _status;

        public Order(int id, string shippingAddress)
            : base(id)
        {
            _orderDate = DateTime.Now;
            _shippingAddress = shippingAddress;
            _orderLines = new List<OrderLine>();
            _status = OrderStatus.Pending;
        }

        /// <summary>
        /// Adds a product to the order.
        /// </summary>
        /// <param name="product">Product to add.</param>
        /// <param name="quantity">Quantity of the product.</param>
        public void AddProduct(Product product, int quantity)
        {
            if (!product.IsStockAvailable(quantity))
                throw new InvalidOperationException("Insufficient stock for product.");

            var orderLine = new OrderLine(product, quantity);
            _orderLines.Add(orderLine);
        }

        /// <summary>
        /// Calculates the total weight of all products in the order.
        /// </summary>
        /// <returns>Total weight.</returns>
        public double CalculateTotalWeight() => _orderLines.Sum(line => line.TotalWeight);

        public override string DisplayInfo()
        {
            throw new NotImplementedException();
        }
    }

}