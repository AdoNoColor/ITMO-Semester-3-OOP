using System;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        public Lesson(DateTime startTime, string groupName, int auditory, string mentor)
        {
            StartTime = startTime;
            GroupName = groupName;
            Auditory = auditory;
            Mentor = mentor;
        }

        public DateTime StartTime { get; }

        public string GroupName { get; }

        public int Auditory { get; }

        public string Mentor { get; }
    }
}