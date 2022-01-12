using System.Collections.Generic;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class StudentExtra : Student
    {
        private static int _id = 0;

        public StudentExtra(string name)
            : base(name)
        {
            _id++;
            Name = name;
            Id = _id;
        }

        public StudentExtra(string name, GroupExtra groupName)
            : base(name, groupName)
        {
            _id++;
            GroupName = groupName;
            Name = name;
            Id = _id;
            Course = groupName.Course;
        }

        public new int Id { get; }
        public new string Name { get; }
        public new GroupExtra GroupName { get; set; }

        public new CourseNumber Course { get; set; }

        public List<OGNPGroup> OgnpSigned { get; set; } = new List<OGNPGroup>();
    }
}