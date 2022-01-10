using System;
using Banks.Banks;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public abstract class Account
    {
        public Account(Bank bank, Client client)
        {
            Id = Guid.NewGuid().ToString();
            AttachedBank = bank;
            AttachedClient = client;
            Time = CentralBank.CurrentDate;
            AccountType = AccountType.Debit;
        }

        public Client AttachedClient { get; set; }
        public Bank AttachedBank { get; set; }
        public decimal Balance { get; private protected set; }
        public DateTime Time { get; set; }
        public AccountType AccountType { get; private protected set;  }
        public string Id { get; }
        public abstract void ChangeBalance(decimal amountOfMoney);

        public string GetId()
        {
            return Id;
        }

        public abstract void SpinTimeMechanism(DateTime oldDate, DateTime newDate);

        public abstract void SetExpirationDate(DateTime dateTime);
    }
}
