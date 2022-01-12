using System;
using System.Collections.Generic;
using Reports.DataAccessLayer.Reports;

namespace Reports.DataAccessLayer.Abstractions
{
    public abstract class AbstractReport
    {
        private static int _id = 0;
        public AbstractReport()
        {
            Id = _id++;
            Tasks = new List<Task.Task>();
            State = ReportState.Open;
            CreationDate = DateTime.Now;
        }
        public int Id { get; }
        public List<Task.Task> Tasks { get; }
        public ReportState State { get; set; }
        public DateTime CreationDate { get; }

        public void AddTask(Task.Task task)
        {
            Tasks.Add(task); 
        }
    }
}