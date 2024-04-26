using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Domain.Entites;
using Backend.Domain.Entites.AssetsEntites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Backend.Infra.Data.EnititesConfiguration
{
    public class JoinAssetsWalletConfiguration : IEntityTypeConfiguration<JoinAssetWallet>
    {
        public void Configure(EntityTypeBuilder<JoinAssetWallet> builder)
        {
            builder.ToTable("JoinAssetsWallet", "product");

            builder.Property(p => p.WalletId)
                .HasColumnName("wallet_id");

            builder.Property(p => p.AssetId)
                .HasColumnName("assets_id");

            builder.HasKey(aw => new 
            {
                aw.AssetId,
                aw.WalletId
            });

            builder.HasOne(o => o.Wallet)
                .WithMany(o => o.JoinAssetWallets)
                .HasForeignKey(o => o.WalletId);


        }
    }
}