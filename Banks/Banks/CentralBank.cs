using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.Clients;
using Banks.Operations;
using Banks.Tools;

namespace Banks.Banks
{
    public static class CentralBank
    {
        public static List<Bank> Banks { get; private set; } = new List<Bank>();

        public static DateTime CurrentDate { get; private set; } = DateTime.Now;

        public static Bank RegisterBank(string name, decimal limit, decimal percent, decimal commission, decimal trustLimit, decimal creditLimit)
        {
            var bank = new Bank(name, limit, percent, commission, trustLimit, creditLimit);
            if (Banks.Any(anotherBank => anotherBank == bank))
            {
                throw new BanksException("Bank like this exists!");
            }

            Banks.Add(bank);
            return bank;
        }

        public static void SpinTime(DateTime newDate)
        {
            if (newDate == CurrentDate)
                throw new BanksException("The same date!");
            if (newDate < CurrentDate)
                throw new BanksException("Lost in the past!");

            if (CurrentDate.Month == newDate.Month) return;

            foreach (IAccount account in Banks.SelectMany(bank => bank.DebitAccounts))
            {
                account.SpinTimeMechanism(CurrentDate, newDate);
            }

            foreach (IAccount account in Banks.SelectMany(bank => bank.DepositAccounts))
            {
                account.SpinTimeMechanism(CurrentDate, newDate);
            }

            foreach (IAccount account in Banks.SelectMany(bank => bank.CreditAccounts))
            {
                account.SpinTimeMechanism(CurrentDate, newDate);
            }

            CurrentDate = newDate;
        }

        public static Transferring TransferMoney(IAccount accountFrom, IAccount accountTo, decimal amountOfMoney)
        {
            var transaction = new Transferring(accountFrom, accountTo, amountOfMoney);
            return transaction;
        }

        public static Withdrawal WithdrawMoney(IAccount account, decimal amountOfMoney)
        {
            var transaction = new Withdrawal(account, amountOfMoney);
            return transaction;
        }

        public static TopUp TopUpBalance(IAccount account, decimal money)
        {
            var transaction = new TopUp(account, money);
            return transaction;
        }

        public static void CancelOperation(IOperation operation)
        {
            operation.CancelOperation();
        }
    }
}