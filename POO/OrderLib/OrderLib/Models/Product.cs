namespace OrderLib.Models
{
    /// <summary>
    /// Represents a product with stock and pricing information.
    /// </summary>
    public class Product : Entity
    {
        private string _name;
        private int _stockQuantity;
        private double _weight;
        private decimal _unitPrice;
        private int _idCarAccessory;

        public string Name => _name;
        public double Weight => _weight;
        public decimal UnitPrice => _unitPrice;
        public int IDCarAccessory => _idCarAccessory;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="stockQuantity"></param>
        /// <param name="weight"></param>
        /// <param name="unitPrice"></param>
        /// <param name="IDCarAccessory"></param>
        public Product(int id, string name, int stockQuantity, double weight, decimal unitPrice, int IDCarAccessory)
            : base(id)
        {
            _name = name;
            _stockQuantity = stockQuantity;
            _weight = weight;
            _unitPrice = unitPrice;
            _idCarAccessory = IDCarAccessory;
            
        }
        
        /// <summary>
        /// Checks if the specified quantity is available in stock.
        /// </summary>
        /// <param name="quantity">Quantity to check.</param>
        /// <returns>True if the quantity is available; otherwise, false.</returns>
        
        /// METODO
        public bool IsStockAvailable(int quantity) => _stockQuantity >= quantity;

        /// <summary>
        /// METODO
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public override string DisplayInfo()
        {
            throw new System.NotImplementedException();
        }
    }
    
}