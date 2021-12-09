using System.Collections.Generic;

namespace Backups.Entities
{
    public class RestorePoint
    {
        public RestorePoint(BackupJob backupJob)
        {
            Storages = backupJob.UsingAlgorithm.StartAlgorithm(backupJob);
        }

        public List<string> Storages { get; }
    }
}