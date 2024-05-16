using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites.UserAssetsEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infra.Data.EnititesConfiguration
{
    public class UserAssetsConfiguration : IEntityTypeConfiguration<UserAssets>
    {
        public void Configure(EntityTypeBuilder<UserAssets> builder)
        {
            builder.ToTable("user_assets", "product");
            
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.Property(p => p.WalletId)
                .HasColumnName("wallet_id");

            builder.Property(p => p.AssetsId)
                .HasColumnName("assets_id");

            builder.Property(p => p.AssetsId)
                .HasColumnName("assets_id");
            
            builder.Property(p => p.BuyPrice)
                .HasColumnName("buy_price");
            
            builder.Property(p => p.AveregePrice)
                .HasColumnName("average_price");
            
            builder.Property(p => p.Amount)
                .HasColumnName("amount");

            builder.Property(p => p.Created_at)
                .HasColumnName("created_at");

            builder.Property(p => p.Deleted_at)
                .HasColumnName("deleted_at");

            builder.Property(p => p.Updated_at)
                .HasColumnName("updated_at");

        }
    }
}