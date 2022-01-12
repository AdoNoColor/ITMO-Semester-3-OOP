using Reports.DataAccessLayer.Reports;
using Reports.DataAccessLayer.Task;
using Reports.DataAccessLayer.Worker;

namespace Reports.Models
    {
        public class TodoItem
        {
            public WorkersStorage WorkersStorage { get; set; }
            public Worker Worker { get; set; }
            public TasksStorage TasksStorage { get; set; }
            public Task Task { get; set; }
            public ReportsStorage ReportsStorage { get; set; }
            public Report Report { get; set; }
            public string? Secret { get; set; }
            public long Id { get; set; }
        }
    }