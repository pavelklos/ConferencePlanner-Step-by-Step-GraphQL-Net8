using GraphQL.Data;
using GraphQL.DataLoader;

namespace GraphQL.Sessions
{
    [ExtendObjectType(Name = "Subscription")]
    public class SessionSubscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Session> OnSessionScheduledAsync(
            [EventMessage] int sessionId,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            sessionById.LoadAsync(sessionId, cancellationToken);
    }
}