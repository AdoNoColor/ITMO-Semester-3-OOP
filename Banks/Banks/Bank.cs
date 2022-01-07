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
        public List<Credit> CreditAccounts { get; set; } = new List<Credit>();
        public List<Debit> DebitAccounts { get; set; } = new List<Debit>();
        public List<Deposit> DepositAccounts { get; set; } = new List<Deposit>();
        public string Name { get; }
        public decimal Limit { get; }
        public decimal Percent { get; private set; }
        public decimal Commission { get; }
        public decimal TrustLimit { get; private set; }
        public decimal CreditLimit { get; }

        public IAccount AddAccount(Client client, string accountType)
        {
            if (Clients.All(anotherClient => anotherClient != client))
            {
                Clients.Add(client);
            }

            switch (accountType)
            {
                case "Debit":
                {
                    var account = new Debit(this, client);
                    DebitAccounts.Add(account);
                    return account;
                }

                case "Credit":
                {
                    var account = new Credit(this, client);
                    CreditAccounts.Add(account);
                    return account;
                }

                case "Deposit":
                {
                    var account = new Deposit(this, client);
                    DepositAccounts.Add(account);
                    return account;
                }

                default:
                {
                    throw new BanksException("No account type like this");
                }
            }
        }

        public void ChangeDepositAccountExpirationDate(IAccount account, DateTime newDate)
        {
            if (!(account is Deposit)) throw new BanksException("Not that type of account!");
            if (newDate < CentralBank.CurrentDate)
                throw new BanksException("Incorrect date input!");

            foreach (Deposit deposit in DepositAccounts.Where(deposit => deposit == account))
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

        public Notification SendNotification(string message, string concreteType)
        {
            var notification = new Notification(this, message);
            switch (concreteType)
            {
                case "Credit":
                {
                    foreach (Debit credit in Clients.SelectMany(client => DebitAccounts.Where(credit => credit.AttachedClient != client)))
                    {
                        credit.AttachedClient.AddTheNotification(notification, this);
                        return notification;
                    }

                    break;
                }

                case "Debit":
                {
                    foreach (Debit debit in Clients.SelectMany(client => DebitAccounts.Where(debit => debit.AttachedClient != client)))
                    {
                        debit.AttachedClient.AddTheNotification(notification, this);
                        return notification;
                    }

                    break;
                }

                case "Deposit":
                {
                    foreach (Deposit deposit in Clients.SelectMany(client => DepositAccounts.Where(deposit => deposit.AttachedClient != client)))
                    {
                        deposit.AttachedClient.AddTheNotification(notification, this);
                        return notification;
                    }

                    break;
                }

                case "All":
                {
                    foreach (Client client in Clients)
                    {
                        client.AddTheNotification(notification, this);
                        return notification;
                    }

                    break;
                }

                default:
                {
                    throw new BanksException("Incorrect input for ConcreteType");
                }
            }

            return null;
        }
    }
}