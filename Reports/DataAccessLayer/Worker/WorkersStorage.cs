using System;
using System.Collections.Generic;
using System.Linq;
using Reports.DataAccessLayer.Abstractions;

namespace Reports.DataAccessLayer.Worker
{
    public class WorkersStorage : AbstractStorage<Worker>
    {
        public List<Worker> Data { get; private set; }

        public void Add(Worker item)
        {
            Data.Add(item);
        }

        public bool Remove(Worker item)
        {
            return Data.Remove(item);
        } 

        public List<Worker> Find(Func<Worker, bool> predicate)
        {
            var results = Data.Where(predicate).ToList();

            return results.Count != 0 ? results : null;
        }
    }
}