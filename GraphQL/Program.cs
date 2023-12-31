using GraphQL.Attendees;
using GraphQL.Data;
using GraphQL.DataLoader;
using GraphQL.Sessions;
using GraphQL.Speakers;
using GraphQL.Tracks;
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
    //.AddQueryType<SpeakerQueries>()
    .AddQueryType(d => d.Name("Query"))
        .AddTypeExtension<AttendeeQueries>()
        .AddTypeExtension<SessionQueries>()
        .AddTypeExtension<SpeakerQueries>()
        .AddTypeExtension<TrackQueries>()
    //.AddMutationType<SpeakerMutations>()
    .AddMutationType(d => d.Name("Mutation"))
        .AddTypeExtension<AttendeeMutations>()
        .AddTypeExtension<SessionMutations>()
        .AddTypeExtension<SpeakerMutations>()
        .AddTypeExtension<TrackMutations>()
    .AddSubscriptionType(d => d.Name("Subscription"))
        .AddTypeExtension<AttendeeSubscriptions>()
        .AddTypeExtension<SessionSubscriptions>()
    .AddType<AttendeeType>()
    .AddType<SessionType>()
    .AddType<SpeakerType>()
    .AddType<TrackType>()
    //.EnableRelaySupport() // OBSOLETE
    .AddGlobalObjectIdentification() // .EnableRelaySupport()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions() // In-Memory pub/sub system for GraphQL subscriptions 
    .AddDataLoader<SpeakerByIdDataLoader>()
    .AddDataLoader<SessionByIdDataLoader>()
    .ModifyRequestOptions(opt => opt.IncludeExceptionDetails = builder.Environment.IsDevelopment()); // ADDED

//------------------------------------------------------------------------------
var app = builder.Build();
//------------------------------------------------------------------------------

// WebSockets
app.UseWebSockets();

// GraphQL
app.MapGraphQL();

// Minimal API
app.MapGet("/", () => "Hello World!");
//app.MapGet("/speakers", ([Service] ApplicationDbContext context)
//    => context.Speakers.ToList());

//------------------------------------------------------------------------------
app.Run();
//------------------------------------------------------------------------------
