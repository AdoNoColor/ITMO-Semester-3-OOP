using System;
using Banks.Banks;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class Deposit : Account
    {
        public Deposit(Bank bank, Client client)
        : base(bank, client)
        {
            FirstReplenishment = false;
            AccountType = AccountType.Deposit;
        }

        public DateTime ExpirationDate { get; private set; }
        public decimal Percent { get; private set; }
        private bool FirstReplenishment { get; set; }

        public override void SetExpirationDate(DateTime expirationDate)
        {
            if (expirationDate < Time)
                throw new BanksException("Incorrect input");
            ExpirationDate = expirationDate;
        }

        public override void ChangeBalance(decimal amountOfMoney)
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

        public override void SpinTimeMechanism(DateTime oldDate, DateTime newDate)
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