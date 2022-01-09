using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using BackupsExtra.Entities;

namespace BackupsExtra.LimitAlgorithms.CleanAlgorithm
{
    public class CleanByAmount : ICleanAlgorithm
    {
        public List<RestorePoint> FindPoints(BackupJobExtra backupJob)
        {
            var result = new List<RestorePoint>();
            if (backupJob.RestorePointMaxAmount >= backupJob.Repository.RestorePoints.Count)
                return result;
            result = backupJob.Repository.RestorePoints.GetRange(0, backupJob.RestorePointMaxAmount - 1);
            return result;
        }

        public void StartAlgorithm(BackupJobExtra backupJobExtra, List<RestorePoint> foundPoints)
        {
            foreach (RestorePoint point in foundPoints.Where(point =>
                backupJobExtra.Repository.RestorePoints.Contains(point)))
            {
                backupJobExtra.Repository.RestorePoints.Remove(point);
            }
        }
    }
}