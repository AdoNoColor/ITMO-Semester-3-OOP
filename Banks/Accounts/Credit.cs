using System;
using Banks.Banks;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class Credit : IAccount
    {
        public Credit(Bank bank, Client client)
        {
            Id = Guid.NewGuid().ToString();
            AttachedBank = bank;
            AttachedClient = client;
            Time = CentralBank.CurrentDate;
            Limit = AttachedBank.CreditLimit;
        }

        public Client AttachedClient { get; set; }
        public Bank AttachedBank { get; set; }
        public decimal Balance { get; private set; }
        public decimal Limit { get; private set; }
        public DateTime Time { get; set; }
        public string Id { get; }

        public string GetId()
        {
            return Id;
        }

        public void ChangeBalance(decimal amountOfMoney)
        {
            if (amountOfMoney > AttachedBank.TrustLimit && AttachedClient.LevelOfTrust == false)
                throw new BanksException("Incorrect input for Credit account!");
            if (Balance < 0)
            {
                Balance += amountOfMoney - (amountOfMoney * (decimal)AttachedBank.Commission);
            }

            Balance += amountOfMoney;
            if (Balance < -Limit || Balance > Limit)
                throw new BanksException("You have reached the limit!");
        }

        public void SpinTimeMechanism(DateTime oldDate, DateTime newDate)
        {
            ChangeBalance(Balance * AttachedBank.Commission * ((decimal)(newDate - oldDate).TotalDays
                                                               / 30.4m));
            Time = newDate;
        }
    }
}