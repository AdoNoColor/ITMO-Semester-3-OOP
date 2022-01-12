using System;
using Isu.Entities;
using IsuExtra.Entities;
using IsuExtra.Services;
using IsuExtra.Tools;
using NUnit.Framework;

namespace IsuExtra.Tests
{
    public class IsuExtraTest
    {
        private IsuExtraService _extraService;
        private GroupService _groupService;
        private LessonService _lessonService;
        private GroupExtra _m3208;
        private StudentExtra _max;
        private StudentExtra _martha;
        private StudentExtra _mark;


        [SetUp]
        public void Setup()
        {
            _extraService = new IsuExtraService();
            _groupService = new GroupService();
            _lessonService = new LessonService();
            _m3208 = _groupService.AddGroup("M3208", 30);
            _max = _groupService.AddStudent(_m3208,"Maxim");
            _martha = _groupService.AddStudent(_m3208, "Martha");
            _mark = _groupService.AddStudent(_m3208, "Mark");
        }

        [Test]
        public void AddOgnp()
        {
            OGNPCourse someLinuxStuff = _extraService.AddOgnpCourse("Some Linux Stuff", MegaFaculty.TINT);
            OGNPStream someLinuxStuffCourseDes = _extraService.AddOgnpStream("Some Linux Stuff Course Two!",
                someLinuxStuff, CourseNumber.Second);
            OGNPGroup someLinuxStuffGroup = _extraService.AddOgnpGroup("Linuxioids 2.1",
                30, someLinuxStuffCourseDes);
            OGNPGroup someLinuxStuffGroupTwo = _extraService.AddOgnpGroup("Linuxioids 2.2", 
                30, someLinuxStuffCourseDes);
            
            Assert.Catch<IsuExtraException>(() =>
            {
                _extraService.AddOgnpGroup("Linuxioids 2.2", 30, someLinuxStuffCourseDes);
            });
            Assert.AreEqual(2, someLinuxStuffCourseDes.OGNPGroups.Count);
            Assert.Contains(someLinuxStuffGroup, someLinuxStuffCourseDes.OGNPGroups);
            Assert.Contains(someLinuxStuffGroupTwo, someLinuxStuffCourseDes.OGNPGroups);
        }
        
        [Test]
        public void SignStudent()
        {
            OGNPCourse someBusinessStuff = _extraService.AddOgnpCourse("Business Stuff", MegaFaculty.BTINS);
            OGNPStream someBusinessStuffCourseDes = _extraService.AddOgnpStream("Course Two!", someBusinessStuff,
                CourseNumber.Second);
            OGNPGroup someBusinessStuffGroup = _extraService.AddOgnpGroup("Jay Z 2.1", 
                30, someBusinessStuffCourseDes);
            OGNPGroup anotherBusinessStuffGroup = _extraService.AddOgnpGroup("Jay Z 2.2", 
                30, someBusinessStuffCourseDes);
            OGNPStream someBusinessStuffCourseTres = _extraService.AddOgnpStream("Course Three!",
                someBusinessStuff, CourseNumber.Third);
            OGNPGroup someBusinessStuffGroupTwo = _extraService.AddOgnpGroup("Jay Z 3.1", 
                30, someBusinessStuffCourseTres);
            _extraService.SignOne(_max, someBusinessStuffGroup, _lessonService);

            var timeOne = new DateTime(2021, 10, 24, 8, 20, 0);
            var timeTwo = new DateTime(2021, 10, 24, 10, 0, 0);
            _lessonService.AddLesson(timeOne, _m3208.GroupName, 412, "Ye");
            _lessonService.AddLesson(timeTwo, someBusinessStuffGroup.GroupName, 123, "Obama");
            _lessonService.AddLesson(timeTwo, anotherBusinessStuffGroup.GroupName, 123, "Mister Hendrix");

            Assert.Contains(_max, someBusinessStuffGroup.Students);
            Assert.Catch<IsuExtraException>(() =>
            {
                _extraService.SignOne(_max, someBusinessStuffGroupTwo, _lessonService);
            });
            
            Assert.Catch<IsuExtraException>(() =>
            {
                _extraService.SignOne(_martha, anotherBusinessStuffGroup, _lessonService);
            });
            
            Assert.AreEqual(CourseNumber.Second, someBusinessStuffCourseDes.CourseNumber);
        }

        [Test]
        public void RemoveStudent()
        {
            OGNPCourse someBusinessStuff = _extraService.AddOgnpCourse("B-Stuff Stuff", MegaFaculty.FTMI);
            OGNPStream someBusinessStuffCourseDes = _extraService.AddOgnpStream("B-Stuff Course Two!", someBusinessStuff,
                CourseNumber.Second);
            OGNPGroup someBusinessStuffGroup = _extraService.AddOgnpGroup("Jay Z 2.1",
                30, someBusinessStuffCourseDes);
            
            _extraService.SignOne(_max, someBusinessStuffGroup, _lessonService);
            _extraService.SignOne(_martha, someBusinessStuffGroup, _lessonService);

            _extraService.RemoveOne(someBusinessStuffGroup, _max);
            Assert.AreEqual(1, someBusinessStuffGroup.Students.Count);
            Assert.Contains(_martha, someBusinessStuffGroup.Students);
        }

        [Test]
        public void ShowStreams()
        {
            OGNPCourse someBusinessStuff = _extraService.AddOgnpCourse("Keep Grinding!", MegaFaculty.FTMI);
            OGNPStream someBusinessStuffCourseDes = _extraService.AddOgnpStream("Keep Grinding Course Two!",
                someBusinessStuff, CourseNumber.Second);
            OGNPStream someBusinessStuffCourseTres = _extraService.AddOgnpStream("Keep Grinding Course Three!",
                someBusinessStuff, CourseNumber.Third);
            
            Assert.AreEqual(2, _extraService.ShowStreams(someBusinessStuff).Count);
            Assert.Contains(someBusinessStuffCourseDes, _extraService.ShowStreams(someBusinessStuff));
            Assert.Contains(someBusinessStuffCourseTres, _extraService.ShowStreams(someBusinessStuff));
        }

        [Test]
        public void ShowNotSignedAndSignedOnes()
        {
            OGNPCourse someBusinessStuff = _extraService.AddOgnpCourse("Some Business Stuff", MegaFaculty.FTMI);
            OGNPStream someBusinessStuffCourseDes = _extraService.AddOgnpStream("Some Business Stuff Course Two!", 
                someBusinessStuff, CourseNumber.Second);
            OGNPGroup someBusinessStuffGroup = _extraService.AddOgnpGroup("Jay Z Business Man Course 2.1", 
                30, someBusinessStuffCourseDes);
            
            _extraService.SignOne(_max, someBusinessStuffGroup, _lessonService);
            _extraService.SignOne(_martha, someBusinessStuffGroup, _lessonService);
            
            Assert.AreEqual(2, _extraService.ShowSignedOnes(someBusinessStuffGroup).Count);
            Assert.AreEqual(null, _mark.OgnpSigned.Item1);
            Assert.AreEqual(null, _mark.OgnpSigned.Item2);
            Assert.IsNotNull(_max.OgnpSigned.Item1);
            Assert.IsNotNull(_martha.OgnpSigned.Item1);
            Assert.AreEqual(1, _extraService.ShowNotSignedOnes(_m3208).Count);
        }
    }
}