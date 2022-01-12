using System.Collections.Generic;
using Reports.DataAccessLayer.Abstractions;

namespace Reports.DataAccessLayer.Reports
{
    public class SprintReport : AbstractReport
    {
        public global::Reports.DataAccessLayer.Worker.Worker Teamlead { get; }
        public List<global::Reports.DataAccessLayer.Worker.Worker> Team { get; }
        
        public List<Report> DailyReport { get; }

        public SprintReport(global::Reports.DataAccessLayer.Worker.Worker teamlead, List<global::Reports.DataAccessLayer.Worker.Worker> team)
        {
            Teamlead = teamlead;
            Team = team;
        }
    }
}