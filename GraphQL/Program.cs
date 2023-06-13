using GraphQL;
using GraphQL.Data;
using Microsoft.EntityFrameworkCore;

//------------------------------------------------------------------------------
var builder = WebApplication.CreateBuilder(args);
//------------------------------------------------------------------------------

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=conferences.db"));

// GraphQL
builder.Services.AddGraphQLServer()
    .AddQueryType<Query>();


//------------------------------------------------------------------------------
var app = builder.Build();
//------------------------------------------------------------------------------

// GraphQL
app.MapGraphQL();

app.MapGet("/", () => "Hello World!");

//------------------------------------------------------------------------------
app.Run();
//------------------------------------------------------------------------------
