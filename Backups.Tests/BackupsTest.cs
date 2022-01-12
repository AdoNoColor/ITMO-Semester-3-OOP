using System.Collections.Generic;
using System.IO;
using Backups.Algorithms;
using Backups.Entities;
using Backups.Repositories;
using NUnit.Framework;

namespace Backups.Tests
{
    public class BackupsTest
    {
        [Test]
        public void SingleStorageAlgorithm()
        {
            var rep = new GitRepository();
            var backupJob = new BackupJob(rep, new SingleStorage());
            backupJob.AddObject(@"123.txt");
            backupJob.AddObject(@"321.txt");
            backupJob.Execute();
            Assert.AreEqual(2, backupJob.JobObjects.Count);
            Assert.AreEqual(1, rep.RestorePoints.Count);
            backupJob.DeleteObject(@"321.txt");
            backupJob.Execute();
            Assert.AreEqual(1, backupJob.JobObjects.Count);
            Assert.AreEqual(2, rep.RestorePoints.Count);
            
            var expectedResult = new List<string>
            {
                @"arc_1.zip" + Path.DirectorySeparatorChar + "123.txt",
                @"arc_1.zip" + Path.DirectorySeparatorChar + "321.txt"
            };
            Assert.AreEqual(expectedResult, rep.RestorePoints[0].Storages);
            
            expectedResult.Clear();
            expectedResult.Add(@"arc_2.zip" + Path.DirectorySeparatorChar + "123.txt");
            Assert.AreEqual(expectedResult, rep.RestorePoints[1].Storages);
        }

        [Test]
        public void SplitStorageAlgorithm()
        {
            var rep = new GitRepository();
            var backupJob = new BackupJob(rep, new SplitStorage());
            backupJob.AddObject(@"123.txt");
            backupJob.AddObject(@"321.txt");
            backupJob.Execute();
            Assert.AreEqual(2, backupJob.JobObjects.Count);
            Assert.AreEqual(1, rep.RestorePoints.Count);
            backupJob.DeleteObject(@"321.txt");
            backupJob.Execute();
            Assert.AreEqual(1, backupJob.JobObjects.Count);
            Assert.AreEqual(2, rep.RestorePoints.Count);
            
            var expectedResult = new List<string>
            {
                @"123.txt_1.zip" + Path.DirectorySeparatorChar + "123.txt",
                @"321.txt_1.zip" + Path.DirectorySeparatorChar + "321.txt"
            };
            Assert.AreEqual(expectedResult, rep.RestorePoints[0].Storages);
            
            expectedResult.Clear();
            expectedResult.Add(@"123.txt_2.zip" + Path.DirectorySeparatorChar + "123.txt");
            Assert.AreEqual(expectedResult, rep.RestorePoints[1].Storages);
        }
    }
}