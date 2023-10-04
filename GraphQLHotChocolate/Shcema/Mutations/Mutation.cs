using GraphQLHotChocolate.Shcema.Queries;
using GraphQLHotChocolate.Shcema.Subscriptions;
using HotChocolate.Subscriptions;

namespace GraphQLHotChocolate.Shcema.Mutations;
/// <summary>
/// This class contains operations for Mutations 
/// </summary>
public class Mutation
{
    /// <summary>
    /// This List of Course Results is were data is stored until DB is instantiated and connected to application
    /// </summary>
    private readonly List<CourseResult> _courses;

    /// <summary>
    /// Class Constructor that creates the list of data to hold
    /// </summary>
    public Mutation()
    {
        _courses = new List<CourseResult>();
    }
  /// <summary>
  /// This method creates a course and also implments the  publisher/subscription relationship through hotchocolate
  /// </summary>
  /// <param name="courseInput">Object that contains multiple properties relevant for course Creation</param>
  /// <param name="topicEventSender">parameter that enables helps to publish the subscription</param>
  /// <returns>Task<CourseResult></returns>
    public async Task<CourseResult> CreateCourse(CourseInputType courseInput,[Service] ITopicEventSender topicEventSender)
    {
        CourseResult courseType = new CourseResult()
        {
            Id =  Guid.NewGuid(),
            Name = courseInput.Name,
            Subject = courseInput.Subject,
            InstrucotrId = courseInput.InstructorId
        };
        _courses.Add(courseType);
        await topicEventSender.SendAsync(nameof(Subscription.CourseCreated), courseType);
        return courseType;
    }
    /// <summary>
    /// This method updates courses
    /// </summary>
    /// <param name="id">Course Id</param>
    /// <param name="courseInput">Object that has multiple properties requested to create a new CourseResult Object</param>
    /// <returns>Course Result</returns>
    /// <exception cref="GraphQLException">Throws exception if the course id returns a null result (Course not found)</exception>

    public  CourseResult UpdateCourse(Guid id,CourseInputType courseInput)
    {
       CourseResult course =  _courses.FirstOrDefault(c => c.Id == id);

       if (course == null)
       {
           throw new GraphQLException(new Error("Course Not Found", "COURSE_NOT_FOUND"));


       }

       course.Id = Guid.NewGuid();
       course.Name = courseInput.Name;
       course.Subject = courseInput.Subject;
       course.InstrucotrId = courseInput.InstructorId;
        
       return course;
    }
/// <summary>
/// This method deletes courses by id
/// </summary>
/// <param name="id"> course id</param>
/// <returns>Confirms course deletion</returns>
    public bool DeleteCourse(Guid id)
    {
        return _courses.RemoveAll(c => c.Id == id) >= 1;
        
    }
}