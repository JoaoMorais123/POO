namespace OrderLib.Models
{
    /// <summary>
    /// Represents a car accessory with additional categorization.
    /// </summary>
    public class CarAccessory : Product
    {
        private string _accessoryCategory;
        private int _stockQuantity;
        private int _IDProduct;

        /// <summary>
        /// Gets the accessory category.
        /// </summary>
        public string AccessoryCategory => _accessoryCategory;
        public int StockQuantity => _stockQuantity;
        
        public int IDProduct => _IDProduct;

        /// <summary>
        /// Initializes a new instance of the CarAccessory class with specified details.
        /// </summary>
        /// <param name="IDProduct">The unique identifier for the product.</param>
        /// <param name="stockQuantity">The stock quantity available.</param>
        /// <param name="accessoryCategory">The category of the accessory.</param>
        public CarAccessory(int IDProduct, int stockQuantity, string accessoryCategory)
            : base(IDProduct, "DefaultName", stockQuantity, 0.0, 0, 0)
        {
            this._IDProduct = IDProduct;
            this._stockQuantity = stockQuantity;
            this._accessoryCategory = accessoryCategory;
        }

        public override string DisplayInfo()
        {
            return $"ID:{IDProduct}, Quantity:{_stockQuantity}, Category:{_accessoryCategory}";
        }
    }
}