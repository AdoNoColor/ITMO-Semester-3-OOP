using System;
using Banks.Banks;
using Banks.Clients;
using Banks.Tools;

namespace Banks.Accounts
{
    public class Debit : Account
    {
        public Debit(Bank bank, Client client)
            : base(bank, client)
        {
            AccountType = AccountType.Debit;
        }

        public override void ChangeBalance(decimal amountOfMoney)
        {
            Balance += amountOfMoney;
            if (Balance < 0)
                throw new BanksException("You have reached the minus!");
        }

        public override void SpinTimeMechanism(DateTime oldDate, DateTime newDate)
        {
            ChangeBalance(Balance * (decimal)(newDate - oldDate).TotalDays * AttachedBank.Percent);
            Time = newDate;
        }

        public override void SetExpirationDate(DateTime dateTime)
        {
            throw new BanksException("Not this type of an account!");
        }
    }
}