using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SimpleMediatr.Data.UnitOfWorks;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.Services
{
    public sealed class CategoryService : ICategoryService
    {
        private const string categoriesListCacheKey = "categoriesList";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public CategoryService(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public IEnumerable<Category> GetCategories()
        {
            if (_cache.TryGetValue(categoriesListCacheKey, out List<Category> categories))
            {
                return categories;
            }

            if (_unitOfWork.Category.Categories.Any())
            {
                categories = _unitOfWork.Category.Categories.ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                                                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                                                .SetPriority(CacheItemPriority.Normal)
                                                .SetSize(1024);

                _cache.Set(categoriesListCacheKey, categories, cacheEntryOptions);

                return categories;
            }

            return Enumerable.Empty<Category>();
        }

        public async Task<Category?> GetCategory(long id)
        {
            if (_cache.TryGetValue($"CategoryId={id}", out Category? category))
            {
                return category;
            }

            if (_unitOfWork.Category.Categories.Any())
            {
                category = await _unitOfWork.Category.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

                if (category != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                                                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                                                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                                                .SetPriority(CacheItemPriority.Normal)
                                                .SetSize(1024);

                    _cache.Set($"CategoryId={category.CategoryId}", category, cacheEntryOptions);

                    return category;
                }
            }

            return null;
        }

        public async Task<Category> CreateCategory(Category category)
        {
            await _unitOfWork.Category.CreateCategoryAsync(category);

            return category;
        }

        public async Task<Category?> UpdateCategory(Category category)
        {
            if (await _unitOfWork.Category.Categories.ContainsAsync(category))
            {
                await _unitOfWork.Category.UpdateCategoryAsync(category);

                return category;
            }

            return null;
        }

        public async Task<Category?> DeleteCategory(long id)
        {
            if (_unitOfWork.Category.Categories.Any())
            {
                var category = await _unitOfWork.Category.Categories.FirstOrDefaultAsync(c => c.CategoryId == id);

                if (category != null)
                {
                    await _unitOfWork.Category.DeleteCategoryAsync(category);

                    return category;
                }
            }

            return null;
        }
    }
}
