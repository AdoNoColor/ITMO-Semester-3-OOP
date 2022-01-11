using System.Collections.Generic;
using Reports.DataAccessLayer.Worker;

namespace Reports.BusinessLogicLayer.WorkersManager
{
    public class WorkersManager
    {
        private WorkersStorage _workersStorage;

        public WorkersManager()
        {
            _workersStorage = new WorkersStorage();
        }

        public Worker CreateWorker(string name)
        {
            return new Worker(name);
        }

        public void AddWorker(Worker worker)
        {
            _workersStorage.Add(worker);
        }

        public bool RemoveWorker(Worker worker)
        {
            return _workersStorage.Remove(worker); 
        } 

        public List<Worker> GetAllWorkers()
        {
            return _workersStorage.Data;
        }

        public void SetBoss(Worker worker, Worker newBoss)
        {
            var workerInstance = _workersStorage.Data.Find(worker1 => worker.Id == worker1.Id);
            if (workerInstance != null) 
                workerInstance.Boss = newBoss;
        }
    }
}