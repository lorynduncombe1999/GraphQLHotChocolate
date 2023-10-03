namespace GraphQLHotChocolate.Shcema.Queries;

/// <summary>
/// This class contains the necessary properties for the InstructorType
/// </summary>
public class InstructorType
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Salary { get; set; }
    
}