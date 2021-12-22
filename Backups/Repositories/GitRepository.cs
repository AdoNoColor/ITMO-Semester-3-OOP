using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Repositories
{
    public class GitRepository : IRepository
    {
        public List<RestorePoint> RestorePoints { get; } = new List<RestorePoint>();
        public void CreateRestorePoint(BackupJob backupJob, string destination)
        {
            var restorePoint = new RestorePoint(backupJob);
            RestorePoints.Add(restorePoint);
        }
    }
}