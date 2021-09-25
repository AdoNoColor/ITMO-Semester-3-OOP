using System.Collections.Generic;
using Isu.Classes;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly List<Group> _allGroups = new List<Group>();
        private readonly List<Student> _allStudents = new List<Student>();

        public Group AddGroup(string name, int maxStudents)
        {
            var @group = new Group(name, maxStudents);
            _allGroups.Add(@group);
            return @group;
        }

        public Student AddStudent(Group @group, string name)
        {
            if (Group.MaxStudents >= @group.Students.Count)
            {
                throw new IsuException($"{@group.GroupName} is fully packed");
            }

            var student = new Student(name);
            @group.Students.Add(student);
            _allStudents.Add(student);
            student.GroupName = @group;

            // student.CourseNumber;
            return student;
        }

        public Student GetStudent(int id)
        {
            foreach (Student student in _allStudents)
            {
                if (id.Equals(student.Id))
                {
                    return student;
                }
            }

            return null;
        }

        public Student FindStudent(string name)
        {
            foreach (Student student in _allStudents)
            {
                if (name.Equals(student.Name))
                {
                    return student;
                }
            }

            return null;
        }

        public List<Student> FindStudents(string groupName)
        {
            return FindGroup(groupName).Students;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            var tempListOfStudents = new List<Student>();
            foreach (Student student in _allStudents)
            {
                if (courseNumber.Equals(student.Course))
                {
                    tempListOfStudents.Add(student);
                }
            }

            return tempListOfStudents;
        }

        public Group FindGroup(string groupName)
        {
            foreach (Group @group in _allGroups)
            {
                if (groupName.Equals(@group.GroupName))
                {
                    return @group;
                }
            }

            return null;
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
            {
                var tempListOfGroups = new List<Group>();
                foreach (Group group in _allGroups)
                {
                    if (courseNumber.Equals(group.Course))
                    {
                        tempListOfGroups.Add(group);
                    }
                }

                return tempListOfGroups;
            }

        public void ChangeStudentGroup(Student student, Group newGroup)
            {
                if (newGroup.Students.Count < Group.MaxStudents)
                {
                    student.GroupName.Students.Remove(student);
                    newGroup.Students.Add(student);
                    student.GroupName = newGroup;
                }
                else
                {
                    throw new IsuException($"{newGroup.GroupName} is fully packed");
                }
            }
        }
    }