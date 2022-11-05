using DesafioBrainlaw.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DesafioBrainlaw.Infrastructure.Mappings
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder
                .Property(p => p.CreatedOn)
                .HasColumnType("datetime")
                .IsRequired();

            builder
                .Property(p => p.IsDeleted)
                .IsRequired();

            builder
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsRequired();

            builder
                .Property(p => p.Description)
                .HasMaxLength(100);

            builder
                .Property(p => p.Price)
                .HasPrecision(18, 2)
                .IsRequired();

            builder
                .Property(p => p.Quantity)
                .IsRequired();

            builder
                .Ignore(p => p.ValidationResult)
                .Ignore(x => x.CascadeMode)
                .Ignore(x => x.RuleLevelCascadeMode)
                .Ignore(x => x.ClassLevelCascadeMode);

            builder.HasQueryFilter(p => p.IsDeleted == false);
        }
    }
}