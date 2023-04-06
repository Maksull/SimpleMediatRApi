using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMediatr.Models;

namespace SimpleMediatr.Data.Configurations
{
    public sealed class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(
                new Product
                {
                    ProductId = 1,
                    Name = "Kayak",
                    Price = 275,
                    CategoryId = 1,
                },
                new Product
                {
                    ProductId = 2,
                    Name = "Lifejacket",
                    Price = 48.95m,
                    CategoryId = 1,
                },
                new Product
                {
                    ProductId = 3,
                    Name = "Ball",
                    Price = 19.50m,
                    CategoryId = 2,
                },
                new Product
                {
                    ProductId = 4,
                    Name = "Corner Flags",
                    Price = 34.95m,
                    CategoryId = 2,
                },
                new Product
                {
                    ProductId = 5,
                    Name = "Stadium",
                    Price = 79500,
                    CategoryId = 2,
                },
                new Product
                {
                    ProductId = 6,
                    Name = "Thinking Cap",
                    Price = 16,
                    CategoryId = 3,
                },
                new Product
                {
                    ProductId = 7,
                    Name = "Unsteady Chair",
                    Price = 29.95m,
                    CategoryId = 3,
                },
                new Product
                {
                    ProductId = 8,
                    Name = "Human Chess Board",
                    Price = 75,
                    CategoryId = 3,
                },
                new Product
                {
                    ProductId = 9,
                    Name = "T-shirt",
                    Price = 1200,
                    CategoryId = 3,
                });
        }
    }
}
