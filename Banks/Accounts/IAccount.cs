using System;
using Banks.Banks;
using Banks.Clients;

namespace Banks.Accounts
{
    public interface IAccount
    {
        public Client AttachedClient { get; set; }
        public Bank AttachedBank { get; set; }
        public decimal Balance { get; }
        public DateTime Time { get; set; }
        public string Id { get; }
        public void ChangeBalance(decimal amountOfMoney);
        /* public void SpinTimeMechanism(DateTime newDate); */

        public string GetId();
        public void SpinTimeMechanism(DateTime oldDate, DateTime newDate);
    }
}