using System;
using System.Collections.Generic;
using System.Linq;
using Banks.Accounts;
using Banks.Clients;
using Banks.Notifications;
using Banks.Tools;

namespace Banks.Banks
{
    public class Bank
    {
        public Bank(string name, decimal limit, decimal percent, decimal commission, decimal trustLimit, decimal creditLimit)
        {
            Name = name;
            Limit = limit;
            Percent = percent;
            Commission = commission;
            TrustLimit = trustLimit;
            CreditLimit = creditLimit;
        }

        public List<Client> Clients { get; set; } = new List<Client>();

        public List<Account> Accounts { get; } = new List<Account>();
        public string Name { get; }
        public decimal Limit { get; }
        public decimal Percent { get; private set; }
        public decimal Commission { get; }
        public decimal TrustLimit { get; private set; }
        public decimal CreditLimit { get; }

        public Account AddAccount(Client client, AccountType accountType)
        {
            if (Clients.All(anotherClient => anotherClient != client))
            {
                Clients.Add(client);
            }

            switch (accountType)
            {
                case AccountType.Debit:
                {
                    var account = new Debit(this, client);
                    Accounts.Add(account);
                    return account;
                }

                case AccountType.Credit:
                {
                    var account = new Credit(this, client);
                    Accounts.Add(account);
                    return account;
                }

                case AccountType.Deposit:
                {
                    var account = new Deposit(this, client);
                    Accounts.Add(account);
                    return account;
                }

                default:
                    throw new BanksException("Type of an account does not exist!");
            }
        }

        public void ChangeDepositAccountExpirationDate(Account account, DateTime newDate)
        {
            if (!(account is Deposit)) throw new BanksException("Not that type of account!");
            if (newDate < CentralBank.CurrentDate)
                throw new BanksException("Incorrect date input!");

            foreach (Account deposit in Accounts.Where(deposit => deposit.Id == account.Id))
            {
                deposit.SetExpirationDate(newDate);
            }
        }

        public void ChangeTrustLimit(decimal limit)
        {
            TrustLimit = limit;
        }

        public void ChangePercent(decimal percent)
        {
            Percent = percent;
        }

        public void TurnNotificationsOn(Client client)
        {
            foreach (Client anotherClient in Clients.Where(anotherClient => anotherClient == client))
            {
                anotherClient.TurnTheNotificationOn(this);
                return;
            }

            throw new BanksException("No client like this!");
        }

        public Notification SendNotification(string message, AccountType concreteType)
        {
            var notification = new Notification(this, message);
            switch (concreteType)
            {
                case AccountType.All:
                {
                    foreach (Account account in Accounts)
                    {
                        account.AttachedClient.AddTheNotification(notification, this);
                    }

                    return notification;
                }

                case AccountType.Deposit:
                {
                    foreach (Account account in Accounts.Where(account => account.AccountType == concreteType))
                    {
                        account.AttachedClient.AddTheNotification(notification, this);
                    }

                    return notification;
                }

                case AccountType.Debit:
                {
                    foreach (Account account in Accounts.Where(account => account.AccountType == concreteType))
                    {
                        account.AttachedClient.AddTheNotification(notification, this);
                    }

                    return notification;
                }

                case AccountType.Credit:
                {
                    foreach (Account account in Accounts.Where(account => account.AccountType == concreteType))
                    {
                        account.AttachedClient.AddTheNotification(notification, this);
                    }

                    return notification;
                }

                default:
                {
                    throw new BanksException("Incorrect input!");
                }
            }
        }
    }
}