using System;
using Banks.Accounts;
using Banks.Tools;

namespace Banks.Operations
{
    public class Withdrawal
    {
        public Withdrawal(Account account, decimal amountOfMoney)
        {
            Id = Guid.NewGuid().ToString();
            Account = account;
            AmountOfMoney = amountOfMoney;
            IsCancelled = false;
            if (account is Credit)
                throw new BanksException("Incorrect input!");
            if (Account.AttachedClient.LevelOfTrust == false && Account.AttachedBank.Limit < amountOfMoney)
                throw new BanksException("Bank doesn't trust that client");
            Account.ChangeBalance(-AmountOfMoney);
        }

        public string Id { get; }

        public Account Account { get; }
        public decimal AmountOfMoney { get; }
        public bool IsCancelled { get; private set; }

        public void CancelOperation()
        {
            if (IsCancelled)
                throw new BanksException("Operation has been already cancelled!");
            Account.ChangeBalance(AmountOfMoney);
            IsCancelled = true;
        }
    }
}