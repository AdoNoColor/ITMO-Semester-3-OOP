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
                RestorePoint restorePoint = backupJob.Repository.RestorePoints[0];
                RestorePoint anotherRestorePoint = backupJob.Repository.RestorePoints[1];
                var storages = new List<string>();

                foreach (string storage in anotherRestorePoint.Storages)
                {
                    storages.AddRange(restorePoint.Storages.Where(anotherStorage =>
                        Path.GetFileName(anotherStorage) != Path.GetFileName(storage) &&
                        !anotherRestorePoint.Storages.Contains(Path.GetFileName(anotherStorage))));
                }

                backupJob.Repository.RestorePoints.Remove(restorePoint);
                foreach (string storage in storages)
                {
                    anotherRestorePoint.Storages.Add(storage);
                }

                backupJob.Repository.RestorePoints.Remove(restorePoint);
            }
        }
    }
}
