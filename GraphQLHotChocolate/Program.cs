
using GraphQLHotChocolate.Shcema;
using GraphQLHotChocolate.Shcema.Mutations;
using GraphQLHotChocolate.Shcema.Queries;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGraphQLServer()
    .AddQueryType<Query>()
    .AddMutationType<Mutation>();

var app = builder.Build();

app.MapGraphQL();

app.Run();

