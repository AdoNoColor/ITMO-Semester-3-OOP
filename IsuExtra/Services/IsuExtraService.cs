using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Services;
using IsuExtra.Entities;
using IsuExtra.Tools;

namespace IsuExtra.Services
{
    public class IsuExtraService : IsuService
    {
        private List<OGNPCourse> _allCourses = new List<OGNPCourse>();
        public OGNPCourse AddOgnpCourse(string name, MegaFaculty attachedFaculty)
        {
            var ognp = new OGNPCourse(name, attachedFaculty);
            if (_allCourses.Any(course => ognp == course))
            {
                throw new IsuExtraException("Course like this exists!");
            }

            _allCourses.Add(ognp);
            return ognp;
        }

        public OGNPStream AddOgnpStream(string name, OGNPCourse attachedOgnpCourse, CourseNumber courseNumber)
        {
            var stream = new OGNPStream(name, attachedOgnpCourse, courseNumber);
            if (attachedOgnpCourse.Streams.Any(flow => stream.Equals(flow)))
            {
                throw new IsuExtraException("Stream like this exists!");
            }

            attachedOgnpCourse.Streams.Add(stream);

            return stream;
        }

        public OGNPGroup AddOgnpGroup(string groupName, int maxStudents, OGNPStream attachedStream)
        {
            var group = new OGNPGroup(groupName, maxStudents, attachedStream);

            if (_allCourses.Where(course => course == attachedStream.AttachedOGNPCourse).
                Any(course => course.Streams.Where(stream => stream == attachedStream).
                    Any(stream => stream.OGNPGroups.Any(anotherGroup => @group.GroupName == anotherGroup.GroupName))))
            {
                throw new IsuExtraException("Group like this exists!");
            }

            attachedStream.OGNPGroups.Add(@group);

            return group;
        }

        public void SignStudent(StudentExtra student, OGNPGroup ognpGroup, LessonService lessonService)
        {
            foreach (OGNPStream stream in _allCourses.SelectMany(course => course.Streams))
            {
                foreach (OGNPGroup @group in stream.OGNPGroups.Where(@group => ognpGroup == @group))
                {
                    if (student.Course != @group.AttachedStream.CourseNumber)
                        throw new IsuExtraException("Courses doesn't match!");

                    if (lessonService.DoTheyMatch(ognpGroup.GroupName, student.GroupName.GroupName))
                        throw new IsuExtraException("Collusion between lessons!");

                    if (student.GroupName.MegaFaculty == ognpGroup.AttachedStream.AttachedOGNPCourse.AttachedFaculty)
                        throw new IsuExtraException("The Mega Faculty is the same!");

                    if (ognpGroup.MaxStudents == ognpGroup.Students.Count)
                        throw new IsuExtraException("The OGNP Group is full!");

                    if (student.OgnpSigned.Count == 2)
                        throw new IsuExtraException("Cannot sign to more than 2 groups!");

                    group.AddStudent(student);
                    return;
                }
            }

            throw new IsuExtraException("ognpGroup hasn't been found!");
        }

        public void RemoveStudent(OGNPGroup ognpGroup, StudentExtra student)
        {
            foreach (OGNPGroup @group in _allCourses.SelectMany(course => course.Streams.SelectMany(stream =>
                stream.OGNPGroups.Where(@group => @group == ognpGroup))))
            {
                @group.RemoveStudent(student);
                return;
            }

            throw new IsuExtraException("Student hasn't been found!");
        }

        public List<OGNPStream> ShowStreams(OGNPCourse ognpCourse)
        {
            return ognpCourse.Streams.ToList();
        }

        public List<Student> ShowSignedStudents(OGNPGroup group)
        {
            return group.Students.ToList();
        }

        public List<StudentExtra> ShowNotSignedStudents(GroupExtra group)
        {
            return @group.Students.Where(student => student.OgnpSigned.Count == 0).ToList();
        }
    }
}