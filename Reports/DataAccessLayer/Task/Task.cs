using System;
using System.Collections.Generic;
using Reports.DataAccessLayer.Abstractions;

namespace Reports.DataAccessLayer.Task
{
    public class Task
    {
        private static int _id = 0;
        public Task(string name, string description, global::Reports.DataAccessLayer.Worker.Worker worker)
        {
            Id = _id++;
            Name = name;
            Description = description;
            Worker = worker;
            State = TaskState.Open;
            LastChangesDate = DateTime.Now;
            Log = new List<(global::Reports.DataAccessLayer.Worker.Worker worker, AbstractSetter setter)>();
        }
        public int Id { get; }
        public string Name { get; }
        public string Description { get; }
        public string Comment { get; set; }
        public global::Reports.DataAccessLayer.Worker.Worker Worker { get; set; }
        public TaskState State { get; set; }
        public DateTime LastChangesDate { get; }
        public List<(global::Reports.DataAccessLayer.Worker.Worker worker, AbstractSetter setter)> Log { get; }
    }
}