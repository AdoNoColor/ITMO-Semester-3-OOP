using System.Collections.Generic;
using System.Linq;
using Shops.Entities;
using Shops.Tools;

namespace Shops.Services
{
    public class ShopService
    {
        private readonly List<Shop> _shops = new List<Shop>();
        private readonly List<Product> _products = new List<Product>();
        private readonly List<ProductExtra> _deliver = new List<ProductExtra>();
        internal List<ProductExtra> Package { get; } = new List<ProductExtra>();

        public Shop RegisterShop(string address, string name)
        {
            var shop = new Shop(address, name);
            if (_shops.Any(i => shop.Id == i.Id && shop.Address == address))
            {
                throw new ShopException("Some shop got the same ID or address!");
            }

            _shops.Add(shop);

            return shop;
        }

        public Product RegisterProduct(string name)
        {
            var product = new Product(name);
            if (_products.Any(item => product.Id == item.Id))
            {
                throw new ShopException("Some product got the same ID!");
            }

            _products.Add(product);
            return product;
        }

        public void AddProductToDeliver(Product product, decimal price, int quantity)
        {
            Product found = _products.Find(item => item.Id == product.Id);
            if (found == null)
                throw new ShopException("No product like that!");
            foreach (ProductExtra item in _deliver.Where(item => item.Id == product.Id))
            {
                item.Price = price;
                item.Quantity = quantity;
                return;
            }

            var productExtra = new ProductExtra(found.Name, found.Id, quantity, price);
            _deliver.Add(productExtra);
        }

        public void DeliverTo(Shop shop)
        {
            foreach (ProductExtra item in _deliver)
            {
                shop.AddInStock(item);
            }

            _deliver.Clear();
        }

        public void ChangePrice(Shop shop, Product product, decimal newPrice)
        {
            foreach (Shop item in _shops)
            {
                if (item.Id == shop.Id)
                {
                    item.ChangeProductPrice(product.Id, newPrice);
                }

                return;
            }

            throw new ShopException("No shop with the ID like that!");
        }

        public void AddProductToSearch(Product product, int quantity)
        {
            if (_products.All(item => item.Id != product.Id))
                throw new ShopException("No product with the ID like that!");
            var foundProduct = new ProductExtra(product.Name, product.Id, quantity);
            Package.Add(foundProduct);
        }

        public decimal SearchProfitableDeal()
        {
            if (Package.Count == 0)
            {
                throw new ShopException("No products to search!");
            }

            decimal totalAmount = _shops.Aggregate(decimal.MaxValue, (current, shop) =>
                shop.FindProfitableDeal(this, current));

            if (totalAmount == decimal.MaxValue)
                throw new ShopException("Something ain't right!");

            return totalAmount;
        }

        public void BuyProducts(Customer customer, Shop shop)
        {
            shop.BuyProducts(customer);
        }
    }
}