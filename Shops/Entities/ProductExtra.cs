namespace Shops.Entities
{
    public class ProductExtra : Product
    {
        public ProductExtra(string name, uint id, int quantity, decimal price)
            : base(name)
        {
            Id = id;
            Quantity = quantity;
            Price = price;
        }

        public ProductExtra(string name, uint id, int quantity)
            : base(name)
        {
            Id = id;
            Quantity = quantity;
        }

        public decimal Price { get; set; }
        public new uint Id { get; }
        public int Quantity { get; set;  }
    }
}