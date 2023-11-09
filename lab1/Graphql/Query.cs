using lab1.Graphql.Filters;
using lab1.plan;
using lab1.Plan;
using System.Linq;

namespace lab1.Graphql
{
    public class Query
    {
        public List<Semester> GetSemesters([Service] PlanGenerator planGenerator)
        {
            return planGenerator.LoadDataFromJson("data.json").Select(semester =>
            {
                return new Semester
                {
                    Id = semester.Id,
                    Name = semester.Name,
                    Year = semester.Year,
                    Groups = null
                };
            }).ToList();
        }

        public IEnumerable<Student> GetStudents([Service] PlanGenerator planGenerator, StudentFilter? filter)
        {
            var students = planGenerator.LoadDataFromJson("data.json").SelectMany(semester => semester.Groups)
                .SelectMany(group => group.Students);

            if (filter != null)
            {
                if (!string.IsNullOrWhiteSpace(filter.Name))
                {
                    students = students.Where(student => student.Name.Contains(filter.Name, StringComparison.OrdinalIgnoreCase));
                }

                if (!string.IsNullOrWhiteSpace(filter.Surname))
                {
                    students = students.Where(student => student.Surname.Contains(filter.Surname, StringComparison.OrdinalIgnoreCase));
                }
            }

            return students;
        }
    }
}
