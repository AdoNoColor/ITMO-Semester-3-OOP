using System;
using System.Collections.Generic;
using System.IO;

namespace Backups.Entities
{
    public class RestorePoint
    {
        public RestorePoint(BackupJob backupJob)
        {
            Storages = backupJob.UsingAlgorithm.StartAlgorithm(backupJob);
            foreach (string jobObject in backupJob.JobObjects)
                OriginalLocation.Add(jobObject);
        }

        public RestorePoint(BackupJob backupJob, DateTime dateTime)
        {
            Storages = backupJob.UsingAlgorithm.StartAlgorithm(backupJob);

            foreach (string jobObject in backupJob.JobObjects)
                OriginalLocation.Add(jobObject);

            DateOfCreation = dateTime;
        }

        public List<string> Storages { get; }
        public List<string> OriginalLocation { get; } = new List<string>();
        public DateTime DateOfCreation { get; set; } = DateTime.Now;
    }
}