using System.IO;
using System.IO.Compression;
using Backups.Entities;
using Backups.Repositories;
using BackupsExtra.Tools;

namespace BackupsExtra.RestoreAlgorithms
{
    public class FileSystemRestoreAlgorithm
    {
        public void RestoreToOriginalLocation(BackupJob backupJob, RestorePoint restorePoint)
        {
            if (!(backupJob.Repository is FileSystemRepository))
                throw new BackupsExtraException("Not the type of Repository needed for this action!");
            if (!backupJob.Repository.RestorePoints.Contains(restorePoint))
                throw new BackupsExtraException("Nothing to restore!");
            if (restorePoint.OriginalLocation.Count == 0)
                throw new BackupsExtraException("No original locations were found!");
            for (int i = 0; i < restorePoint.Storages.Count; i++)
            {
                ZipFile.ExtractToDirectory(
                    Path.GetDirectoryName(restorePoint.Storages[i]),
                    Path.GetDirectoryName(restorePoint.OriginalLocation[i]));
            }
        }

        public void RestoreToDifferentLocation(BackupJob backupJob, RestorePoint restorePoint, string destination)
        {
            if (!(backupJob.Repository is FileSystemRepository))
                throw new BackupsExtraException("Not the type of Repository needed for this action!");
            if (!backupJob.Repository.RestorePoints.Contains(restorePoint))
                throw new BackupsExtraException("Nothing to restore!");

            foreach (string storage in restorePoint.Storages)
            {
                ZipFile.ExtractToDirectory(Path.GetDirectoryName(storage), destination);
            }
        }
    }
}