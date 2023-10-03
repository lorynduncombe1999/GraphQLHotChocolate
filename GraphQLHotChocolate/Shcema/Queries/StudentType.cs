namespace GraphQLHotChocolate.Shcema.Queries;
/// <summary>
/// This class contains properties for the Student Type
/// </summary>
public class StudentType
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double GPA { get; set; }
    
}