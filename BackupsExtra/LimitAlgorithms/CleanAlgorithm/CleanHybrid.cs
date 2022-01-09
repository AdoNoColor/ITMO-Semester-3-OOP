using System;
using System.Collections.Generic;
using System.Linq;
using Backups.Entities;
using BackupsExtra.Entities;
using BackupsExtra.Tools;

namespace BackupsExtra.LimitAlgorithms.CleanAlgorithm
{
    public class CleanHybrid : ICleanAlgorithm
    {
        public List<RestorePoint> FindPoints(BackupJobExtra backupJob)
        {
            var amount = new CleanByAmount();
            var date = new CleanByDate();
            var result = new List<RestorePoint>();

            switch (backupJob.HybridOption)
            {
                case HybridOption.One:
                {
                    List<RestorePoint> amountList = amount.FindPoints(backupJob);
                    List<RestorePoint> dateList = date.FindPoints(backupJob);
                    foreach (RestorePoint restorePoint in amountList)
                    {
                        result.AddRange(dateList.Where(anotherRestorePoint => restorePoint == anotherRestorePoint));
                    }

                    return result;
                }

                case HybridOption.Both:
                {
                    List<RestorePoint> amountList = amount.FindPoints(backupJob);
                    List<RestorePoint> dateList = date.FindPoints(backupJob);
                    foreach (RestorePoint restorePoint in amountList)
                    {
                        result.AddRange(dateList.Where(anotherRestorePoint => restorePoint == anotherRestorePoint));
                    }

                    return result;
                }

                default:
                    throw new BackupsExtraException("Something ain't right!");
            }
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