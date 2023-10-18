using GraphQLHotChocolate.DTO;
using GraphQLHotChocolate.Services.Courses;
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
    private readonly CoursesRepo _coursesRepo;


    public Mutation(CoursesRepo courses)
    {
        _coursesRepo  = courses;
    }

    /// <summary>
  /// This method creates a course and also implments the  publisher/subscription relationship through hotchocolate
  /// </summary>
  /// <param name="courseInput">Object that contains multiple properties relevant for course Creation</param>
  /// <param name="topicEventSender">parameter that enables helps to publish the subscription</param>
  /// <returns>Task<CourseResult></returns>
    public async Task<CourseResult> CreateCourse(CourseInputType courseInput,[Service] ITopicEventSender topicEventSender)
    {
        CourseDTO courseDto = new CourseDTO()
        {
            Name = courseInput.Name,
            Subject = courseInput.Subject,
            InstructorId = courseInput.InstructorId
        };
        courseDto   = await _coursesRepo.Create(courseDto);
        
        
        CourseResult courseType = new CourseResult()
        {
            Id =  courseDto.Id,
            Name = courseDto.Name,
            Subject = courseDto.Subject,
            InstrucotrId = courseDto.InstructorId
        };
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

    public async Task<CourseResult> UpdateCourse(Guid id,CourseInputType courseInput)
    {
        CourseDTO courseDto = new CourseDTO()
        {
            Id = id,
            Name = courseInput.Name,
            Subject = courseInput.Subject,
            InstructorId = courseInput.InstructorId
        };
        courseDto = await _coursesRepo.Update(courseDto);
        
        CourseResult courseType = new CourseResult()
        {
            Id =  courseDto.Id,
            Name = courseDto.Name,
            Subject = courseDto.Subject,
            InstrucotrId = courseDto.InstructorId
        };
        courseType.Name = courseInput.Name;
        courseType.Subject = courseInput.Subject;
        courseType.InstrucotrId = courseInput.InstructorId;
        
        return courseType;
    }
/// <summary>
/// This method deletes courses by id
/// </summary>
/// <param name="id"> course id</param>
/// <returns>Confirms course deletion</returns>
    public async Task<bool> DeleteCourse(Guid id)
{
    return await _coursesRepo.Delete(id);

}
}