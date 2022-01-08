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

        public void RemoveLesson(DateTime startTime, string group, int auditory, string mentor)
        {
            var lesson = new Lesson(startTime, group, auditory, mentor);

            if (AllLessons.Any(anotherLesson => anotherLesson == lesson))
            {
                AllLessons.Remove(lesson);
            }

            throw new IsuExtraException("No lesson to remove!");
        }

        public bool DoTheyMatch(string nameOne, string nameTwo)
        {
            Lesson lessonOne = null;
            Lesson lessonTwo = null;

            foreach (Lesson lesson in AllLessons.Where(lesson => nameOne == lesson.Group))
            {
                lessonOne = lesson;
            }

            foreach (Lesson lesson in AllLessons.Where(lesson => nameTwo == lesson.Group))
            {
                lessonTwo = lesson;
            }

            return lessonOne != null && lessonTwo != null && lessonOne.StartTime != lessonTwo.StartTime;
        }
    }
}