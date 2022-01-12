using System.Collections.Generic;
using Reports.DataAccessLayer.Task;
using Reports.DataAccessLayer.Worker;

namespace Reports.BusinessLogicLayer.TaskManager.TaskSearching
{
    public class SearchByEditing : ISearcher
    {
        private Worker _worker;
        
        public SearchByEditing(Worker worker)
        {
            _worker = worker;
        }

        public List<Task> Search(TasksStorage tasksStorage)
        {
            return tasksStorage.Find(task => task.Log.Find(tuple => tuple.worker.Id == _worker.Id) != default);
        }
    }
}