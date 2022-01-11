using Reports.DataAccessLayer.Abstractions;
using Reports.DataAccessLayer.Task;
using Reports.DataAccessLayer.Worker;

namespace BusinessLogicLayer.TaskManager.TaskSetting
{
    public class CommentSetter : AbstractSetter
    {
        private string _comment;

        public CommentSetter(string comment)
        {
            _comment = comment;
        }
        
        public override void Set(Task task, Worker worker)
        {
            task.Comment = _comment;
            CommitChanges(task, worker);
        }
    }
}