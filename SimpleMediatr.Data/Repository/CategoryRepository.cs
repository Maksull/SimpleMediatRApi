using SimpleMediatr.Data.Database;
using SimpleMediatr.Data.Repository.Interfaces;
using SimpleMediatr.Models;

namespace SimpleMediatr.Data.Repository
{
    public sealed class CategoryRepository : ICategoryRepository
    {
        private readonly SimpleMediatrDataContext _dbContext;

        public CategoryRepository(SimpleMediatrDataContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Category> Categories => _dbContext.Categories;

        public async Task CreateCategoryAsync(Category category)
        {
            await _dbContext.Categories.AddAsync(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteCategoryAsync(Category category)
        {
            _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateCategoryAsync(Category category)
        {
            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();
        }
    }
}
