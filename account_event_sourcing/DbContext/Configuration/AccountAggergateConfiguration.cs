using account_event_sourcing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace account_event_sourcing
{
    public class AccountAggergateConfiguration : IEntityTypeConfiguration<AccountAggergate>
    {
        public void Configure(EntityTypeBuilder<AccountAggergate> builder)
        {
            builder.ToTable("AccountAggergate", "BANK");
            builder.HasKey(a => a.Id);
            builder.Property(a => a.CreateDate).HasColumnType("DateTime").IsRequired();
            builder.Property(a => a.UpdateDate).HasColumnType("DateTime");
            builder.Property(a => a.AccountNumber).HasColumnType("varchar").IsRequired();
            builder.Property(a => a.OwnerName).HasColumnType("varchar").HasMaxLength(200).IsRequired();
            builder.Property(a => a.OwnerEmail).HasColumnType("varchar").HasMaxLength(30).IsRequired();
            builder.Property(a => a.OwnerPhone).HasColumnType("varchar").HasMaxLength(9).IsRequired();
            builder.Property(a => a.OwnerID).HasColumnType("varchar").HasMaxLength(12).IsRequired();
            builder.HasMany<Account>(a => a.AccountList).WithOne(b => b.AccountAggergate).HasForeignKey(b => b.AccountAggergateId);
        }
    }
}
