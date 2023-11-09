using Bogus;
using lab1.Plan;
using System.Text.Json;

namespace lab1.plan
{
    public class PlanGenerator
    {
        private Faker<Semester> semeterFaker;  
        private Faker<Group> groupFaker;  
        private Faker<Student> studentFaker;  
        private Faker<Lesson> lessonFaker;  
        private Faker<Teacher> teacherFaker;  

        public PlanGenerator()
        {
            semeterFaker = new Faker<Semester>()
               .RuleFor(p => p.Id, f => f.Random.Number(1,40000))
               .RuleFor(p => p.Name, f => (f.Random.Number() == 1)? "Letni": "Zimowy")
               .RuleFor(p => p.Year, f => f.Random.Number(2022, 2025))
               .RuleFor(p => p.Groups, f => new List<Group>());

            groupFaker = new Faker<Group>()
                .RuleFor(p => p.Id, f => f.Random.Number(1, 40000))
                .RuleFor(p => p.Name, f => f.Random.Word())
                .RuleFor(p => p.Lessons, f => new List<Lesson>())
                .RuleFor(p => p.Students, f => new List<Student>());
            studentFaker = new Faker<Student>()
                .RuleFor(p => p.Id, f => f.Random.Number(1, 40000))
                .RuleFor(p => p.Name, f => f.Random.Word())
                .RuleFor(p => p.Surname, f => f.Random.Word());
            teacherFaker = new Faker<Teacher>()
                .RuleFor(p => p.Id, f => f.Random.Number(1, 40000))
                .RuleFor(p => p.Name, f => f.Random.Word())
                .RuleFor(p => p.Surname, f => f.Random.Word());
            lessonFaker = new Faker<Lesson>()
                .RuleFor(p => p.Id, f => f.Random.Number(1, 40000))
                .RuleFor(p => p.Name, f => f.Random.Word())
                .RuleFor(p => p.Description, f => f.Random.Word())
                .RuleFor(p => p.StartDate, DateTime.Now.AddMinutes(Random.Shared.Next()))
                .RuleFor(p => p.EndDate, DateTime.Now.AddMinutes(Random.Shared.Next()))
                .RuleFor(p => p.Teacher, f => new Teacher())
                .RuleFor(p => p.Room, f => f.Random.Word());
        }

        public List<Semester> GenerateData(int numSemesters, int numGroupsPerSemester, int numStudentsPerGroup, int numTeachers, int numLessonsPerGroup)
        {
            List<Semester> semesters = semeterFaker.Generate(numSemesters);
            List<Group> groups = groupFaker.Generate(numGroupsPerSemester * numSemesters);
            List<Student> students = studentFaker.Generate(numStudentsPerGroup * numGroupsPerSemester * numSemesters);
            List<Teacher> teachers = teacherFaker.Generate(numTeachers);
            List<Lesson> lessons = lessonFaker.Generate(numLessonsPerGroup * numGroupsPerSemester * numSemesters);

            for (int i = 0; i < numSemesters; i++)
            {
                for (int j = 0; j < numGroupsPerSemester; j++)
                {
                    semesters[i].Groups.Add(groups[i * numGroupsPerSemester + j]);
                }
            }

            for (int i = 0; i < numSemesters * numGroupsPerSemester; i++)
            {
                for (int j = 0; j < numStudentsPerGroup; j++)
                {
                    groups[i].Students.Add(students[i * numStudentsPerGroup + j]);
                }
            }

            for (int i = 0; i < numLessonsPerGroup * numGroupsPerSemester * numSemesters; i++)
            {
                lessons[i].Teacher = teachers[i % numTeachers];
            }

            for (int i = 0; i < numLessonsPerGroup * numGroupsPerSemester * numSemesters; i++)
            {
                groups[i / numLessonsPerGroup].Lessons.Add(lessons[i]);
            }

            return semesters;
        }

        public void SaveDataToJson(List<Semester> data, string filePath)
        {
            string json = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        public List<Semester> LoadDataFromJson(string filePath)
        {
            string json = File.ReadAllText(filePath);
            return JsonSerializer.Deserialize<List<Semester>>(json);
        }

    }
}
