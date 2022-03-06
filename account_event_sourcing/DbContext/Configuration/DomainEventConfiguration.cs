using account_event_sourcing.Domain;
using account_event_sourcing.Domain.Event;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace account_event_sourcing
{
    public class DomainEventConfiguration : IEntityTypeConfiguration<DomainEvent>
    {
        public void Configure(EntityTypeBuilder<DomainEvent> builder)
        {

            builder.ToTable("DomainEvent", "EVENTSOURCING");
            builder.HasKey(b => b.SequenceId); ;
            builder.Property(b => b.CreateDate).HasColumnType("DateTime").IsRequired();
            builder.Property(b => b.EventJson).HasColumnType("varchar")
                .HasMaxLength(1000).IsRequired();
            builder.HasOne<AccountAggergate>(b=>b.AccountAggergate).
                WithMany(a=>a.DomainEvent).
                HasForeignKey(b => b.AccountAggergateId).
                IsRequired().
                OnDelete(DeleteBehavior.NoAction);
        }
    }
}
