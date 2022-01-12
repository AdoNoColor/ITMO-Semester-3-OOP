using System;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Repositories;
using BackupsExtra.LimitAlgorithms;
using BackupsExtra.LimitAlgorithms.CleanAlgorithm;
using BackupsExtra.LimitAlgorithms.MergeAlgorithm;
using BackupsExtra.Loggers;
using BackupsExtra.MergeAlgorithm;
using BackupsExtra.RestoreAlgorithms;

namespace BackupsExtra.Entities
{
    public class BackupJobExtra : BackupJob
    {
        public BackupJobExtra(IRepository rep, IAlgorithm usingAlgorithm, ICleanAlgorithm usingCleanAlgorithm, ILogger logger)
            : base(rep, usingAlgorithm)
        {
            CleanAlgorithm = usingCleanAlgorithm;
            Logger = logger;
        }

        public ICleanAlgorithm CleanAlgorithm { get; set; }
        public DateTime RestorePointExpirationDate { get; set; } = DateTime.Now.AddMonths(6);
        public int RestorePointMaxAmount { get; set; } = 2;
        public Merge MergeAlgorithm { get; set; }
        public LimitBehaivor LimitBehaivor { get; set; } = LimitBehaivor.DeletePoints;
        public LimitAlgorithm LimitAlgorithm { get; set; } = new LimitAlgorithm();

        public TimeViaLogger TimeViaLogger { get; set; } = TimeViaLogger.No;
        public string LoggerStatePath { get; set; }

        public ILogger Logger { get; }

        public override void AddObject(string objectName)
        {
            JobObjects.Add(objectName);
            Logger.JobObjectAdded(this);
        }

        public override void DeleteObject(string objectName)
        {
            JobObjects.Remove(objectName);
            Logger.JobObjectRemoved(this);
        }

        public override void Execute()
        {
            Repository.CreateRestorePoint(this);
            Logger.RestorePointCreated(this);
            LimitAlgorithm.Execute(this);
        }

        public void Execute(DateTime timeOfCreation, Merge merge)
        {
            Repository.CreateRestorePoint(this, timeOfCreation);
            Logger.RestorePointCreated(this);
            LimitAlgorithm.Execute(this, merge);
        }

        public void Execute(DateTime timeOfCreation)
        {
            Repository.CreateRestorePoint(this, timeOfCreation);
            Logger.RestorePointCreated(this);
            LimitAlgorithm.Execute(this);
        }

        public void RestorePointToOriginalLocation(RestorePoint restorePoint)
        {
            var alg = new FileSystemRestoreAlgorithm();
            alg.RestoreToOriginalLocation(this, restorePoint);
        }

        public void RestorePointToDifferentLocation(RestorePoint restorePoint, string destination)
        {
            var alg = new FileSystemRestoreAlgorithm();
            alg.RestoreToDifferentLocation(this, restorePoint, destination);
        }
    }
}