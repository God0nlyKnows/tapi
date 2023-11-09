using lab1.plan;

namespace lab1.Graphql
{
    public class Mutation
    {
        public Student CreateStudent([Service] PlanGenerator planGenerator, string name, string surname)
        {

            Student newStudent = new Student
            {
                Id = Random.Shared.Next(1,30000),
                Name = name,
                Surname = surname
            };
            var semesters = planGenerator.LoadDataFromJson("data.json");
            semesters[0].Groups[0].Students.Add(newStudent);

            planGenerator.SaveDataToJson(semesters, "data.json");
            return newStudent;
        }
    }
}
