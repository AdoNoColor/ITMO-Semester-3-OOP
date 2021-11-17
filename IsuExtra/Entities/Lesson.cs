using System;

namespace IsuExtra.Entities
{
    public class Lesson
    {
        public Lesson()
        {
        }

        public Lesson(DateTime startTime, string group, int auditory, string mentor)
        {
            StartTime = startTime;
            Group = group;
            Auditory = auditory;
            Mentor = mentor;
        }

        public DateTime StartTime { get; }

        public string Group { get; }

        public int Auditory { get; }

        public string Mentor { get; }
    }
}