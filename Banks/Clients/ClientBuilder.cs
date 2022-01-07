namespace Banks.Clients
{
    public class ClientBuilder
    {
        private string _name;
        private string _surname;
        private string _address;
        private string _passportNumber;

        public void ResetSettings()
        {
            _name = null;
            _surname = null;
            _address = null;
            _passportNumber = null;
        }

        public ClientBuilder SetName(string name)
        {
            _name = name;
            return this;
        }

        public ClientBuilder SetSurname(string surname)
        {
            _surname = surname;
            return this;
        }

        public ClientBuilder SetAddress(string address)
        {
            _address = address;
            return this;
        }

        public ClientBuilder SetPassportNumber(string number)
        {
            _passportNumber = number;
            return this;
        }

        public Client GetClient()
        {
        var client = new Client(_name, _surname, _address, _passportNumber);
        ResetSettings();
        return client;
        }
    }
}