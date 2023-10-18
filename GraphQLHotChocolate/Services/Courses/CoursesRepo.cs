using GraphQLHotChocolate.DTO;
using Microsoft.EntityFrameworkCore;

namespace GraphQLHotChocolate.Services.Courses;

public class CoursesRepo
{
    //Ensures new DB context instance and resolve any EF concurrency issues
    private readonly IDbContextFactory<SchoolDbContext> _contextFactory;

    public CoursesRepo(IDbContextFactory<SchoolDbContext> contextFactory)
    {
        _contextFactory = contextFactory;
    }

    public async Task<CourseDTO> Create(CourseDTO course)
    {
        using (SchoolDbContext context = _contextFactory.CreateDbContext())
        {
            context.Courses.Add(course);
            await context.SaveChangesAsync();

            return course;
        }
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