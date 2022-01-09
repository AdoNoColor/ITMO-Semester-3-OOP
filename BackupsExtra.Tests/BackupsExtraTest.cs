using System;
using Backups.Algorithms;
using Backups.Repositories;
using BackupsExtra.Entities;
using BackupsExtra.LimitAlgorithms.CleanAlgorithm;
using BackupsExtra.MergeAlgorithm;
using NUnit.Framework;

namespace BackupsExtra.Tests
{
    public class BackupsExtraTest
    {
        [Test]
        public void CleanByAmount()
        {
            var rep = new GitRepository();
            var job = new BackupJobExtra(rep, new SingleStorage(), new CleanByAmount());
            job.LimitBehaivor = LimitBehaivor.DeletePoints;
            job.AddObject(@"123.txt");
            job.AddObject(@"321.txt");
            job.Execute();
            job.DeleteObject(@"321.txt");
            job.Execute();
            job.AddObject(@"321123.txt");
            job.Execute();
            Assert.AreEqual(2, job.Repository.RestorePoints.Count);
        }
        
        [Test]
        public void CleanByDate()
        {
            var rep2 = new GitRepository();
            var job2 = new BackupJobExtra(rep2, new SingleStorage(), new CleanByDate());
            job2.AddObject(@"123.txt");
            job2.AddObject(@"321.txt");
            job2.Execute(DateTime.Now.AddMonths(7));
            job2.DeleteObject(@"321.txt");
            job2.Execute(DateTime.Now.AddMonths(7));
            job2.AddObject(@"31231.txt");
            job2.Execute(DateTime.Now.AddMonths(5));
            Assert.AreEqual(2, job2.Repository.RestorePoints.Count);
        }

        [Test]
        public void Merge()
        {
            var rep = new GitRepository();
            var job = new BackupJobExtra(rep, new SplitStorage(), new CleanByDate());
            job.LimitBehaivor = LimitBehaivor.MergePoints;
            job.AddObject(@"123.txt");
            job.AddObject(@"321.txt");
            job.Execute(DateTime.Now.AddMonths(6));
            job.DeleteObject(@"321.txt");
            job.Execute(DateTime.Now.AddMonths(5));
            Assert.AreEqual(1, job.Repository.RestorePoints.Count);
        }

        [Test]
        public void HybridCleanAlgorithm()
        {
            var rep = new GitRepository();
            var job = new BackupJobExtra(rep, new SplitStorage(), new CleanHybrid());
            job.LimitBehaivor = LimitBehaivor.MergePoints;
            job.HybridOption = HybridOption.One;
            job.AddObject(@"123.txt");
            job.AddObject(@"321.txt");
            job.Execute(DateTime.Now.AddMonths(6));
            job.DeleteObject(@"321.txt");
            job.Execute(DateTime.Now.AddMonths(5));
            Assert.AreEqual(2, job.Repository.RestorePoints.Count);
        }
    }
}