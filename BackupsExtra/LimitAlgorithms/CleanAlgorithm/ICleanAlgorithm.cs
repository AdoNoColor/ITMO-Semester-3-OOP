using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Entities;

namespace BackupsExtra.LimitAlgorithms.CleanAlgorithm
{
    public interface ICleanAlgorithm
    {
        List<RestorePoint> FindPoints(BackupJobExtra backupJob);
        void StartAlgorithm(BackupJobExtra backupJobExtra, List<RestorePoint> foundPoints);
    }
}