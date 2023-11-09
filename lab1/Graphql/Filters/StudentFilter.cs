namespace lab1.Graphql.Filters
{
    public class StudentFilterInputType : InputObjectType<StudentFilter>
    {
        protected override void Configure(IInputObjectTypeDescriptor<StudentFilter> descriptor)
        {
            descriptor.Field(t => t.Name)
                .Description("Filter by student name.");

            descriptor.Field(t => t.Surname)
                .Description("Filter by student surname.");
        }
    }

    public class StudentFilter
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
    }
}
