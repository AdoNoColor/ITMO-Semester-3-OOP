using Reports.DataAccessLayer.Reports;
using Reports.DataAccessLayer.Task;
using Reports.DataAccessLayer.Worker;

namespace Reports.Models
{
    public class TodoItemDTO
    {
        public Worker Worker { get; set; }
        public Task Task { get; set; }
        public Report Report { get; set; }
        public WorkersStorage WorkersStorage { get; set; }
        public TasksStorage TasksStorage { get; set; }
        public ReportsStorage ReportsStorage { get; set; }
        public long Id { get; set; }
    }

}