namespace OrderLib.Models
{
    /// <summary>
    /// Represents a shipping carrier with shipping cost calculation.
    /// </summary>
    public class Carrier
    {
        private string _name;
        private double _shippingCostPerKg;
        private int _deliveryTimeInDays;

        public string Name => _name;
        public double ShippingCostPerKg => _shippingCostPerKg;
        public int EstimatedDeliveryTime => _deliveryTimeInDays;

        public Carrier(string name, double shippingCostPerKg, int deliveryTimeInDays)
        {
            _name = name;
            _shippingCostPerKg = shippingCostPerKg;
            _deliveryTimeInDays = deliveryTimeInDays;
        }

        /// <summary>
        /// Calculates the total shipping cost based on weight.
        /// </summary>
        /// <param name="totalWeight">Total weight of the order.</param>
        /// <returns>Total shipping cost.</returns>
        public double CalculateShippingCost(double totalWeight) => (double)totalWeight * _shippingCostPerKg;

        /// <summary>
        /// Validates if the carrier can deliver within the expected timeframe.
        /// </summary>
        /// <param name="expectedDays">Expected delivery days.</param>
        /// <returns>True if the carrier can deliver within the expected timeframe; otherwise, false.</returns>
        public bool ValidateDeliveryTime(int expectedDays) => _deliveryTimeInDays <= expectedDays;
    }
}