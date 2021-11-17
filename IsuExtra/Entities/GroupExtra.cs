using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class GroupExtra : Group
    {
        public GroupExtra(string groupName, int maxStudents)
            : base(groupName, maxStudents)
        {
            Students = new List<StudentExtra>();
            MegaFaculty = MegaFacultyIdentifier();
        }

        public MegaFaculty MegaFaculty { get; }

        public new List<StudentExtra> Students { get; }

        private MegaFaculty MegaFacultyIdentifier()
        {
            if (GroupName[0] == 'M')
            {
                return MegaFaculty.TINT;
            }

            if (GroupName[0] == 'B')
            {
                return MegaFaculty.MF;
            }

            if (GroupName[0] == 'C')
            {
                return MegaFaculty.KTY;
            }

            if (GroupName[0] == 'D')
            {
                return MegaFaculty.BTINS;
            }

            if (GroupName[0] == 'Y')
            {
                return MegaFaculty.FTMI;
            }

            throw new IsuExtraException("No MegaFaculty like this!");
        }

        private bool IsGroupNameCorrect(string name)
        {
            if (name?.Length != 5) return false;
            return Enumerable.Range('A', 'Z').Contains(name[0]) && name[1] == '3' && int.Parse(name[2].ToString()) >= 1 &&
                   int.Parse(name[2].ToString()) <= 4 && int.Parse(name[3].ToString()) >= 0 &&
                   int.Parse(name[3].ToString()) <= 9 && int.Parse(name[4].ToString()) >= 0 &&
                   int.Parse(name[4].ToString()) <= 9 && name[3..] != "00";
        }
    }
}