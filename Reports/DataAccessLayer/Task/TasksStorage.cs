using System;
using System.Collections.Generic;
using System.Linq;
using Reports.DataAccessLayer.Abstractions;

namespace Reports.DataAccessLayer.Task
{
    public class TasksStorage : AbstractStorage<Task>
    {
        public List<Task> Data { get; private set; }

        public void Add(Task item)
        {
            Data.Add(item);
        } 

        public void Remove(Task item)
        { 
            Data.Remove(item); 
        } 

        public List<Task> Find(Func<Task, bool> predicate)
        {
            var result = Data.Where(predicate).ToList();

            return result.Count != 0 ? result : null;
        }
    }
}