using System;
using Banks.Accounts;
using Banks.Banks;
using Banks.Clients;
using Banks.Notifications;
using Banks.Operations;
using Banks.Tools;
using NUnit.Framework;

namespace Banks.Tests
{
    public class BanksTests
    {
        [Test]
        public void CreatingClientsAndAccount()
        {
            Bank sberbank = CentralBank.RegisterBank("Sberbank", 5000, 0.3m, 0.1m, 
                50000, 300000);
            var builder = new ClientBuilder();
            builder.SetName("Maxim");
            builder.SetSurname("Ivanov");
            builder.SetPassportNumber("3013 456969");
            builder.SetAddress("Kronversky pr., d. 49");
            Client max = builder.GetClient();
            Account account = sberbank.AddAccount(max, AccountType.Debit);
            Assert.Contains(account, sberbank.Accounts);
            Assert.Contains(max, sberbank.Clients);
            Assert.Contains(sberbank, CentralBank.Banks);
        }

        [Test]
        public void Transactions()
        {
            Bank tinkoff = CentralBank.RegisterBank("Tinkoff", 5000, 0.3m, 0.1m, 
                50000, 300000);
            Bank sberbank = CentralBank.RegisterBank("Sberbank", 5000, 0.3m, 0.1m, 
                50000, 300000);
            var builder = new ClientBuilder();
            
            builder.SetName("Maxim");
            builder.SetSurname("Ivanov");
            builder.SetPassportNumber("3013 456969");
            builder.SetAddress("Kronversky pr., d. 49");
            Client max = builder.GetClient();
            
            builder.SetName("Josh");
            builder.SetSurname("Hopkins");
            builder.SetPassportNumber("1234234");
            builder.SetAddress("Hollywood");
            Client josh = builder.GetClient();
            
            Account maxDebitAccount = sberbank.AddAccount(max, AccountType.Debit);
            Account maxCreditAccount = sberbank.AddAccount(max, AccountType.Credit);
            Account maxDepositAccount = sberbank.AddAccount(max, AccountType.Deposit);
            Account joshDebitAccount = tinkoff.AddAccount(max, AccountType.Debit);
            Account joshCreditAccount = tinkoff.AddAccount(max, AccountType.Credit);
            Account joshDepositAccount = tinkoff.AddAccount(max, AccountType.Deposit);

            CentralBank.TopUpBalance(maxDebitAccount, 300000);
            Assert.AreEqual(300000, maxDebitAccount.Balance);
            CentralBank.TopUpBalance(joshDebitAccount, 300000);
            CentralBank.TopUpBalance(joshDepositAccount, 20000);
            CentralBank.TransferMoney(joshDebitAccount, maxDebitAccount, 5000);
            CentralBank.WithdrawMoney(maxDebitAccount, 5000);
            tinkoff.ChangeDepositAccountExpirationDate(joshDepositAccount, DateTime.MaxValue);

            Assert.Catch<BanksException>(() =>
            {
                CentralBank.TransferMoney(joshCreditAccount, joshDebitAccount, 
                    5000);
            });
            
            Assert.Catch<BanksException>(() =>
            {
                CentralBank.WithdrawMoney(joshDepositAccount, 40000);
            });
        }

        [Test]
        public void TimeMechanism()
        {
            Bank tinkoff = CentralBank.RegisterBank("Tinkoff", 5000, 0.3m, 0.1m, 
                50000, 300000);
            var builder = new ClientBuilder();
            builder.SetName("Maxim");
            builder.SetSurname("Ivanov");
            builder.SetPassportNumber("3013 456969");
            builder.SetAddress("Kronversky pr., d. 49");
            Client max = builder.GetClient();
            Account debitAccount = tinkoff.AddAccount(max, AccountType.Debit);
            Account creditAccount = tinkoff.AddAccount(max, AccountType.Credit);
            Account depositAccount = tinkoff.AddAccount(max, AccountType.Deposit);
            CentralBank.TopUpBalance(debitAccount, 100m);
            Assert.AreEqual(100m, debitAccount.Balance);
            CentralBank.TopUpBalance(creditAccount, 100m);
            CentralBank.TopUpBalance(depositAccount, 100m);
            CentralBank.SpinTime(CentralBank.CurrentDate.AddMonths(1));
            Assert.AreEqual(1030m, debitAccount.Balance);
            Assert.AreEqual(165.1m, depositAccount.Balance);
        }

        [Test]
        public void Notifications()
        {
            Bank tinkoff = CentralBank.RegisterBank("Tinkoff", 5000, 0.3m, 0.1m, 50000, 
                300000);
            var builder = new ClientBuilder();
            builder.SetName("Maxim");
            builder.SetSurname("Ivanov");
            builder.SetPassportNumber("3013 456969");
            builder.SetAddress("Kronversky pr., d. 49");
            Client max = builder.GetClient();
            Account debitAccount = tinkoff.AddAccount(max, AccountType.Debit);
            Account creditAccount = tinkoff.AddAccount(max, AccountType.Credit);
            max.TurnTheNotificationOn(tinkoff);
            Notification notification = tinkoff.SendNotification("We are sorry, but debit accounts are " +
                                                                 "being closed!", AccountType.All);
            Assert.AreEqual(1, tinkoff.Clients.Count);
            Assert.Contains(notification, max.Notifications);
        }
    }
}