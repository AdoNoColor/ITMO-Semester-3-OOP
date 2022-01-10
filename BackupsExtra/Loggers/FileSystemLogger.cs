using System;
using System.IO;
using BackupsExtra.Entities;

namespace BackupsExtra.Loggers
{
    public class FileSystemLogger : ILogger
    {
        public void BackupJobCreated(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                PrintMessage(backupJob, $"[{DateTime.Now}" + " Backup Job was created!]");
                return;
            }

            PrintMessage(backupJob, "[Backup Job was created!]");
        }

        public void RestorePointCreated(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                PrintMessage(backupJob, $"[{DateTime.Now}" + " Restore Point was created!]");
                return;
            }

            PrintMessage(backupJob, "[Restore Point was created!]");
        }

        public void JobObjectAdded(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                PrintMessage(backupJob, $"[{DateTime.Now}" + " Job Object Added!]");
                return;
            }

            PrintMessage(backupJob, "[Job Object Added!]");
        }

        public void JobObjectRemoved(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                PrintMessage(backupJob, $"[{DateTime.Now}" + " Job Object Removed!]");
                return;
            }

            PrintMessage(backupJob, "[Job Object Removed!]");
        }

        public void AlgorithmExecuted(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                PrintMessage(backupJob, $"[{DateTime.Now}" + $" Algorithm {backupJob.LimitBehaivor} Started to work!]");
                return;
            }

            PrintMessage(backupJob, $"[Algorithm {backupJob.LimitBehaivor} Started to work!]");
        }

        private void PrintMessage(BackupJobExtra backupJob, string message)
        {
            using (var sw = new StreamWriter(backupJob.LoggerStatePath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine(message);
            }
        }
    }
}