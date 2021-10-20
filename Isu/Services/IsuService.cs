using System.Collections.Generic;
using System.Linq;
using Isu.Entities;
using Isu.Tools;

namespace Isu.Services
{
    public class IsuService : IIsuService
    {
        private readonly List<Group> _allGroups = new List<Group>();
        private readonly List<Student> _allStudents = new List<Student>();

        public Group AddGroup(string name, int maxStudents)
        {
            var group = new Group(name, maxStudents);
            _allGroups.Add(group);
            return group;
        }

        public Student AddStudent(Group group, string name)
        {
            if (group == null)
            {
                throw new IsuException("There is no group like that");
            }

            if (group.MaxStudents <= group.Students.Count)
            {
                throw new IsuException($"{group.GroupName} is fully packed");
            }

            var student = new Student(name, group);
            group.Students.Add(student);
            _allStudents.Add(student);
            return student;
        }

        public Student GetStudent(int id)
        {
            return _allStudents.FirstOrDefault(student => id.Equals(student.Id));
        }

        public Student FindStudent(string name)
        {
            return _allStudents.Find(student => name == student.Name);
        }

        public List<Student> FindStudents(string groupName)
        {
            return FindGroup(groupName).Students;
        }

        public List<Student> FindStudents(CourseNumber courseNumber)
        {
            return _allStudents.Where(student => courseNumber.Equals(student.Course)).ToList();
        }

        public Group FindGroup(string groupName)
        {
            return _allGroups.FirstOrDefault(group => groupName.Equals(group.GroupName));
        }

        public List<Group> FindGroups(CourseNumber courseNumber)
        {
            return _allGroups.Where(group => courseNumber.Equals(group.Course)).ToList();
        }

        public void ChangeStudentGroup(Student student, Group newGroup)
        {
			if (newGroup == null)
				throw new IsuException($"{newGroup.GroupName} does not exist");
            if (newGroup.Students.Count >= newGroup.MaxStudents)
                throw new IsuException($"{newGroup.GroupName} is fully packed");
            student.GroupName.Students.Remove(student);
            newGroup.Students.Add(student);
            student.GroupName = newGroup;
        }
    }
}