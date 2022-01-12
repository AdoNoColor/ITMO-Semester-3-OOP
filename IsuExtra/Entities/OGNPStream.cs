using System.Collections.Generic;
using System.Reflection.Metadata;
using Isu.Entities;

namespace IsuExtra.Entities
{
    public class OGNPStream
    {
        public OGNPStream(string name, OGNPCourse attachedOgnpCourse, CourseNumber courseNumber)
        {
            Name = name;
            CourseNumber = courseNumber;
            AttachedOGNPCourse = attachedOgnpCourse;
        }

        public OGNPCourse AttachedOGNPCourse { get; set; }
        public string Name { get; }
        public CourseNumber CourseNumber { get; set; }
        public List<OGNPGroup> OGNPGroups { get; set; } = new List<OGNPGroup>();
    }
}