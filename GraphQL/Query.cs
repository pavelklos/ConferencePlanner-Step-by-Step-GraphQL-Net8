using GraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphQL
{
    public class Query
    {
        [UseApplicationDbContext]
        public Task<List<Speaker>> GetSpeakers(
            [ScopedService] ApplicationDbContext context) =>
            context.Speakers.ToListAsync();
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
