using BackupsExtra.Entities;

namespace BackupsExtra.Loggers
{
    public interface ILogger
    {
        void BackupJobCreated(BackupJobExtra backupJob);
        void RestorePointCreated(BackupJobExtra backupJobExtra);
        void JobObjectAdded(BackupJobExtra backupJob);
        void JobObjectRemoved(BackupJobExtra backupJob);

        void AlgorithmExecuted(BackupJobExtra backupJob);
    }
}