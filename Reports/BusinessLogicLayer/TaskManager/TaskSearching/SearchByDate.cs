using System;
using System.Collections.Generic;
using Reports.DataAccessLayer.Task;

namespace Reports.BusinessLogicLayer.TaskManager.TaskSearching
{
    public class SearcherByDate : ISearcher
    {
        private DateTime _date;

        public SearcherByDate(DateTime date)
        {
            _date = date;
        }
        
        public List<Task> Search(TasksStorage tasksStorage)
        {
            return tasksStorage.Find(task => task.LastChangesDate == _date);
        }
    }
}