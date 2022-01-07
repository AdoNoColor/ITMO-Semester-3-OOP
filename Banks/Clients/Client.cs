using System.Collections.Generic;
using System.Linq;
using Banks.Banks;
using Banks.Notifications;
using Banks.Tools;

namespace Banks.Clients
{
    public class Client
    {
        public Client(string name, string surname, string address, string passportNumber)
        {
            Name = name;
            Surname = surname;
            Address = address;
            PassportNumber = passportNumber;
            if (Address == null || PassportNumber == null)
                LevelOfTrust = false;
            else
                LevelOfTrust = true;
        }

        public List<Notification> Notifications { get; private set; } = new List<Notification>();
        public List<Bank> AllowedBanksToNotify { get; } = new List<Bank>();

        public string Name { get; }
        public string Surname { get; }
        public string Address { get; }
        public string PassportNumber { get; }
        public bool LevelOfTrust { get; }

        public void TurnTheNotificationOn(Bank bank)
        {
            if (AllowedBanksToNotify.Any(anotherBank => anotherBank == bank))
            {
                throw new BanksException("The bank is already on notification list");
            }

            AllowedBanksToNotify.Add(bank);
        }

        public void AddTheNotification(Notification notification, Bank bank)
        {
            if (AllowedBanksToNotify.Any(anotherBank => anotherBank != bank))
            {
                throw new BanksException("Bank is not on the list allowing it to notify you!");
            }

            if (Notifications.Any(anotherNotification => anotherNotification == notification))
            {
                return;
            }

            Notifications.Add(notification);
        }
    }
}