using System.Collections.Generic;

namespace IsuExtra.Entities
{
    public class OGNPCourse
    {
        public OGNPCourse(string name, MegaFaculty attachedFaculty)
        {
            Name = name;
            AttachedFaculty = attachedFaculty;
        }

        public string Name { get; set; }
        public MegaFaculty AttachedFaculty { get; set; }
        public List<OGNPStream> Streams { get; } = new List<OGNPStream>();
    }
}