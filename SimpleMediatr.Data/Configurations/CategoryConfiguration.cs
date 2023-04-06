using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimpleMediatr.Models;

namespace SimpleMediatr.Data.Configurations
{
    public sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(
                new Category
                {
                    CategoryId = 1,
                    Name = "Watersports",
                },
                new Category
                {
                    CategoryId = 2,
                    Name = "Football",
                },
                new Category
                {
                    CategoryId = 3,
                    Name = "Chess",
                });
        }
    }
}
