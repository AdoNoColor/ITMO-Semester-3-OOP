using System.Collections.Generic;
using Backups.Entities;

namespace Backups.Algorithms
{
    public interface IAlgorithm
    {
        public List<string> StartAlgorithm(BackupJob backupJob);
    }
}