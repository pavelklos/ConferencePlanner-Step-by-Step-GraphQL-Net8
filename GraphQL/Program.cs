using GraphQL.Data;
using Microsoft.EntityFrameworkCore;

//------------------------------------------------------------------------------
var builder = WebApplication.CreateBuilder(args);
//------------------------------------------------------------------------------

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite("Data Source=conferences.db"));


//------------------------------------------------------------------------------
var app = builder.Build();
//------------------------------------------------------------------------------

app.MapGet("/", () => "Hello World!");

//------------------------------------------------------------------------------
app.Run();
//------------------------------------------------------------------------------
