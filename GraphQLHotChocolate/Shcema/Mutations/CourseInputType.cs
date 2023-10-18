using GraphQLHotChocolate.Models;
using GraphQLHotChocolate.Shcema.Queries;

namespace GraphQLHotChocolate.Shcema.Mutations;
/// <summary>
/// This class contains the properties often requested in mutation parameters. This class simplifies the multiple parameters into a singular object and turns the request into a JSON object
/// </summary>
public class CourseInputType
{
    public string Name { get; set; }
    public Subject Subject { get; set; }
    public Guid InstructorId { get; set; }
    
}