using SimpleMediatr.Models;

namespace SimpleMediatr.Services.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetProducts();
        Task<Product?> GetProduct(long id);
        Task<Product> CreateProduct(Product product);
        Task<Product?> UpdateProduct(Product product);
        Task<Product?> DeleteProduct(long id);
        Task<Product> EventOccured(Product product, string message);
    }
}
