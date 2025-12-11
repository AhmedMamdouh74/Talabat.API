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
                a.Property(p => p.Street).HasColumnName("Street").IsRequired().HasMaxLength(180);
                a.Property(p => p.City).HasColumnName("City").IsRequired().HasMaxLength(100);
                a.Property(p => p.FName).HasColumnName("FName").IsRequired().HasMaxLength(60);
                a.Property(p => p.LName).HasColumnName("LName").IsRequired().HasMaxLength(60);

            });

            builder.Property(o => o.Status).HasConversion(
                oStatus => oStatus.ToString(),
                OStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), OStatus));

            builder.Property(o => o.Subtotal).HasColumnType("decimal(18,2)");
            builder.ToTable("Orders");


        }
    }
}
