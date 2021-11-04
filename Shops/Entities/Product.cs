namespace Shops.Entities
{
    public class Product
    {
        private static uint _id = 0;
        public Product(string name)
        {
            _id++;
            Name = name;
            Id = _id;
        }

        public uint Id { get; }
        public string Name { get; }
    }
}