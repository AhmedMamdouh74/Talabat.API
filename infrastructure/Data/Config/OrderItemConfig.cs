using Domain.Entities.Order_Aggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace infrastructure.Data.Config
{
    internal class OrderItemConfig : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.Property(oi => oi.Price).HasColumnType("decimal(18,2)");
            builder.ToTable("OrderItems");
            builder.OwnsOne(oi => oi.Product, po =>
            {
                po.WithOwner(); // 1:1 [total]
                po.Property(p => p.ProductItemId).HasColumnName("ProductItemId").IsRequired();
                po.Property(p => p.ProductName).HasColumnName("ProductName").IsRequired().HasMaxLength(100);
                po.Property(p => p.PictureUrl).HasColumnName("PictureUrl").IsRequired().HasMaxLength(180);
            });

        }

    }
}
