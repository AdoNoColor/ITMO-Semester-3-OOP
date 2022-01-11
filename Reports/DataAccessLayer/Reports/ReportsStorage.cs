using System;
using System.Collections.Generic;
using System.Linq;
using Reports.DataAccessLayer.Abstractions;

namespace Reports.DataAccessLayer.Reports
{
    public class ReportsStorage : AbstractStorage<Report>
    {
        public List<Report> Data { get; private set; }

        public void Add(Report item) => Data.Add(item);

        public bool Remove(Report item) => Data.Remove(item);

        public List<Report> Find(Func<Report, bool> predicate)
        {
            var ans = Data.Where(item => predicate(item)).ToList();

            return ans.Count != 0 ? ans : null;
        }
    }
}