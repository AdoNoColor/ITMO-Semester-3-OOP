using System;
using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Classes
{
    public class Group
    {
        public Group(string groupName, int maxStudents)
        {
            if (!IsGroupNameCorrect(groupName))
            {
                throw new IsuException("Invalid name");
            }

            MaxStudents = maxStudents;
            Students = new List<Student>();
            GroupName = groupName;
            Course = (CourseNumber)groupName[2];
        }

        public static int MaxStudents { get; set; }
        public List<Student> Students { get; }
        public string GroupName { get; }
        public CourseNumber Course { get; }

        private bool IsGroupNameCorrect(string name)
        {
            int groupNumber = int.Parse(name[3..]);
            return name[0] == 'M' && name[1] == '3' && name[2] > 0 && name[2] < 5 && groupNumber > 0 &&
                   groupNumber < 100;
        }
    }
}