using account_event_sourcing.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace account_event_sourcing.Repository
{
    public class AccountRepository : Repository<Account,Guid>
    {
        private readonly AccountDbContext _accountDbContext;

        public AccountRepository(AccountDbContext accountDbContext)
        {
            _accountDbContext = accountDbContext;
        }

        public async Task<List<Account>> FindAll()
        {
            return await _accountDbContext.Accounts.ToListAsync();
        }

        public async Task<Account> FindById(Guid id)
        {
         return await _accountDbContext.Accounts.FindAsync(id);
        }

        public async Task Save(Account account)
        {
            await _accountDbContext.Accounts.AddAsync(account);
            await _accountDbContext.SaveChangesAsync();
        }

        public async Task Update(Account account)
        {
             _accountDbContext.Accounts.Update(account);
             await _accountDbContext.SaveChangesAsync();
        }
    }
}
