using BackupsExtra.Entities;

namespace BackupsExtra.LimitAlgorithms
{
    public interface ILimitAlgorithm
    {
        void Execute(BackupJobExtra backupJob);
    }
}