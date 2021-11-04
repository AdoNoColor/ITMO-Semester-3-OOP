namespace Shops.Entities
{
    public class ProductExtra : Product
    {
        public ProductExtra(string name, uint id, int quantity, int price)
            : base(name)
        {
            Id = id;
            Quantity = quantity;
            Price = price;
        }

        public int Price { get; set; }
        public new uint Id { get; }
        public int Quantity { get; set;  }
    }
}