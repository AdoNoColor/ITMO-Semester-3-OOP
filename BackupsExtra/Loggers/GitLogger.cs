using System;
using BackupsExtra.Entities;
using BackupsExtra.MergeAlgorithm;
using BackupsExtra.Tools;

namespace BackupsExtra.Loggers
{
    public class GitLogger : ILogger
    {
        public void BackupJobCreated(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
                Console.WriteLine($"{DateTime.Now}");
            Console.WriteLine("Backup Job was created!");
        }

        public void RestorePointCreated(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
                Console.WriteLine($"{DateTime.Now}");
            Console.WriteLine("Restore Point was created!");
        }

        public void JobObjectAdded(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
                Console.WriteLine($"{DateTime.Now}");
            Console.WriteLine("Job Object Added!");
        }

        public void JobObjectRemoved(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
                Console.WriteLine($"{DateTime.Now}");
            Console.WriteLine("Job Object Removed!");
        }

        public void AlgorithmExecuted(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
                Console.WriteLine($"{DateTime.Now}");
            Console.WriteLine("Algorithm started to work!");
            switch (backupJob.LimitBehaivor)
            {
                case LimitBehaivor.MergePoints:
                {
                    Console.WriteLine("Type of behaivor: Merge Algorithm");
                    break;
                }

                case LimitBehaivor.DeletePoints:
                {
                    Console.WriteLine("Type of behaivor: Clean Algorithm");
                    break;
                }

                default:
                    throw new BackupsExtraException("Incorrect input!");
            }
        }
    }
}