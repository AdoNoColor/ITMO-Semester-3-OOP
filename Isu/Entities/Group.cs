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
            Course = Convert.ToInt32(groupName[2]);
        }

        public int MaxStudents { get; private set; }
        public List<Student> Students { get; }
        public string GroupName { get; }
        public CourseNumber Course { get; }

        private bool IsGroupNameCorrect(string name)
        {
            if (name?.Length != 5) return false;
            return name[0] == 'M' && name[1] == '3' && int.Parse(name[2].ToString()) >= 1 && int.Parse(name[2].ToString()) <= 4 && int.Parse(name[3].ToString()) >= 0 && int.Parse(name[3].ToString()) <= 9 && int.Parse(name[4].ToString()) >= 0 && int.Parse(name[4].ToString()) <= 9 && name[3..] != "00";
        }
    }
}
