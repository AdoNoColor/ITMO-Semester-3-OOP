using System;
using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Repositories
{
    public interface IRepository
    {
        public List<RestorePoint> RestorePoints { get; }
        public void CreateRestorePoint(BackupJob backupJob);
        public void CreateRestorePoint(BackupJob backupJob, DateTime dateTime);
    }
}