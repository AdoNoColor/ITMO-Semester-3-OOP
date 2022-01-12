using System.Collections.Generic;

namespace Reports.DataAccessLayer.Worker
{
    public class Worker
    {
        private static int _id = 0;
        public Worker(string name)
        {
            Id = _id++;
            Name = name;
        }
        public List<Worker> SubWorkers { get; set; }
        public int Id { get; }
        public string Name { get; }
        public Worker Boss { get; set; }
    }
}