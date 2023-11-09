using lab1.Graphql;
using lab1.Graphql.Filters;
using lab1.plan;
using lab1.Plan;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<PlanGenerator>();
builder.Services.AddGraphQLServer()
    .AddTypeExtension<Filters>()
    .AddMutationType<Mutation>()
    .AddQueryType<Query>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapGraphQL();


//PlanGenerator planGenerator = new PlanGenerator();
//planGenerator.SaveDataToJson( planGenerator.GenerateData(2, 3, 10, 5, 4), "data.json");




//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

//app.MapGet("/semesters", () =>
//{
//    Stopwatch stopwatch = new Stopwatch();
//    stopwatch.Start();

//    List<Semester> semestersWithoutGroups = planGenerator.LoadDataFromJson("data.json").Select(semester =>
//    {
//        return new  Semester
//        {
//            Id = semester.Id,
//            Name = semester.Name,
//            Year = semester.Year,
//            Groups = null
//        };
//    }).ToList();

//    stopwatch.Stop();
//    TimeSpan elapsedTime = stopwatch.Elapsed;

//    Console.WriteLine($"Response produced in {elapsedTime.TotalMilliseconds} milliseconds.");

//    return semestersWithoutGroups;
//})
//.WithName("GetSemesters")
//.WithOpenApi();

//app.MapGet("/students", () =>
//{
//    return planGenerator.LoadDataFromJson("data.json").SelectMany(semester => semester.Groups)
//    .SelectMany(group => group.Students);
//})
//.WithName("GetStudents")
//.WithOpenApi();



app.Run();

internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
