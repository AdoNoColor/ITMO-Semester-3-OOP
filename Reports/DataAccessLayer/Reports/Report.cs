using System;
using System.Collections.Generic;
using Reports.DataAccessLayer.Abstractions;

namespace Reports.DataAccessLayer.Reports
{
    public class Report
    {
        public global::Reports.DataAccessLayer.Worker.Worker Worker { get; }

        public Report(global::Reports.DataAccessLayer.Worker.Worker worker)
        {
            Worker = worker;
        }
    }
}