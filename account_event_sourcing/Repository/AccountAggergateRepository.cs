using account_event_sourcing.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace account_event_sourcing.Repository
{
    public class AccountAggergateRepository : Repository<AccountAggergate,Guid>
    {
        private readonly AccountDbContext _accountDbContext;
        
        public AccountAggergateRepository(AccountDbContext accountDbContext)
        {
            _accountDbContext=accountDbContext;
        }

        public async Task<List<AccountAggergate>> FindAll()
        {
           return await _accountDbContext.AccountAggergates.ToListAsync();
        }

        public async Task<AccountAggergate> FindById(Guid id)
        {
            return await _accountDbContext.AccountAggergates.
                Where<AccountAggergate>(c => c.AccountNumber.Equals(id))
                           .Include(c => c.AccountList).FirstOrDefaultAsync();
        }

        public async Task Save(AccountAggergate accountAggergate)
        {
            await _accountDbContext.AccountAggergates.AddAsync(accountAggergate);
            await _accountDbContext.SaveChangesAsync();
        }

        public async Task Update(AccountAggergate accountAggergate)
        {
            _accountDbContext.AccountAggergates.Update(accountAggergate);
            await _accountDbContext.SaveChangesAsync();
        }
    }
}
