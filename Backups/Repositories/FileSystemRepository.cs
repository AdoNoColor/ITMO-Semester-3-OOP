using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using Backups.Entities;

namespace Backups.Repositories
{
    public class FileSystemRepository : IRepository
    {
        public List<RestorePoint> RestorePoints { get; } = new List<RestorePoint>();
        public void CreateRestorePoint(BackupJob backupJob, string destination)
        {
            var restorePoint = new RestorePoint(backupJob);

            for (int i = 0; i < backupJob.JobObjects.Count; i++)
            {
                Directory.CreateDirectory(destination);
                File.Copy(
                    backupJob.JobObjects[i],
                    destination + Path.DirectorySeparatorChar + Path.GetFileName(backupJob.JobObjects[i]));

                string storageName = destination + Path.DirectorySeparatorChar +
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

                restorePoint.Storages[i] = destination + Path.DirectorySeparatorChar + restorePoint.Storages[i];

                Directory.Delete(destination, true);
            }
        }
    }
}