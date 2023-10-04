using GraphQLHotChocolate.Shcema.Mutations;
using GraphQLHotChocolate.Shcema.Queries;
using HotChocolate.Execution;
using HotChocolate.Subscriptions;

namespace GraphQLHotChocolate.Shcema.Subscriptions;

/// <summary>
/// This class demonstrates the Subscription feature within hot chocolate
/// </summary>
public class Subscription
{
    [Subscribe]
    public CourseResult CourseCreated([EventMessage]CourseResult course) => course;

   
}