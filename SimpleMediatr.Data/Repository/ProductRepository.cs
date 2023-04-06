using SimpleMediatr.Data.Database;
using SimpleMediatr.Data.Repository.Interfaces;
using SimpleMediatr.Models;

namespace SimpleMediatr.Data.Repository
{
    public sealed class ProductRepository : IProductRepository
    {
        private readonly SimpleMediatrDataContext _dbContext;

        public ProductRepository(SimpleMediatrDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Product> Products => _dbContext.Products;

        public async Task CreateProductAsync(Product product)
        {
            await _dbContext.Products.AddAsync(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Product product)
        {
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateProductAsync(Product product)
        {
            _dbContext.Products.Update(product);
            await _dbContext.SaveChangesAsync();
        }
    }
}
