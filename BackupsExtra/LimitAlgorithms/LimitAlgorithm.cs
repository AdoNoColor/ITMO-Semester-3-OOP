using BackupsExtra.Entities;
using BackupsExtra.LimitAlgorithms.MergeAlgorithm;
using BackupsExtra.MergeAlgorithm;

namespace BackupsExtra.LimitAlgorithms
{
    public class LimitAlgorithm
    {
        public void Execute(BackupJobExtra backupJob)
        {
            backupJob.LimitBehaivor = LimitBehaivor.DeletePoints;
            if (backupJob.CleanAlgorithm.FindPoints(backupJob).Count != 0)
            {
                backupJob.CleanAlgorithm.StartAlgorithm(backupJob, backupJob.CleanAlgorithm.FindPoints(backupJob));
                backupJob.Logger.AlgorithmExecuted(backupJob);
            }
        }

        public void Execute(BackupJobExtra backupJob, Merge merge)
        {
            backupJob.LimitBehaivor = LimitBehaivor.MergePoints;
            if (backupJob.CleanAlgorithm.FindPoints(backupJob).Count != 0)
            {
                merge.StartAlgorithm(backupJob, backupJob.CleanAlgorithm.FindPoints(backupJob));
                backupJob.Logger.AlgorithmExecuted(backupJob);
            }
        }
    }
}