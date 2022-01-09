using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using BackupsExtra.Entities;

namespace BackupsExtra.LimitAlgorithms.CleanAlgorithm
{
    public class CleanByDate : ICleanAlgorithm
    {
        public List<RestorePoint> FindPoints(BackupJobExtra backupJob)
        {
            var result = new List<RestorePoint>();

            foreach (var restorePoint in backupJob.Repository.RestorePoints)
            {
                if (backupJob.RestorePointExpirationDate >= restorePoint.DateOfCreation)
                {
                    result.Add(restorePoint);
                }
            }

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