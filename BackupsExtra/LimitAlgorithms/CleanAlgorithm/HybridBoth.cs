using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using BackupsExtra.Entities;

namespace BackupsExtra.LimitAlgorithms.CleanAlgorithm
{
    public class HybridBoth : ICleanAlgorithm
    {
        public List<RestorePoint> FindPoints(BackupJobExtra backupJob)
        {
            var amount = new CleanByAmount();
            var date = new CleanByDate();
            var result = new List<RestorePoint>();
            List<RestorePoint> amountList = amount.FindPoints(backupJob);

            List<RestorePoint> dateList = date.FindPoints(backupJob);
            foreach (RestorePoint restorePoint in amountList)
            {
                result.AddRange(dateList.Where(anotherRestorePoint => restorePoint == anotherRestorePoint));
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