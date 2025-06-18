using Domain.Models.OrderModels;

namespace Persistance.Configratoins;

public class OrderItemConfigrations : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.ToTable("OrderItem");
        builder.Property(d => d.Price)
            .HasColumnType("decimal(8,2)");
        builder.OwnsOne(x => x.Product , x => x.WithOwner());
    }
}