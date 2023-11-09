namespace lab1.plan
{
    public class Lesson
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Teacher Teacher { get; set; }
        public string Room { get; set; }
    }
}
