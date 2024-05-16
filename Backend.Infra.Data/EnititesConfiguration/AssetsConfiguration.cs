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
    public class AssetsConfiguration : IEntityTypeConfiguration<Assets>
    {
        public void Configure(EntityTypeBuilder<Assets> builder)
        {
            builder.ToTable("assets", "product");

            builder.HasKey(k => k.Id);

            builder.Property(p => p.Id)
                .HasColumnName("id");

            builder.Property(p => p.CodName)
                .HasColumnName("cod_name")
                .HasMaxLength(10)
                .IsRequired();

            builder.Property(p => p.CurrentPrice)
                .HasColumnName("current-price")
                .HasPrecision(10, 2)
                .IsRequired();

            builder.Property(p => p.SourceTypeAssets)
                .HasColumnName("source_type_assets")
                .IsRequired();
                
            builder.Property(p => p.SourceCreate)
                .HasColumnName("source_create");

            builder.Property(p => p.Created_at)
                .HasColumnName("created_at");

            builder.Property(p => p.Deleted_at)
                .HasColumnName("deleted_at");

            builder.Property(p => p.Updated_at)
                .HasColumnName("updated_at");
        }
    }
}