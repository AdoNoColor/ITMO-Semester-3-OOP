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
            if (attachedOgnpCourse.Streams.Any(flow => stream == flow))
            {
                throw new IsuExtraException("Stream like this exists!");
            }

            foreach (OGNPCourse course in _allCourses.Where(course => course == attachedOgnpCourse))
            {
                course.Streams.Add(stream);
            }

            return stream;
        }

        public OGNPGroup AddOgnpGroup(string groupName, int maxStudents, OGNPStream attachedStream)
        {
            var group = new OGNPGroup(groupName, maxStudents, attachedStream);

            if (_allCourses.Where(course => course == attachedStream.AttachedOGNPCourse).
                Any(course => course.Streams.Where(stream => stream == attachedStream).
                    Any(stream => stream.OGNPGroups.Any(anothaGroup => @group.GroupName == anothaGroup.GroupName))))
            {
                throw new IsuExtraException("Group like this exists!");
            }

            foreach (OGNPStream stream in _allCourses.Where(course => course == attachedStream.AttachedOGNPCourse).
                SelectMany(course => course.Streams.Where(stream => stream == attachedStream)))
            {
                stream.OGNPGroups.Add(@group);
            }

            return group;
        }

        public void SignOne(StudentExtra student, OGNPGroup ognpGroup, LessonService lessonService)
        {
            foreach (OGNPStream stream in _allCourses.SelectMany(course => course.Streams))
            {
                foreach (OGNPGroup @group in stream.OGNPGroups.Where(@group => ognpGroup == @group))
                {
                    if (student.Course != @group.AttachedStream.CourseNumber)
                        throw new IsuExtraException("Courses doesn't match!");

                    if (lessonService.DoTheyMatch(ognpGroup.GroupName, student.GroupName.GroupName))
                        throw new IsuExtraException("Collusion between lessons!");
                    @group.Students.Add(student);
                    if (student.OgnpSigned.Item1 == null)
                    {
                        (string, string) ognpSigned = student.OgnpSigned;
                        ognpSigned.Item1 = ognpGroup.GroupName;
                        student.OgnpSigned = ognpSigned;
                        break;
                    }

                    if (student.OgnpSigned.Item2 == null)
                    {
                        (string, string) ognpSigned = student.OgnpSigned;
                        ognpSigned.Item2 = ognpGroup.GroupName;
                        student.OgnpSigned = ognpSigned;
                        break;
                    }
                }
            }
        }

        public void RemoveOne(OGNPGroup ognpGroup, StudentExtra student)
        {
            foreach (OGNPGroup @group in _allCourses.SelectMany(course => course.Streams.
                SelectMany(stream => stream.OGNPGroups.Where(@group => @group == ognpGroup))))
            {
                foreach (StudentExtra anotherStudent in ognpGroup.Students.
                    Where(anotherStudent => student == anotherStudent))
                {
                    @group.Students.Remove(anotherStudent);
                    if (anotherStudent.OgnpSigned.Item1.Equals(@group.GroupName))
                    {
                        (string, string) signed = anotherStudent.OgnpSigned;
                        signed.Item1 = null;
                        anotherStudent.OgnpSigned = signed;
                        break;
                    }

                    if (anotherStudent.OgnpSigned.Item2.Equals(@group.GroupName))
                    {
                        (string, string) signed = anotherStudent.OgnpSigned;
                        signed.Item2 = null;
                        anotherStudent.OgnpSigned = signed;
                        break;
                    }
                }
            }
        }

        public List<OGNPStream> ShowStreams(OGNPCourse ognpCourse)
        {
            return ognpCourse.Streams.ToList();
        }

        public List<Student> ShowSignedOnes(OGNPGroup group)
        {
            return group.Students.ToList();
        }

        public List<StudentExtra> ShowNotSignedOnes(GroupExtra group)
        {
            var notSigned = @group.Students.Where(student => student.OgnpSigned.Item1 == null && student.OgnpSigned.Item2 == null).ToList();

            return notSigned.ToList();
        }
    }
}