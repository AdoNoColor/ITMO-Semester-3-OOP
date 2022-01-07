using System;
using Banks.Accounts;
using Banks.Tools;

namespace Banks.Operations
{
    public class TopUp
    {
        public TopUp(IAccount account, decimal money)
        {
            Id = Guid.NewGuid().ToString();
            if (money < 0)
                throw new BanksException("Incorrect input");
            Money = money;
            Account = account;
            account.ChangeBalance(Money);
            IsCancelled = false;
        }

        public decimal Money { get; }
        public IAccount Account { get; }
        public bool IsCancelled { get; private set; }
        public string Id { get; }

        public void CancelOperation()
        {
            if (IsCancelled)
                throw new BanksException("Operation has been already cancelled!");
            Account.ChangeBalance(-Money);
            IsCancelled = true;
        }
    }
}