using GraphQLHotChocolate.DTO;
using Microsoft.EntityFrameworkCore;

namespace GraphQLHotChocolate.Services.Courses;

public class CoursesRepo
{
    //Ensures new DB context instance and resolve any EF concurrency issues
    private readonly IDbContextFactory<SchoolDbContext> _contextFactory;
    
    //Will not work in a distributed environment due to it being static (In-Memory Lock)
    private static readonly SemaphoreSlim SemaphoreSlim = new(1, 1);
    public CoursesRepo(IDbContextFactory<SchoolDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<CourseDTO> Create(CourseDTO course)
    {
        using (SchoolDbContext context = _contextFactory.CreateDbContext())
        {
            try
            {
                if (!await SemaphoreSlim.WaitAsync(100))
                {
                    throw new Exception("Please try again later");
                }

                //Critical section goes inside of the Mutex
                context.Courses.Add(course);
                await context.SaveChangesAsync();
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }
        return course;
    }

    public async Task<CourseDTO> Update(CourseDTO course)
    {
        using (SchoolDbContext context = _contextFactory.CreateDbContext())
        {
            context.Courses.Update(course);
            await context.SaveChangesAsync();

            return course;
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        using (SchoolDbContext context = _contextFactory.CreateDbContext())
        {
            CourseDTO course = new CourseDTO()
            {
                Id = id
            };

            context.Courses.Remove(course);
            return await context.SaveChangesAsync()>0;
            
        }
    }
}