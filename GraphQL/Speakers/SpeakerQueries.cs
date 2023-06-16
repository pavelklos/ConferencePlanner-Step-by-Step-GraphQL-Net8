using GraphQL.Data;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.Speakers
{
    [ExtendObjectType("Query")]
    public class SpeakerQueries
    {
        [UseApplicationDbContext]
        [UsePaging]
        public IQueryable<Speaker> GetSpeakers(
            [ScopedService] ApplicationDbContext context) =>
            context.Speakers.OrderBy(t => t.Name);

        //[UseApplicationDbContext]
        //public Task<List<Speaker>> GetSpeakersAsync(
        //    [ScopedService] ApplicationDbContext context) =>
        //    context.Speakers.ToListAsync();

        public Task<Speaker> GetSpeakerByIdAsync(
            [ID(nameof(Speaker))] int id,
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            dataLoader.LoadAsync(id, cancellationToken);

        public async Task<IEnumerable<Speaker>> GetSpeakersByIdAsync(
            [ID(nameof(Speaker))] int[] ids,
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
            await dataLoader.LoadAsync(ids, cancellationToken);
    }
}

//using GraphQL.Data;

//namespace GraphQL
//{
//    public class Query
//    {
//        private readonly ILogger _logger;

//        public Query(ILogger<Query> logger)
//        {
//            _logger = logger;
//        }

//        //public IQueryable<Speaker> GetSpeakers([Service] ApplicationDbContext context) =>
//        //    context.Speakers;

//        public IQueryable<Speaker> GetSpeakers([Service] ApplicationDbContext context)
//        {
//            _logger.LogInformation("█ DB-TABLE █ Speakers: {count} row(s)", context.Speakers.Count());

//            return context.Speakers;
//        }
//    }
//}
