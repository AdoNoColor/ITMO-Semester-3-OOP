using System;
using System.Collections.Generic;
using System.Linq;
using IsuExtra.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class LessonService
    {
        public List<Lesson> AllLessons { get; } = new List<Lesson>();

        public void AddLesson(DateTime startTime, string group, int auditory, string mentor)
        {
            var lesson = new Lesson(startTime, group, auditory, mentor);

            if (AllLessons.Any(anotherLesson => anotherLesson == lesson))
            {
                throw new IsuExtraException("Lesson like this exists!");
            }

            AllLessons.Add(lesson);
        }

        public void RemoveLesson(DateTime startTime, string groupName, int auditory, string mentor)
        {
            var lesson = new Lesson(startTime, groupName, auditory, mentor);

            if (AllLessons.Any(anotherLesson => anotherLesson == lesson))
            {
                AllLessons.Remove(lesson);
            }

            throw new IsuExtraException("No lesson to remove!");
        }

        public bool LessonCollision(string groupNameOne, string groupNameTwo)
        {
            Lesson lessonOne = null;
            Lesson lessonTwo = null;

            foreach (Lesson lesson in AllLessons)
            {
                if (lesson.GroupName == groupNameOne)
                    lessonOne = lesson;

                if (lesson.GroupName == groupNameTwo)
                    lessonTwo = lesson;

                if (lessonOne != null && lessonTwo != null && lessonOne.StartTime == lessonTwo.StartTime)
                    return false;
            }

            return true;
        }
    }
}