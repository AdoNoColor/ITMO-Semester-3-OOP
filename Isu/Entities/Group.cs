using System.Collections.Generic;
using Isu.Tools;

namespace Isu.Entities
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

        public int MaxStudents { get; private set; }
        public List<Student> Students { get; }
        public string GroupName { get; }
        public CourseNumber Course { get; }

        private bool IsGroupNameCorrect(string name)
        {
            if (name.Length != 5) throw new IsuException("Name of the group does not follow certain rules. TIP: M3XYY");
            return name[0] == 'M' && name[1] == '3' && (int)name[2] > 0 && (int)name[2] < 5 && (int)name[3] >= 0 && (int)name[3] <= 9 &&
                   (int)name[4] >= 0 && (int)name[4] <= 9;
        }
    }
}
