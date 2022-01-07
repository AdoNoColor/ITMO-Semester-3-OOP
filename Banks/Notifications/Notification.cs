using Banks.Banks;

namespace Banks.Notifications
{
    public class Notification
    {
        public Notification(Bank bank, string message)
        {
            AttachedBank = bank;
            Message = message;
        }

        public Bank AttachedBank { get; }
        public string Message { get; }

        public string GetResult()
        {
            return $"{AttachedBank.Name}: {Message}";
        }
    }
}