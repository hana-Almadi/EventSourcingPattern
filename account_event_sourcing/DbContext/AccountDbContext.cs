using account_event_sourcing.Domain;
using account_event_sourcing.Domain.Event;
using Microsoft.EntityFrameworkCore;


namespace account_event_sourcing
{
    public class AccountDbContext: DbContext
    {
        public AccountDbContext(DbContextOptions<AccountDbContext> options): base(options)
        { }
        public DbSet<Account> Accounts { get; set; }
       public DbSet<DomainEvent> DomainEvents { get; set; }
        public DbSet<AccountCreateEvent> AccountCreateEvents { get; set; }

        public DbSet<AccountAggergate> AccountAggergates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new DomainEventConfiguration());
            modelBuilder.ApplyConfiguration(new AccountAggergateConfiguration());
            modelBuilder.Entity<AccountCreateEvent>().HasBaseType<DomainEvent>();
            modelBuilder.Entity<AccountDepositEvent>().HasBaseType<DomainEvent>();
            modelBuilder.Entity<AccountTransferEvent>().HasBaseType<DomainEvent>();

        }
    }
}
