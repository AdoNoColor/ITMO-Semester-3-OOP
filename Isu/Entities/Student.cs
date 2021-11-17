namespace Isu.Entities
{
    public class Student
    {
        private static int _id = 0;

        public Student(string name)
        {
            _id++;
            Name = name;
            Id = _id;
        }

        public Student(string name, Group groupName)
        {
            _id++;
            GroupName = groupName;
            Name = name;
            Id = _id;
            Course = groupName.Course;
        }

        public int Id { get; }
        public string Name { get; }
        public Group GroupName { get; set; }

        public CourseNumber Course { get; set; }
    }
}