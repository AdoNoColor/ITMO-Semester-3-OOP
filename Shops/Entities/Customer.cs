using System;
using System.Collections.Generic;
using Shops.Tools;

namespace Shops.Entities
{
    public class Customer
    {
        private static uint _id = 0;
        private List<ProductExtra> _wishlist = new List<ProductExtra>();
        public Customer(int balance)
        {
            _id++;
            Balance = balance;
            Id = _id;
        }

        public decimal Balance { get; private set; }
        private uint Id { get; }

        public void Payment(decimal price)
        {
            if (price < Balance)
                throw new ShopException("Not enough money!");
            Balance -= price;
        }

        public void AddToWishlist(Product product, int quantity)
        {
            var foundProduct = new ProductExtra(product.Name, product.Id, quantity);
            _wishlist.Add(foundProduct);
        }

        public void DeleteFromWishlist(int i)
        {
            _wishlist.Remove(_wishlist[i]);
        }

        public void ClearWishlist()
        {
            _wishlist.Clear();
        }

        public uint GetItemIdFromWishList(int i)
        {
            return _wishlist[i].Id;
        }

        public int GetItemQuantityFromWishList(int i)
        {
            return _wishlist[i].Quantity;
        }

        public int GetWishlistQuantity()
        {
            return _wishlist.Count;
        }
    }
}