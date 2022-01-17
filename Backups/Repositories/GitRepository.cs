using System;
using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Repositories
{
    public class GitRepository : IRepository
    {
        public List<RestorePoint> RestorePoints { get; } = new List<RestorePoint>();
        public void CreateRestorePoint(BackupJob backupJob)
        {
            var restorePoint = new RestorePoint(backupJob);
            RestorePoints.Add(restorePoint);
        }

        public void CreateRestorePoint(BackupJob backupJob, DateTime dateTime)
        {
            var restorePoint = new RestorePoint(backupJob, dateTime);
            RestorePoints.Add(restorePoint);
        }
    }
}