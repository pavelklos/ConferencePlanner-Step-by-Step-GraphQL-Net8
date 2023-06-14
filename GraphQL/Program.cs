using GraphQL;
using GraphQL.Data;
using GraphQL.DataLoader;
using GraphQL.Speakers;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

//------------------------------------------------------------------------------
var builder = WebApplication.CreateBuilder(args);
//------------------------------------------------------------------------------

// Logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// DbContext
//builder.Services.AddDbContext<ApplicationDbContext>(options =>
builder.Services.AddPooledDbContextFactory<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=conferences.db"));

// GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>()
    //.AddMutationType<SpeakerMutations>()
    .AddMutationType(d => d.Name("Mutation"))
        .AddTypeExtension<SpeakerMutations>()
    .AddType<SpeakerType>()
    .AddDataLoader<SpeakerByIdDataLoader>()
    .AddDataLoader<SessionByIdDataLoader>();

//------------------------------------------------------------------------------
var app = builder.Build();
//------------------------------------------------------------------------------

// GraphQL
app.MapGraphQL();

// Minimal API
app.MapGet("/", () => "Hello World!");
//app.MapGet("/speakers", ([Service] ApplicationDbContext context)
//    => context.Speakers.ToList());

//------------------------------------------------------------------------------
app.Run();
//------------------------------------------------------------------------------
