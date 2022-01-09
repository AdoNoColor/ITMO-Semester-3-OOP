using System;
using System.IO;
using BackupsExtra.Entities;
using BackupsExtra.MergeAlgorithm;
using BackupsExtra.Tools;

namespace BackupsExtra.Loggers
{
    public class FileSystemLogger : ILogger
    {
        public void BackupJobCreated(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                using var sw = new StreamWriter(backupJob.LoggerStatePath, true, System.Text.Encoding.Default);
                sw.WriteLine($"{DateTime.Now}");
            }

            using (var sw = new StreamWriter(backupJob.LoggerStatePath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine($"Backup Job Created!");
            }
        }

        public void RestorePointCreated(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                using var sw = new StreamWriter(backupJob.LoggerStatePath, true, System.Text.Encoding.Default);
                sw.WriteLine($"{DateTime.Now}");
            }

            using (var sw = new StreamWriter(backupJob.LoggerStatePath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine($"Restore Point was Created!");
            }
        }

        public void JobObjectAdded(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                using var sw = new StreamWriter(backupJob.LoggerStatePath, true, System.Text.Encoding.Default);
                sw.WriteLine($"{DateTime.Now}");
            }

            Console.WriteLine("Job Object Added!");
        }

        public void JobObjectRemoved(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                using var sw = new StreamWriter(backupJob.LoggerStatePath, true, System.Text.Encoding.Default);
                sw.WriteLine($"{DateTime.Now}");
            }

            using (var sw = new StreamWriter(backupJob.LoggerStatePath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("Job Object Removed!");
            }
        }

        public void AlgorithmExecuted(BackupJobExtra backupJob)
        {
            if (backupJob.TimeViaLogger == (TimeViaLogger)1)
            {
                using var sw = new StreamWriter(backupJob.LoggerStatePath, true, System.Text.Encoding.Default);
                sw.WriteLine($"{DateTime.Now}");
            }

            using (var sw = new StreamWriter(backupJob.LoggerStatePath, true, System.Text.Encoding.Default))
            {
                sw.WriteLine("Algorithm Started to work!");
            }

            switch (backupJob.LimitBehaivor)
            {
                case LimitBehaivor.MergePoints:
                {
                    using var sw = new StreamWriter(backupJob.LoggerStatePath, true, System.Text.Encoding.Default);
                    sw.WriteLine("Type of behaivor: Merge Algorithm");

                    break;
                }

                case LimitBehaivor.DeletePoints:
                {
                    using var sw = new StreamWriter(backupJob.LoggerStatePath, true, System.Text.Encoding.Default);
                    sw.WriteLine("Type of behaivor: Clean Algorithm");

                    break;
                }

                default:
                    throw new BackupsExtraException("Incorrect input!");
            }
        }
    }
}