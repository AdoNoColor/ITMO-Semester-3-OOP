using System.Collections.Generic;
using Backups.Entities;
using BackupsExtra.Entities;

namespace BackupsExtra.LimitAlgorithms.MergeAlgorithm
{
    public interface IMergeAlgorithm
    {
        void StartAlgorithm(BackupJobExtra backupJob, List<RestorePoint> outOfLimitPoints);
    }
}