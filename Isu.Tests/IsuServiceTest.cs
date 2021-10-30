using System;
using Isu.Services;
using Isu.Tools;
using Isu.Entities;
using NUnit.Framework;

namespace Isu.Tests
{
    public class Tests
    {
        private IIsuService _isuService;

        [SetUp]
        public void Setup()
        {
            //TODO: implement
            _isuService = new IsuService();
        }

        [Test]
        public void AddStudentToGroup_StudentHasGroupAndGroupContainsStudent()
        {
            Group group = _isuService.AddGroup("M3208", 30);
            Student student = _isuService.AddStudent(group, "Maxim Ivanov");
            Assert.Contains(student, group.Students);
        }

        [Test]
        public void ReachMaxStudentPerGroup_ThrowException()
        {
            Group group = _isuService.AddGroup("M3208", 30);
            for (int i = 0; i < group.MaxStudents; i++)
            {
                _isuService.AddStudent(group, "Steve Austin");
            }

            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddStudent(group, "Steve Jobs");
            });
        }

        [Test]
        public void CreateGroupWithInvalidName_ThrowException()
        {
            Assert.Catch<IsuException>(() =>
            {
                _isuService.AddGroup("P2104", 30);
            });
        }

        [Test]
        public void TransferStudentToAnotherGroup_GroupChanged()
        {
            Group group = _isuService.AddGroup("M3208", 30);
            Student student = _isuService.AddStudent(group, "Max Ivanov");
            Group newGroup = _isuService.AddGroup("M3202", 30);
            _isuService.ChangeStudentGroup(student, newGroup);
            Assert.IsTrue(!group.Students.Contains(student) && newGroup.Students.Contains(student));
        }
    }
}