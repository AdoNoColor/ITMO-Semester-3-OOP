using BackupsExtra.Entities;
using BackupsExtra.LimitAlgorithms.MergeAlgorithm;
using BackupsExtra.MergeAlgorithm;

namespace BackupsExtra.LimitAlgorithms
{
    public class LimitAlgorithm
    {
        public void Execute(BackupJobExtra backupJob)
        {
            switch (backupJob.LimitBehaivor)
            {
                case LimitBehaivor.DeletePoints:
                {
                    if (backupJob.CleanAlgorithm.FindPoints(backupJob).Count != 0)
                    {
                        backupJob.CleanAlgorithm.StartAlgorithm(backupJob, backupJob.CleanAlgorithm.FindPoints(backupJob));
                        backupJob.Logger.AlgorithmExecuted(backupJob);
                    }

                    break;
                }

                case LimitBehaivor.MergePoints:
                {
                    if (backupJob.CleanAlgorithm.FindPoints(backupJob).Count != 0)
                    {
                        var merge = new Merge();
                        merge.StartAlgorithm(backupJob, backupJob.CleanAlgorithm.FindPoints(backupJob));
                        backupJob.Logger.AlgorithmExecuted(backupJob);
                    }

                    break;
                }
            }
        }
    }
}