using GraphQL.Attendees;
using GraphQL.Data;
using GraphQL.Types;
using HotChocolate;
using HotChocolate.Execution;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Snapshooter.Xunit;

namespace GraphQL.Tests
{
    public class AttendeeTests
    {
        [Fact]
        public async Task Attendee_Schema_Changed()
        {
            // ARRANGE
            // ACT
            ISchema schema = await new ServiceCollection()
                .AddPooledDbContextFactory<ApplicationDbContext>(options =>
                    options.UseSqlite("Data Source=conferences.db"))
                .AddGraphQL()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<AttendeeQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<AttendeeMutations>()
                .AddType<AttendeeType>()
                .AddType<SessionType>()
                .AddType<SpeakerType>()
                .AddType<TrackType>()
                //.EnableRelaySupport() // OBSOLETE
                .AddGlobalObjectIdentification() // .EnableRelaySupport()
                .BuildSchemaAsync();

            // ASSERT
            schema.Print().MatchSnapshot();
        }

        [Fact]
        public async Task Register_Attendee()
        {
            // ARRANGE
            IRequestExecutor executor = await new ServiceCollection()
                .AddPooledDbContextFactory<ApplicationDbContext>(
                    options => options.UseInMemoryDatabase("Data Source=conferences.db"))
                .AddGraphQL()
                .AddQueryType(d => d.Name("Query"))
                    .AddTypeExtension<AttendeeQueries>()
                .AddMutationType(d => d.Name("Mutation"))
                    .AddTypeExtension<AttendeeMutations>()
                .AddType<AttendeeType>()
                .AddType<SessionType>()
                .AddType<SpeakerType>()
                .AddType<TrackType>()
                //.EnableRelaySupport() // OBSOLETE
                .AddGlobalObjectIdentification() // .EnableRelaySupport()
                .BuildRequestExecutorAsync();

            // ACT
            IExecutionResult result = await executor.ExecuteAsync(@"
        mutation RegisterAttendee {
            registerAttendee(
                input: {
                    emailAddress: ""michael@chillicream.com""
                        firstName: ""michael""
                        lastName: ""staib""
                        userName: ""michael3""
                    })
            {
                attendee {
                    id
                }
            }
        }");

            // ASSERT
            result.ToJson().MatchSnapshot();
        }
    }
}