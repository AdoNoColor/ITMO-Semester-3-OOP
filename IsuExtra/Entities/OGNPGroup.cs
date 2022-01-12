using System;
using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Entities
{
    public class OGNPGroup
    {
        public OGNPGroup(string groupName, int maxStudents, OGNPStream attachedStream)
        {
            GroupName = groupName;
            MaxStudents = maxStudents;
            AttachedStream = attachedStream;
        }

        public OGNPStream AttachedStream { get; }
        public List<Lesson> Lessons { get; } = new List<Lesson>();
        public string GroupName { get; }
        public int MaxStudents { get; }
        public List<Student> Students { get; set; } = new List<Student>();
    }
}