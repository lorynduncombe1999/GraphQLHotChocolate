
using GraphQLHotChocolate.Services;
using GraphQLHotChocolate.Services.Courses;
using GraphQLHotChocolate.Shcema;
using GraphQLHotChocolate.Shcema.Mutations;
using GraphQLHotChocolate.Shcema.Queries;
using GraphQLHotChocolate.Shcema.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();

string connectionString = builder.Configuration.GetConnectionString("default") ;
builder.Services.AddPooledDbContextFactory<SchoolDbContext>(o => o.UseSqlite(connectionString));

builder.Services.AddScoped<CoursesRepo>();
var app = builder.Build();
//Web Sockets added for Subscriptions
app.UseWebSockets();
app.MapGraphQL();

app.Run();

