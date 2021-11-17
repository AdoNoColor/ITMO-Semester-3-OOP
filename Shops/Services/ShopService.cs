﻿using System;
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
        private readonly List<Tuple<uint, int>> _package = new List<Tuple<uint, int>>();

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

        public void ProductToDeliver(Product product, int price, int quantity)
        {
            Product found = _products.Find(item => item.Id == product.Id);
            if (found == null) throw new ShopException("No product like that!");
            {
                foreach (ProductExtra item in _deliver.Where(item => item.Id == product.Id))
                {
                    item.Price = price;
                    item.Quantity = quantity;
                    return;
                }

                var productExtra = new ProductExtra(found.Name, found.Id, quantity, price);
                _deliver.Add(productExtra);
            }
        }

        public void DeliverTo(Shop shop)
        {
            foreach (ProductExtra item in _deliver)
            {
                shop.AddInStock(item);
            }

            _deliver.Clear();
        }

        public void ChangePrice(Shop shop, Product product, int newPrice)
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
            foreach (Product item in _products)
            {
                if (item.Id == product.Id)
                    _package.Add(Tuple.Create(item.Id, quantity));
                return;
            }

            throw new ShopException("No product with the ID like that!");
        }

        /*public void RemoveAllProductsToSearch()
        {
            _package.Clear();
        }*/

        public int SearchProfitableDeal()
        {
            if (_package.Count == 0)
            {
                throw new ShopException("No products to search!");
            }

            int totalAmount = int.MaxValue;
            foreach (Shop shop in _shops)
            {
                uint productQuantity = 0;
                foreach (Tuple<uint, int> unused in _package.Where(t => shop.BoolFindProduct(t.Item1) && shop.FindProduct(t.Item1).Quantity >= t.Item2))
                {
                    productQuantity++;
                }

                if (productQuantity != _package.Count) throw new ShopException("Not enough products or products don't exist!");
                {
                    int currentTotalAmount = 0;
                    foreach ((uint item1, int item2) in _package)
                    {
                        currentTotalAmount += shop.ProductPrice(item1) * item2;
                        if (currentTotalAmount < totalAmount)
                        {
                            totalAmount = currentTotalAmount;
                        }
                    }
                }
            }

            if (totalAmount != int.MaxValue)
            {
                return totalAmount;
            }

            throw new ShopException("Something ain't right!");
        }

        public void BuyProducts(Customer customer, Shop shop)
        {
            for (int i = 0; i < customer.GetWishlistQuantity(); i++)
            {
                for (int j = 0; j < shop.StockQuantity(); j++)
                {
                    if (customer.GetItemFromWishList(i) == shop.GetItemFromStock(j).Id &&
                        customer.GetItemQuantityFromWishList(i) <= shop.GetItemFromStock(j).Quantity)
                    {
                        if (customer.Balance - (shop.GetItemFromStock(j).Price * customer.GetItemQuantityFromWishList(i)) < 0)
                        {
                            throw new ShopException("Not enough money!");
                        }

                        customer.Balance -= shop.GetItemFromStock(j).Price * customer.GetItemQuantityFromWishList(i);
                        shop.ChangeItemQuantity(j, customer.GetItemQuantityFromWishList(i));
                    }
                }
            }
        }
    }
}