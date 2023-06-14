using GraphQL.Data;
using GraphQL.DataLoader;
using Microsoft.EntityFrameworkCore;

namespace GraphQL
{
    public class Query
    {
        [UseApplicationDbContext]
        public Task<List<Speaker>> GetSpeakers([ScopedService] ApplicationDbContext context) =>
            context.Speakers.ToListAsync();

        public Task<Speaker> GetSpeakerAsync(
            int id,
            SpeakerByIdDataLoader dataLoader,
            CancellationToken cancellationToken) =>
                dataLoader.LoadAsync(id, cancellationToken);
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
