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
            var lessonOne = new Lesson();
            var lessonTwo = new Lesson();
            foreach (Lesson lesson in AllLessons)
            {
                if (nameOne == lesson.Group)
                    lessonOne = lesson;
                if (nameTwo == lesson.Group)
                    lessonTwo = lesson;
            }

            return lessonOne.StartTime != lessonTwo.StartTime;
        }
    }
}