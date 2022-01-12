using System;
using Banks.Banks;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class Credit : Account
    {
        public Credit(Bank bank, Client client)
            : base(bank, client)
        {
            AccountType = AccountType.Credit;
            Limit = AttachedBank.CreditLimit;
        }

        public decimal Limit { get; private set; }

        public override void ChangeBalance(decimal amountOfMoney)
        {
            if (amountOfMoney > AttachedBank.TrustLimit && AttachedClient.LevelOfTrust == false)
                throw new BanksException("Incorrect input for Credit account!");
            if (Balance < 0)
            {
                Balance += amountOfMoney - (amountOfMoney * AttachedBank.Commission);
            }

            Balance += amountOfMoney;
            if (Balance < -Limit || Balance > Limit)
                throw new BanksException("You have reached the limit!");
        }

        public override void SpinTimeMechanism(DateTime oldDate, DateTime newDate)
        {
            ChangeBalance(Balance * AttachedBank.Commission * ((decimal)(newDate - oldDate).TotalDays
                                                               / 30.4m));
            Time = newDate;
        }

        public override void SetExpirationDate(DateTime dateTime)
        {
            throw new BanksException("Not this type of an account!");
        }
    }
}