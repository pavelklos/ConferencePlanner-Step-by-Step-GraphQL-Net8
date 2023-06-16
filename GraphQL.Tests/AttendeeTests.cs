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
    }
}