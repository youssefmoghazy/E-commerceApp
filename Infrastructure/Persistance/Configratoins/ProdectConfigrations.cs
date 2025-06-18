
using Domain.Models.Product;

namespace Persistance.Configratoins;

internal class ProdectConfigrations : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasOne(p => p.productBrand)
            .WithMany()
            .HasForeignKey(p => p.BrandId);
        builder.HasOne(p => p.ProductType)
            .WithMany()
            .HasForeignKey(p => p.TypeId);
        builder.Property(p => p.Price)
            .HasColumnType("decimal(8,3)");
    }
}
