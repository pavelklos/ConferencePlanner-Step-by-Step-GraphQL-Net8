using GraphQL.Data;

namespace GraphQL.Attendees
{
    [ExtendObjectType(Name = "Mutation")]
    public class AttendeeMutations
    {
        [UseApplicationDbContext]
        public async Task<RegisterAttendeePayload> RegisterAttendeeAsync(
            RegisterAttendeeInput input,
            [ScopedService] ApplicationDbContext context,
            CancellationToken cancellationToken)
        {
            var attendee = new Attendee
            {
                FirstName = input.FirstName,
                LastName = input.LastName,
                UserName = input.UserName,
                EmailAddress = input.EmailAddress
            };

            context.Attendees.Add(attendee);

            await context.SaveChangesAsync(cancellationToken);

            return new RegisterAttendeePayload(attendee);
        }
    }
}