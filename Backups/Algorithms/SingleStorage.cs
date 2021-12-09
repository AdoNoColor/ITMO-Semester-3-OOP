using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Backups.Entities;

namespace Backups.Algorithms
{
    public class SingleStorage : IAlgorithm
    {
        public List<string> StartAlgorithm(BackupJob backupJob)
        {
            return backupJob.JobObjects.Select(storage => @"arc_" +
                                                          Convert.ToString(backupJob.Repository.RestorePoints.Count + 1)
                                                          + ".zip" + Path.DirectorySeparatorChar +
                                                          Path.GetFileName(storage)).ToList();
        }
    }
}