using System;
using Banks.Accounts;
using Banks.Tools;

namespace Banks.Operations
{
    public class Transferring
    {
        public Transferring(IAccount accountFrom, IAccount accountTo, decimal money)
        {
            Id = Guid.NewGuid().ToString();
            AccountFrom = accountFrom;
            AccountTo = accountTo;
            AmountOfMoney = money;
            IsCancelled = false;
            if (AccountFrom is Credit)
                throw new BanksException("Cannot transfer money from that type of account!");

            AccountTo.ChangeBalance(AmountOfMoney);
            AccountFrom.ChangeBalance(-AmountOfMoney);
        }

        public string Id { get; }
        public IAccount AccountFrom { get; }
        public IAccount AccountTo { get; }
        public decimal AmountOfMoney { get; }
        public bool IsCancelled { get; private set; }

        public void CancelOperation()
        {
            if (IsCancelled)
                throw new BanksException("Operation has been already cancelled!");
            AccountTo.ChangeBalance(-AmountOfMoney);
            AccountFrom.ChangeBalance(AmountOfMoney);
            IsCancelled = true;
        }
    }
}