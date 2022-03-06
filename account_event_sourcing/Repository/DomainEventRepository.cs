using account_event_sourcing.Domain.Event;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace account_event_sourcing.Repository
{
    public class DomainEventRepository : Repository<DomainEvent,long>
    {

        private readonly AccountDbContext _accountDbContext;

        public DomainEventRepository(AccountDbContext accountDbContext)
        {
            _accountDbContext = accountDbContext;

        }

        public async Task<List<DomainEvent>> FindAll()
        {
            return await _accountDbContext.DomainEvents.ToListAsync();
        }

        public async Task<DomainEvent> FindById(long id)
        {
            return await _accountDbContext.DomainEvents.FindAsync(id);
        }

        public async Task Save(DomainEvent t)
        {
            await _accountDbContext.DomainEvents.AddAsync(t);
            await _accountDbContext.SaveChangesAsync();
        }

        public async Task Update(DomainEvent t)
        {
             _accountDbContext.DomainEvents.Update(t);
            await _accountDbContext.SaveChangesAsync();

        }

        public async Task<List<DomainEvent>> FindAllByaccountAggergateId(Guid accountAggergateId)
        {
            var events = await _accountDbContext.DomainEvents.
                Where(a => a.AccountAggergateId.Equals(accountAggergateId))
                .OrderBy(c => c.CreateDate)
                .ToListAsync();
            return events;
        }
    }
}
