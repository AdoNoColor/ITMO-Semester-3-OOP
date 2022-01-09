using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Algorithms;
using Backups.Entities;
using BackupsExtra.Entities;
using NUnit.Framework;

namespace BackupsExtra.LimitAlgorithms.MergeAlgorithm
{
    public class Merge : IMergeAlgorithm
    {
    public void StartAlgorithm(BackupJobExtra backupJob, List<RestorePoint> outOfLimitPoints)
        {
            if (backupJob.UsingAlgorithm is SingleStorage)
            {
                foreach (RestorePoint point in
                    outOfLimitPoints.Where(point => backupJob.Repository.RestorePoints.Contains(point)))
                {
                    backupJob.Repository.RestorePoints.Remove(point);
                }

                return;
            }

            for (int restorePointCount = 0; restorePointCount < outOfLimitPoints.Count; restorePointCount++)
            {
                RestorePoint restorePoint1 = backupJob.Repository.RestorePoints[0];
                RestorePoint restorePoint2 = backupJob.Repository.RestorePoints[1];
                var storages = new List<string>();

                foreach (string storage2 in restorePoint2.Storages)
                {
                    storages.AddRange(restorePoint1.Storages.Where(storage1 =>
                        Path.GetFileName(storage1) != Path.GetFileName(storage2) &&
                        !restorePoint2.Storages.Contains(Path.GetFileName(storage1))));
                }

                backupJob.Repository.RestorePoints.Remove(restorePoint1);
                foreach (string storage in storages)
                {
                    restorePoint2.Storages.Add(storage);
                }

                backupJob.Repository.RestorePoints.Remove(restorePoint1);
            }
        }
    }
}
