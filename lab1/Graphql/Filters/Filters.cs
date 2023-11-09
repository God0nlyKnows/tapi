namespace lab1.Graphql.Filters
{
    public class Filters : ObjectTypeExtension
    {
        protected override void Configure(IObjectTypeDescriptor descriptor)
        {
            descriptor.Field("students")
                .Argument("filter", a => a.Type<StudentFilterInputType>())
                .ResolveWith<Query>(q => q.GetStudents(default!, default!));
        }
    }

}
