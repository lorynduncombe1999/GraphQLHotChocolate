namespace GraphQLHotChocolate.Shcema.Queries;
/// <summary>
/// This class creates the neccessary properties for the CourseType
/// Subject is a nested type within course type and is represented by using enums
/// </summary>

public enum Subject
{
    Mathematics,
    Science,
    History
}
public class CourseType
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Subject Subject { get; set; }
   
    [GraphQLNonNullType]
    public InstructorType Instructor { get; set; }
    public IEnumerable<StudentType> Students { get; set; }
}