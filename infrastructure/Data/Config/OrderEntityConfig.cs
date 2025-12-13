using Domain.Entities.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Data.Config
{
    internal class OrderEntityConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.OwnsOne(o => o.ShipToAddress, a =>
            {

                a.WithOwner(); // 1:1 [total]
                a.Property(p => p.Street).IsRequired().HasMaxLength(180);
                a.Property(p => p.City).IsRequired().HasMaxLength(100);
                a.Property(p => p.FirstName).IsRequired().HasMaxLength(60);
                a.Property(p => p.LastName).IsRequired().HasMaxLength(60);

            });

            builder.Property(o => o.Status).HasConversion(
                oStatus => oStatus.ToString(),
                OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));

            builder.Property(o => o.Subtotal).HasColumnType("decimal(18,2)");
            builder.HasOne(o => o.DeliveryMethod).WithMany().OnDelete(DeleteBehavior.SetNull);



        }
    }
}
