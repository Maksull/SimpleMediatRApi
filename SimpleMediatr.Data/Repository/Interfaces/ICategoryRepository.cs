using SimpleMediatr.Models;

namespace SimpleMediatr.Data.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        IQueryable<Category> Categories { get; }

        Task CreateCategoryAsync(Category category);
        Task DeleteCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category);
    }
}