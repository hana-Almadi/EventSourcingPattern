using account_event_sourcing.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace account_event_sourcing
{
    public class AccountConfiguration : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.ToTable("ACCOUNT", "BANK");
            builder.HasKey(b => b.AccountId);
            builder.Property(b => b.Money).HasColumnType("int").HasDefaultValueSql("0").IsRequired();
            builder.Property(b => b.CreateDate).HasColumnType("DateTime").IsRequired();
            builder.Property(b => b.ActionType).HasColumnType("int").IsRequired();
            builder.HasOne<AccountAggergate>(b => b.AccountAggergate)
                .WithMany(a => a.AccountList)
                .HasForeignKey(b => b.AccountAggergateId).IsRequired();
        }
    }
}
