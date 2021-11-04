using System;
using System.Collections.Generic;

namespace Shops.Entities
{
    public class Customer
    {
        private List<Tuple<uint, int>> _wishlist = new List<Tuple<uint, int>>();
        public Customer(int balance, uint id)
        {
            Balance = balance;
            Id = id;
        }

        public int Balance { get; set; }
        private uint Id { get; }

        public void AddToWishlist(Product product, int quantity)
        {
            _wishlist.Add(Tuple.Create(product.Id, quantity));
        }

        public void DeleteFromWishlist(int i)
        {
            _wishlist.Remove(_wishlist[i]);
        }

        public void ClearWishlist()
        {
            _wishlist.Clear();
        }

        public uint GetItemFromWishList(int i)
        {
            return _wishlist[i].Item1;
        }

        public int GetItemQuantityFromWishList(int i)
        {
            return _wishlist[i].Item2;
        }

        public int GetWishlistQuantity()
        {
            return _wishlist.Count;
        }
    }
}