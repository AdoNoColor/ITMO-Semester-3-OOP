using System.Collections.Generic;
using Backups.Algorithms;
using Backups.Repositories;
using Backups.Tools;

namespace Backups.Entities
{
    public class BackupJob
    {
        public BackupJob(IRepository rep, IAlgorithm usingAlgorithm)
        {
            Repository = rep;
            UsingAlgorithm = usingAlgorithm;
        }

        public List<string> JobObjects { get; } = new List<string>();
        public IAlgorithm UsingAlgorithm { get; }
        public IRepository Repository { get; }

        public virtual void AddObject(string objectName)
        {
            JobObjects.Add(objectName);
        }

        public virtual void DeleteObject(string objectName)
        {
            JobObjects.Remove(objectName);
        }

        public virtual void Execute()
        {
            Repository.CreateRestorePoint(this);
        }
    }
}