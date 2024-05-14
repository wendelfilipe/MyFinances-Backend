using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.WalletEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infra.Data.EnititesConfiguration
{
    public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
    {
        public void Configure(EntityTypeBuilder<Wallet> builder)
        {
            builder.ToTable("wallet", "product");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.Property(p => p.Name)
                .HasColumnName("name")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(p => p.UserId)
                .HasColumnName("user_id")
                .IsRequired();
                
            builder.Property(p => p.SourceCreate)
                .HasColumnName("source_create")
                .IsRequired();

            builder.Property(p => p.Created_at)
                .HasColumnName("created_at")
                .IsRequired();

            builder.Property(p => p.Deleted_at)
                .HasColumnName("deleted_at");

            builder.Property(p => p.Updated_at)
                .HasColumnName("updated_at")
                .IsRequired();

            builder.HasOne(p => p.Assets)
                .WithMany(p => p.Wallets)
                .HasForeignKey(p => p.AssetsId);

        }
    }
}