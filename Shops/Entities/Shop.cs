using System.Collections.Generic;
using System.Linq;
using Shops.Tools;

namespace Shops.Entities
{
    public class Shop
    {
        private static uint _id = 0;
        private readonly List<ProductExtra> _stock = new List<ProductExtra>();
        public Shop(string address, string name)
        {
            _id++;
            Address = address;
            Name = name;
            Id = _id;
        }

        internal uint Id { get; }
        internal string Address { get; }
        private string Name { get; }

        public void AddInStock(ProductExtra product)
        {
            foreach (ProductExtra item in _stock.Where(item => product.Id == item.Id))
            {
                item.Price = product.Price;
                item.Quantity += product.Quantity;
            }

            _stock.Add(product);
        }

        public bool BoolFindProduct(uint productId)
        {
            return _stock.Any(product => product.Id == productId);
        }

        public bool BoolFindProduct(Product product)
        {
            return _stock.Any(item => item.Id == product.Id);
        }

        public ProductExtra FindProduct(Product product)
        {
            return _stock.FirstOrDefault(item => item.Id == product.Id);
        }

        public int ProductPrice(uint productId)
        {
            foreach (ProductExtra product in _stock.Where(product => productId == product.Id))
            {
                return product.Price;
            }

            throw new ShopException("No product like this!");
        }

        public void ChangeProductPrice(uint productId, int newPrice)
        {
            foreach (ProductExtra t in _stock.Where(t => t.Id == productId))
            {
                t.Price = newPrice;
                return;
            }

            throw new ShopException("No shop with the ID like that!");
        }

        public ProductExtra GetItemFromStock(int i)
        {
            if (_stock[i] == null)
            {
                throw new ShopException("No item like this");
            }

            return _stock[i];
        }

        public void ChangeItemQuantity(int i, int quantity)
        {
            if (_stock[i] == null)
            {
                throw new ShopException("No item like this");
            }

            if (_stock[i].Quantity - quantity < 0)
            {
                throw new ShopException("Not enough products in this particular shop");
            }

            _stock[i].Quantity -= quantity;
        }

        public int StockQuantity()
        {
            return _stock.Count;
        }

        internal ProductExtra FindProduct(uint productId)
        {
            return _stock.FirstOrDefault(item => item.Id == productId);
        }
    }
}