
namespace Reports.DataAccessLayer.Abstractions
{
    public abstract class AbstractSetter
    {
        protected void CommitChanges(Task.Task task, Worker.Worker worker)
        {
            task.Log.Add((worker, this));
        }

        public abstract void Set(Task.Task task, Worker.Worker worker);
    }
}