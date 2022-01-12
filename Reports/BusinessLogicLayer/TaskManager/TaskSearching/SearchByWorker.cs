using System.Collections.Generic;
using Reports.DataAccessLayer.Task;
using Reports.DataAccessLayer.Worker;

namespace Reports.BusinessLogicLayer.TaskManager.TaskSearching
{
    public class SearcherByWorker : ISearcher
    {
        private Worker _worker;

        public SearcherByWorker(Worker worker)
        {
            _worker = worker;
        }
        
        public List<Task> Search(TasksStorage tasksStorage)
        {
            return tasksStorage.Find(task => task.Worker.Id == _worker.Id);
        }
    }
}