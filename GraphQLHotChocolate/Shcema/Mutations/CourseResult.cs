using GraphQLHotChocolate.Shcema.Queries;

namespace GraphQLHotChocolate.Shcema.Mutations;

/// <summary>
/// This class contains the properties that are returned within a course result 
/// </summary>
public class CourseResult
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid InstrucotrId { get; set; }
    public Subject Subject { get; set; }
}