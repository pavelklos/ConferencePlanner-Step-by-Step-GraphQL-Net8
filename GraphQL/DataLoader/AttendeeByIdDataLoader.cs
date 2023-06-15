﻿using GraphQL.Data;
using Microsoft.EntityFrameworkCore;

namespace GraphQL.DataLoader
{
    public class AttendeeByIdDataLoader : BatchDataLoader<int, Attendee>
    {
        private readonly IDbContextFactory<ApplicationDbContext> _dbContextFactory;

        public AttendeeByIdDataLoader(
            IBatchScheduler batchScheduler,
            IDbContextFactory<ApplicationDbContext> dbContextFactory)
            : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Attendee>> LoadBatchAsync(
            IReadOnlyList<int> keys,
            CancellationToken cancellationToken)
        {
            await using ApplicationDbContext dbContext =
                _dbContextFactory.CreateDbContext();

            return await dbContext.Attendees
                .Where(s => keys.Contains(s.Id))
                .ToDictionaryAsync(t => t.Id, cancellationToken);
        }
    }
}