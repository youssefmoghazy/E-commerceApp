using Domain.Models.OrderModels;

namespace Persistance.Configratoins;

public class DelivaryMethodConfigrations : IEntityTypeConfiguration<DeliveryMethod>
{
    public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
    {
        builder.ToTable("DeliverMethods");
        builder.Property(d => d.Price)
            .HasColumnType("decimal(8,2)");
    }
}
