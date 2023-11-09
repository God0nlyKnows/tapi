using lab1.plan;

namespace lab1.Plan
{
    public class Semester
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        [GraphQLIgnore]
        public List<Group> Groups { get; set; }
    }
}
