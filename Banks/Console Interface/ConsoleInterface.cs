using System;
using System.Linq;
using System.Threading;
using Banks.Accounts;
using Banks.Banks;
using Banks.Clients;

namespace Banks.Console_Interface
{
    public class ConsoleInterface
    {
        public ConsoleInterface(Bank attachedBank1)
        {
            AttachedBank = attachedBank1;
        }

        private Bank AttachedBank { get; }

        public void StartApplication()
        {
            Console.Clear();
            while (true)
            {
                Client client = Authorize();
                bool exitMenuFlag = false;
                while (!exitMenuFlag)
                {
                    Menu();
                    int option = int.Parse(Console.ReadLine() ?? string.Empty);
                    Console.Clear();
                    switch (option)
                    {
                        case 1:
                            SubscribeToNotifications(client);
                            Console.Clear();
                            Thread.Sleep(3000);
                            break;
                        case 2:
                            ShowAccounts(client);
                            Console.Clear();
                            Thread.Sleep(3000);
                            break;
                        case 3:
                            RegisterAccount(client);
                            Console.Clear();
                            Thread.Sleep(3000);
                            break;
                        case 4:
                            TransferTransaction();
                            Thread.Sleep(3000);
                            Console.Clear();
                            break;
                        case 5:
                            TopUpTheBalance();
                            Thread.Sleep(3000);
                            Console.Clear();
                            break;
                        case 6:
                            WithdrawMoney();
                            Thread.Sleep(3000);
                            Console.Clear();
                            break;
                        case 7:
                            exitMenuFlag = true;
                            Console.WriteLine($"Bye bye, {client.Name} {client.Surname}");
                            Thread.Sleep(3000);
                            break;
                    }
                }
            }
        }

        private void Menu()
        {
            Console.WriteLine($"Welcome to {AttachedBank.Name}!");
            Console.WriteLine("1 - Subscribe to notifications\n" +
                              "2 - Show all Accounts\n" +
                              "3 - Register Account\n" +
                              "4 - Transfer money\n" +
                              "5 - Top Up Account\n" +
                              "6 - Withdraw Money\n" +
                              "7 - Exit\n" +
                              "Type the number:");
        }

        private Client Authorize()
        {
            var builder = new ClientBuilder();
            Console.WriteLine("Please type your name:");
            builder.SetName(Console.ReadLine());
            Console.WriteLine("Please type your surname:");
            builder.SetSurname(Console.ReadLine());
            Console.WriteLine("Please type your address:");
            builder.SetAddress(Console.ReadLine());
            Console.WriteLine("Please type your passport number:");
            builder.SetPassportNumber(Console.ReadLine());
            Console.WriteLine("Please type your name:");
            Console.WriteLine("Thank you!:");
            return builder.GetClient();
        }

        private void SubscribeToNotifications(Client client)
        {
            client.TurnTheNotificationOn(AttachedBank);
            Console.WriteLine("You've successfully subscribed!");
        }

        private void ShowAccounts(Client client)
        {
            foreach (var account in AttachedBank.Accounts.Where(account =>
                account.AttachedClient == client))
            {
                Console.WriteLine($"Type: {account.AccountType}\n" +
                                  $"Id: {account.Id}\n"
                                  + $"Balance: {account.Balance}");
            }
        }

        private void RegisterAccount(Client client)
        {
            Console.WriteLine("What type of an account do you want?");
            Console.WriteLine("1 - Debit\n" +
                              "2 - Credit\n" +
                              "3 - Deposit\n");
            int accountType = int.Parse(Console.ReadLine() ?? string.Empty);
            switch (accountType)
            {
                case 1:
                {
                    Account account = AttachedBank.AddAccount(client, AccountType.Debit);
                    Console.WriteLine($"Account {account.Id} has been successfully created!");
                    break;
                }

                case 2:
                {
                    Account account = AttachedBank.AddAccount(client, AccountType.Credit);
                    Console.WriteLine($"Account {account.Id} has been successfully created!");
                    break;
                }

                case 3:
                {
                    Account account = AttachedBank.AddAccount(client, AccountType.Deposit);
                    Console.WriteLine($"Account {account.Id} has been successfully created!");
                    break;
                }

                default:
                {
                    Console.WriteLine("Something went wrong!");
                    break;
                }
            }
        }

        private void TransferTransaction()
        {
            Account accountFrom = null;
            Account accountTo = null;
            Console.WriteLine("Please type the Id of your account:");
            string idFrom = Console.ReadLine();
            Console.WriteLine("Please type the Id of an account in which you want to transfer your money:");
            string idTo = Console.ReadLine();
            Console.WriteLine("Please type how much money you want to transfer:");
            int balance = int.Parse(Console.ReadLine() ?? string.Empty);
            Console.WriteLine("Does the another account belong to Our bank?");
            Console.WriteLine("1 - yes\n" +
                              "2 - no\n");
            int decision = int.Parse(Console.ReadLine() ?? string.Empty);
            switch (decision)
            {
                case 1:
                {
                    foreach (var account in AttachedBank.Accounts)
                    {
                        if (idFrom == account.Id)
                            accountFrom = account;
                        if (idTo == account.Id)
                            accountTo = account;
                        if (accountFrom != null && accountTo != null)
                            CentralBank.TransferMoney(accountFrom, accountTo, balance);
                        Console.WriteLine("Transaction was successful!");
                        break;
                    }

                    Console.WriteLine("Something went wrong!");
                    break;
                }

                case 2:
                {
                    {
                        foreach (Bank bank in CentralBank.Banks)
                        {
                            foreach (var account in bank.Accounts)
                            {
                                if (idFrom == account.Id)
                                    accountFrom = account;
                                if (idTo == account.Id)
                                    accountTo = account;
                                Console.WriteLine("Transaction was successful!");
                                if (accountFrom == null || accountTo == null) continue;
                                CentralBank.TransferMoney(accountFrom, accountTo, balance);
                                break;
                            }
                        }
                    }

                    Console.WriteLine("Something went wrong!");
                    break;
                }
            }
        }

        private void TopUpTheBalance()
        {
            Console.WriteLine("Please type the Id of your account:");
            string id = Console.ReadLine();
            Console.WriteLine("How much money do you want to add?");
            int balance = int.Parse(Console.ReadLine() ?? string.Empty);
            foreach (var account in AttachedBank.Accounts.Where(account => account.Id == id))
            {
                CentralBank.TopUpBalance(account, balance);
                Console.WriteLine("Transaction was successful!");
                return;
            }

            Console.WriteLine("Something went wrong!");
        }

        private void WithdrawMoney()
        {
            Console.WriteLine("Please type the Id of your account:");
            string id = Console.ReadLine();
            Console.WriteLine("How much money do you want to withdraw?");
            int balance = int.Parse(Console.ReadLine() ?? string.Empty);
            foreach (var account in AttachedBank.Accounts.Where(account => account.Id == id))
            {
                CentralBank.WithdrawMoney(account, balance);
                Console.WriteLine("Transaction was successful!");
                return;
            }

            Console.WriteLine("Something went wrong!");
        }
    }
}