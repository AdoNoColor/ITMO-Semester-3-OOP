using System;
using Banks.Banks;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class Debit : IAccount
    {
        public Debit(Bank bank, Client client)
        {
            Id = Guid.NewGuid().ToString();
            AttachedBank = bank;
            AttachedClient = client;
            Time = CentralBank.CurrentDate;
            AccountType = AccountType.Debit;
        }

        public Client AttachedClient { get; set; }
        public Bank AttachedBank { get; set; }
        public decimal Balance { get; private set; }
        public DateTime Time { get; set; }
        public string Id { get; }

        public AccountType AccountType { get; }

        public string GetId()
        {
            return Id;
        }

        public void ChangeBalance(decimal amountOfMoney)
        {
            Balance += amountOfMoney;
            if (Balance < 0)
                throw new BanksException("You have reached the minus!");
        }

        public void SpinTimeMechanism(DateTime oldDate, DateTime newDate)
        {
            ChangeBalance(Balance * (decimal)(newDate - oldDate).TotalDays * AttachedBank.Percent);
            Time = newDate;
        }
    }
}