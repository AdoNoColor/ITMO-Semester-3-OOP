using System;
using Banks.Banks;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class Deposit : IAccount
    {
        public Deposit(Bank bank, Client client)
        {
            Id = Guid.NewGuid().ToString();
            AttachedBank = bank;
            AttachedClient = client;
            Time = CentralBank.CurrentDate;
            FirstReplenishment = false;
        }

        public Client AttachedClient { get; set; }
        public Bank AttachedBank { get; set; }
        public decimal Balance { get; private set; }
        public DateTime Time { get; set; }
        public DateTime ExpirationDate { get; private set; }
        public string Id { get; }
        public decimal Percent { get; private set; }
        private bool FirstReplenishment { get; set; }

        public void SetExpirationDate(DateTime expirationDate)
        {
            if (expirationDate < Time)
                throw new BanksException("Incorrect input");
            ExpirationDate = expirationDate;
        }

        public void ChangeBalance(decimal amountOfMoney)
        {
            if (FirstReplenishment == false)
            {
                SetPercent(amountOfMoney);
                FirstReplenishment = true;
            }

            if (amountOfMoney < 0 && Time < ExpirationDate)
                throw new BanksException("The deposit hasn't been expired!");
            Balance += amountOfMoney;
        }

        public string GetId()
        {
            return Id;
        }

        public void SpinTimeMechanism(DateTime oldDate, DateTime newDate)
        {
            ChangeBalance(Balance * (decimal)(newDate - oldDate).TotalDays * Percent);
            Time = newDate;
        }

        private void SetPercent(decimal money)
        {
            if (money < 50000)
            {
                Percent = AttachedBank.Percent * 0.07m;
                return;
            }

            if (money >= 50000 && money > 100000)
            {
                Percent = AttachedBank.Percent * 0.14m;
                return;
            }

            if (money >= 100000)
            {
                Percent = AttachedBank.Percent * 0.21m;
            }
        }
    }
}