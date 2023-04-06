using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using SimpleMediatr.Data.UnitOfWorks;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;

namespace SimpleMediatr.Services
{
    public sealed class ProductService : IProductService
    {
        private const string productsListCacheKey = "productsList";
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache _cache;

        public ProductService(IUnitOfWork unitOfWork, IMemoryCache cache)
        {
            _unitOfWork = unitOfWork;
            _cache = cache;
        }

        public IEnumerable<Product> GetProducts()
        {
            if (_cache.TryGetValue(productsListCacheKey, out List<Product> products))
            {
                return products;
            }

            if (_unitOfWork.Product.Products.Any())
            {
                products = _unitOfWork.Product.Products.ToList();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                                                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                                                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                                                .SetPriority(CacheItemPriority.Normal)
                                                .SetSize(1024);

                _cache.Set(productsListCacheKey, products, cacheEntryOptions);

                return products;
            }

            return Enumerable.Empty<Product>();
        }

        public async Task<Product?> GetProduct(long id)
        {
            if (_cache.TryGetValue($"ProductId={id}", out Product? product))
            {
                return product;
            }

            if (_unitOfWork.Product.Products.Any())
            {
                product = await _unitOfWork.Product.Products.FirstOrDefaultAsync(p => p.ProductId == id);

                if (product != null)
                {
                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                                                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                                                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                                                .SetPriority(CacheItemPriority.Normal)
                                                .SetSize(1024);

                    _cache.Set($"ProductId={product.ProductId}", product, cacheEntryOptions);

                    return product;
                }
            }

            return null;
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _unitOfWork.Product.CreateProductAsync(product);

            return product;
        }

        public async Task<Product?> UpdateProduct(Product product)
        {
            if (await _unitOfWork.Product.Products.ContainsAsync(product))
            {
                await _unitOfWork.Product.UpdateProductAsync(product);

                return product;
            }

            return null;
        }

        public async Task<Product?> DeleteProduct(long id)
        {
            if (_unitOfWork.Product.Products.Any())
            {
                var product = await _unitOfWork.Product.Products.FirstOrDefaultAsync(p => p.ProductId == id);

                if (product != null)
                {
                    await _unitOfWork.Product.DeleteProductAsync(product);

                    return product;
                }
            }

            return null;
        }

        public async Task<Product> EventOccured(Product product, string message)
        {
            var p = await GetProduct(product.ProductId);
            p.Name = $"{p.Name} evt: {message}";

            await UpdateProduct(p);

            return p;
        }
    }
}
