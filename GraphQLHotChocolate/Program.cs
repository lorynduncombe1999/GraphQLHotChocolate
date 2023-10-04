
using GraphQLHotChocolate.Shcema;
using GraphQLHotChocolate.Shcema.Mutations;
using GraphQLHotChocolate.Shcema.Queries;
using GraphQLHotChocolate.Shcema.Subscriptions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>()
    .AddSubscriptionType<Subscription>()
    .AddInMemorySubscriptions();


var app = builder.Build();
//Web Sockets added for Subscriptions
app.UseWebSockets();
app.MapGraphQL();

app.Run();

