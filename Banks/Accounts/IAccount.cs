using System;
using Banks.Banks;
using Banks.Clients;

namespace Banks.Accounts
{
    public interface IAccount
    {
        Client AttachedClient { get; set; }
        Bank AttachedBank { get; set; }
        decimal Balance { get; }
        DateTime Time { get; set; }
        AccountType AccountType { get; }
        string Id { get; }
        void ChangeBalance(decimal amountOfMoney);
        string GetId();
        void SpinTimeMechanism(DateTime oldDate, DateTime newDate);
    }
}