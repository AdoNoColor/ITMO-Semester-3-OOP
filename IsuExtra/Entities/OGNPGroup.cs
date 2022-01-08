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
        public List<Student> Students { get; private set; } = new List<Student>();

        public void AddStudent(StudentExtra student)
        {
            if (Students.Any(anotherStudent => anotherStudent == student))
            {
                throw new IsuExtraException("Student like this exists!");
            }

            Students.Add(student);
            student.OgnpSigned.Add(this);
        }

        public void RemoveStudent(StudentExtra student)
        {
            if (Students.All(anotherStudent => anotherStudent != student))
                throw new IsuExtraException("Student like this doesn't exist!");
            Students.Remove(student);
            student.OgnpSigned.Remove(this);
        }
    }
}