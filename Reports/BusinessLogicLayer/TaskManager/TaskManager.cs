using System.Collections.Generic;
using Reports.BusinessLogicLayer.TaskManager.TaskSearching;
using Reports.DataAccessLayer.Abstractions;
using Reports.DataAccessLayer.Task;
using Reports.DataAccessLayer.Worker;

namespace Reports.BusinessLogicLayer.TaskManager
{
    public class TaskManager
    {
        private TasksStorage _tasksStorage;

        public TaskManager()
        {
            _tasksStorage = new TasksStorage();
        }

        public Task CreateTask(string name, string description, Worker worker)
        {
            return new Task(name, description, worker);
        }

        public void AddTask(Task task)
        {
            _tasksStorage.Add(task);
  
        }

        public void SetTask(Task task, Worker worker, AbstractSetter setter)
        {
            setter.Set(task, worker); 
        }

        public List<Task> SearchTasks(ISearcher searcher)
        {
            return searcher.Search(_tasksStorage);
        }
    }
}