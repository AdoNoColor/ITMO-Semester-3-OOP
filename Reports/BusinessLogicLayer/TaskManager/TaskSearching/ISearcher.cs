using System.Collections.Generic;
using Reports.DataAccessLayer.Task;

namespace Reports.BusinessLogicLayer.TaskManager.TaskSearching
{
    public interface ISearcher
    {
        List<Task> Search(TasksStorage tasksStorage);
    }
}