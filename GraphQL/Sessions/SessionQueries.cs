using GraphQL.Data;
using GraphQL.DataLoader;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Sessions
{
    [ExtendObjectType("Query")]
    public class SessionQueries
    {
        [UseApplicationDbContext]
        [UsePaging(typeof(NonNullType<SessionType>))]
        //[UseFiltering]
        [UseFiltering(typeof(SessionFilterInputType))]
        [UseSorting]
        public IQueryable<Session> GetSessions(
            [ScopedService] ApplicationDbContext context) =>
            context.Sessions;

        //[UseApplicationDbContext]
        ////[UsePaging]
        //[UsePaging(typeof(NonNullType<SessionType>))]
        //public IQueryable<Session> GetSessions(
        //    [ScopedService] ApplicationDbContext context) =>
        //    context.Sessions;

        //[UseApplicationDbContext]
        //public async Task<IEnumerable<Session>> GetSessionsAsync(
        //    [ScopedService] ApplicationDbContext context,
        //    CancellationToken cancellationToken) =>
        //    await context.Sessions.ToListAsync(cancellationToken);

        public Task<Session> GetSessionByIdAsync(
            [ID(nameof(Session))] int id,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            sessionById.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Session>> GetSessionsByIdAsync(
            [ID(nameof(Session))] int[] ids,
            SessionByIdDataLoader sessionById,
            CancellationToken cancellationToken) =>
            await sessionById.LoadAsync(ids, cancellationToken);
    }
}