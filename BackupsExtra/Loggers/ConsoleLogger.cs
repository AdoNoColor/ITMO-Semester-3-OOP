using System;
using BackupsExtra.Entities;
using BackupsExtra.MergeAlgorithm;
using BackupsExtra.Tools;

namespace BackupsExtra.Loggers
{
    public class ConsoleLogger : ILogger
    {
        public void BackupJobCreated(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                PrintMessage($"[{DateTime.Now}" + " Backup Job was created!]");
                return;
            }

            PrintMessage("[Backup Job was created!]");
        }

        public void RestorePointCreated(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                PrintMessage($"[{DateTime.Now}" + " Restore Point was created!]");
                return;
            }

            PrintMessage("[Restore Point was created!]");
        }

        public void JobObjectAdded(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                PrintMessage($"[{DateTime.Now}" + " Job Object Added!]");
                return;
            }

            PrintMessage("[Job Object Added!]");
        }

        public void JobObjectRemoved(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                PrintMessage($"[{DateTime.Now}" + " Job Object Removed!]");
                return;
            }

            PrintMessage("[Job Object Removed!]");
        }

        public void AlgorithmExecuted(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                PrintMessage($"[{DateTime.Now}" + $" Algorithm {backupJob.LimitBehaivor} Started to work!]");
                return;
            }

            PrintMessage($"[Algorithm {backupJob.LimitBehaivor} Started to work!]");
        }

        private void PrintMessage(string message)
        {
            Console.WriteLine(message);
        }
    }
}