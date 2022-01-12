using System.Collections.Generic;
using Isu.Tools;
using IsuExtra.Entities;

namespace IsuExtra.Services
{
    public class GroupService
    {
        private readonly List<GroupExtra> _allGroups = new List<GroupExtra>();
        private readonly List<StudentExtra> _allStudents = new List<StudentExtra>();

        public GroupExtra AddGroup(string name, int maxStudents)
        {
            var group = new GroupExtra(name, maxStudents);
            _allGroups.Add(group);
            return group;
        }

        public StudentExtra AddStudent(GroupExtra group, string name)
        {
            if (group == null)
            {
                throw new IsuException("There is no group like that");
            }

            if (group.MaxStudents <= group.Students.Count)
            {
                throw new IsuException($"{group.GroupName} is fully packed");
            }

            var student = new StudentExtra(name, group);
            group.Students.Add(student);
            _allStudents.Add(student);
            return student;
        }
    }
}