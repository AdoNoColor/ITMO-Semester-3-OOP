using Reports.DataAccessLayer.Abstractions;
using Reports.DataAccessLayer.Task;
using Reports.DataAccessLayer.Worker;

namespace BusinessLogicLayer.TaskManager.TaskSetting
{
    public class StateSetter : AbstractSetter
    {
        private TaskState _state;

        public StateSetter(TaskState state)
        {
            _state = state;
        }
        
        public override void Set(Task task, Worker worker)
        {
            task.State = _state;
            CommitChanges(task, worker);
        }
    }
}