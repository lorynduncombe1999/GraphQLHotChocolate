using Bogus;

namespace GraphQLHotChocolate.Shcema.Queries;
/// <summary>
/// This class contains operations for executing queries
/// </summary>
public class Query
{
    /// <summary>
    /// This Faker class is from the nuget package "Bogus" which will act as a way to create fake data until db is instantiated and connected.
    /// It generates fake instuctors,courses, and students
    /// </summary>
    private readonly Faker<InstructorType> _instructorFaker;
    private readonly Faker<CourseType> _courseFaker;
    private readonly Faker<StudentType> _studentFaker;

    /// <summary>
    /// This class constructor sets the faker objects to rules for creating the properties within each respective type
    /// </summary>
    public Query()
    {
        _instructorFaker = new Faker<InstructorType>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.Salary, f => f.Random.Double(0, 10000));
        
        _studentFaker = new Faker<StudentType>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.FirstName, f => f.Name.FirstName())
            .RuleFor(c => c.LastName, f => f.Name.LastName())
            .RuleFor(c => c.GPA, f => f.Random.Double(1, 4));

        _courseFaker = new Faker<CourseType>()
            .RuleFor(c => c.Id, f => Guid.NewGuid())
            .RuleFor(c => c.Name, f => f.Name.JobTitle())
            .RuleFor(c => c.Instructor, f => _instructorFaker.Generate())
            .RuleFor(c => c.Students, f => _studentFaker.Generate(3));




    }
    /// <summary>
    /// This method  gets all courses
    /// </summary>
    /// <returns>all courses within faker object</returns>
    public IEnumerable<CourseType> GetCourses()
    {
        return _courseFaker.Generate(5);
    }
    
    /// <summary>
    /// This query gets courses by Id
    /// </summary>
    /// <param name="id"> Course Id</param>
    /// <returns>Singular Course</returns>
    public async Task<CourseType> GetCourseByIdAsync(Guid id)
    {
        await Task.Delay(1000);
        CourseType course = _courseFaker.Generate();
        course.Id = id;
        return  course;
    }
    
}