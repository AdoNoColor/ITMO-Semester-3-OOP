using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;

namespace Backups.Repositories
{
    public class FileSystemRepository : IRepository
    {
        public List<RestorePoint> RestorePoints { get; } = new List<RestorePoint>();
        public void CreateRestorePoint(BackupJob backupJob)
        {
            var restorePoint = new RestorePoint(backupJob);
            string destination = @"C:\Users\Иванов\dir";

            for (int i = 0; i < backupJob.JobObjects.Count; i++)
            {
                Directory.CreateDirectory(destination);
                File.Copy(
                    backupJob.JobObjects[i],
                    destination + Path.DirectorySeparatorChar + Path.GetFileName(backupJob.JobObjects[i]));

                string storageName = @"C:\Users\Иванов" + Path.DirectorySeparatorChar +
                                     Path.GetFileName(Path.GetDirectoryName(restorePoint.Storages[i]));

                try
                {
                    ZipFile.CreateFromDirectory(destination, storageName);
                }
                catch
                {
                    using ZipArchive archive = ZipFile.Open(storageName, ZipArchiveMode.Update);
                    archive.CreateEntryFromFile(
                        backupJob.JobObjects[i],
                        Path.GetFileName(restorePoint.Storages[i]));
                }

                restorePoint.Storages[i] = @"C:\Users\Иванов" + Path.DirectorySeparatorChar + restorePoint.Storages[i];

                Directory.Delete(destination, true);
            }
        }

        public void CreateRestorePoint(BackupJob backupJob, DateTime dateTime)
        {
            var restorePoint = new RestorePoint(backupJob, dateTime);
            string destination = @"C:\Users\Иванов\dir";

            for (int i = 0; i < backupJob.JobObjects.Count; i++)
            {
                Directory.CreateDirectory(destination);
                File.Copy(
                    backupJob.JobObjects[i],
                    destination + Path.DirectorySeparatorChar + Path.GetFileName(backupJob.JobObjects[i]));

                string storageName = @"C:\Users\Иванов" + Path.DirectorySeparatorChar +
                                     Path.GetFileName(Path.GetDirectoryName(restorePoint.Storages[i]));

                try
                {
                    ZipFile.CreateFromDirectory(destination, storageName);
                }
                catch
                {
                    using ZipArchive archive = ZipFile.Open(storageName, ZipArchiveMode.Update);
                    archive.CreateEntryFromFile(
                        backupJob.JobObjects[i],
                        Path.GetFileName(restorePoint.Storages[i]));
                }

                restorePoint.Storages[i] = @"C:\Users\Иванов" + Path.DirectorySeparatorChar + restorePoint.Storages[i];

                Directory.Delete(destination, true);
            }
        }
    }
}