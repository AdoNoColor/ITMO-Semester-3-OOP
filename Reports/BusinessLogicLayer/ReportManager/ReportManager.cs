using System.Collections.Generic;
using Reports.DataAccessLayer.Abstractions;
using Reports.DataAccessLayer.Reports;
using Reports.DataAccessLayer.Worker;

namespace Reports.BusinessLogicLayer.ReportManager
{
    public class ReportManager
    {
        private ReportsStorage _reportsStorage;

        public ReportManager()
        {
            _reportsStorage = new ReportsStorage();
        }

        public Report CreateDailyReport(Worker worker) => new Report(worker);

        public SprintReport CreateSprintReport(Worker teamlead, List<Worker> workers) =>
            new SprintReport(teamlead, workers);

        public void LoadReport(Report report) => _reportsStorage.Add(report);
    }
}