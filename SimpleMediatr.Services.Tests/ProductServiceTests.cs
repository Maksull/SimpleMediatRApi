using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using MockQueryable.Moq;
using Moq;
using SimpleMediatr.Data.UnitOfWorks;
using SimpleMediatr.Models;

namespace SimpleMediatr.Services.Tests
{
    public sealed class ProductServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly MemoryCache _cache;
        private readonly ProductService _productService;

        public ProductServiceTests()
        {
            _unitOfWork = new();
            _cache = new(new MemoryCacheOptions());
            _productService = new(_unitOfWork.Object, _cache);
        }

        #region GetProducts

        [Fact]
        public void GetProducts_WhenCalled_ReturnProducts()
        {
            //Arrange
            List<Product> products = new ()
            {
                new()
                {
                    ProductId = 1,
                    Name = "First",
                    Price = 1,
                    CategoryId = 1,
                },
                new()
                {
                    ProductId = 2,
                    Name = "Second",
                    Price = 2,
                    CategoryId = 2,
                }
            };

            _unitOfWork.Setup(u => u.Product.Products).Returns(products.AsQueryable());

            //Act
            var result = _productService.GetProducts()?.ToList();

            //Assert
            result.Should().BeOfType<List<Product>>();
            result.Should().HaveCount(2);
        }

        [Fact]
        public void GetProducts_WhenThereIsCache_ReturnProducts()
        {
            //Arrange
            List<Product> products = new()
            {
                new()
                {
                    ProductId = 1,
                    Name = "First",
                    Price = 1,
                    CategoryId = 1,
                },
                new()
                {
                    ProductId = 2,
                    Name = "Second",
                    Price = 2,
                    CategoryId = 2,
                }
            };

            _unitOfWork.Setup(u => u.Product.Products).Returns(products.AsQueryable());
            _cache.Set("productsList", products);

            //Act
            var result = _productService.GetProducts()?.ToList();

            //Assert
            result.Should().BeOfType<List<Product>>();
            result.Should().BeEquivalentTo(products);
        }

        [Fact]
        public void GetProducts_WhenCalled_ReturnEmpty()
        {
            //Arrange
            List<Product> products = new() { };

            _unitOfWork.Setup(u => u.Product.Products).Returns(products.AsQueryable());

            //Act
            var result = _productService.GetProducts();

            //Assert
            result.Should().BeEmpty();
        }

        #endregion

        #region GetProduct

        [Fact]
        public void GetProduct_WhenCalled_ReturnProduct()
        {
            //Arrange
            var products = new List<Product>()
            {
                new()
                {
                    ProductId = 1,
                    Name = "First",
                    Price = 1,
                    CategoryId = 1,
                },
                new()
                {
                    ProductId = 2,
                    Name = "Second",
                    Price = 2,
                    CategoryId = 2,
                }
            };

            //build mock by extension
            var mock = products.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Product.Products).Returns(mock);

            //Act
            var result = _productService.GetProduct(1).Result;

            //Assert
            result.Should().BeOfType<Product>();
            result.Should().BeEquivalentTo(products[0]);
        }

        [Fact]
        public void GetProduct_WhenThereIsCache_ReturnProduct()
        {
            //Arrange
            var products = new List<Product>()
            {
                new()
                {
                    ProductId = 1,
                    Name = "First",
                    Price = 1,
                    CategoryId = 1,
                },
                new()
                {
                    ProductId = 2,
                    Name = "Second",
                    Price = 2,
                    CategoryId = 2,
                }
            };

            //build mock by extension
            var mock = products.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Product.Products).Returns(mock);
            _cache.Set($"ProductId={1}", products[0]);

            //Act
            var result = _productService.GetProduct(1).Result;

            //Assert
            result.Should().BeOfType<Product>();
            result.Should().BeEquivalentTo(products[0]);
        }

        [Fact]
        public void GetProduct_WhenCalled_ReturnNull()
        {
            //Arrange
            List<Product> products = new() { };

            //build mock by extension
            var mock = products.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Product.Products).Returns(mock);

            //Act
            var result = _productService.GetProduct(1).Result;

            //Assert
            result.Should().BeNull();
        }

        #endregion

        #region CreateProduct

        [Fact]
        public void CreateProduct_WhenCalled_ReturnProduct()
        {
            //Arrange
            Product product = new()
            {
                ProductId = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };

            _unitOfWork.Setup(u => u.Product.CreateProductAsync(It.IsAny<Product>())).Returns(Task.FromResult(product));

            //Act
            var result = _productService.CreateProduct(product).Result;

            //Assert
            result.Should().BeOfType<Product>();
            result.Should().BeEquivalentTo(product);
        }

        #endregion

        #region UpdateProduct

        [Fact]
        public void UpdateProduct_WhenCalled_IfProductExist_ReturnProduct()
        {
            //Arrange
            Product product = new()
            {
                ProductId = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };
            var products = new List<Product>() { product };

            var mock = products.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Product.Products).Returns(mock);
            _unitOfWork.Setup(u => u.Product.UpdateProductAsync(It.IsAny<Product>())).Returns(Task.FromResult(product));

            //Act
            var result = _productService.UpdateProduct(product).Result;

            //Assert
            result.Should().BeOfType<Product>();
            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void UpdateProduct_WhenCalled_IfProductNotExist_ReturnNull()
        {
            //Arrange
            Product product = new()
            {
                ProductId = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };
            var products = new List<Product>();

            var mock = products.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Product.Products).Returns(mock);
            _unitOfWork.Setup(u => u.Product.UpdateProductAsync(It.IsAny<Product>())).Returns(Task.FromResult(product));

            //Act
            var result = _productService.UpdateProduct(product).Result;

            //Assert
            result.Should().BeNull();
        }

        #endregion

        #region DeleteProduct

        [Fact]
        public void DeleteProduct_WhenCalled_IfProductExist_ReturnProduct()
        {
            //Arrange
            Product product = new()
            {
                ProductId = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };
            var products = new List<Product>() { product };

            var mock = products.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Product.Products).Returns(mock);
            _unitOfWork.Setup(u => u.Product.DeleteProductAsync(It.IsAny<Product>())).Returns(Task.FromResult(product));

            //Act
            var result = _productService.DeleteProduct(1).Result;

            //Assert
            result.Should().BeOfType<Product>();
            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void DeleteProduct_WhenCalled_IfProductDoesNotExist_ReturnNull()
        {
            //Arrange
            Product product = new()
            {
                ProductId = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };
            var products = new List<Product>();

            var mock = products.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Product.Products).Returns(mock);
            _unitOfWork.Setup(u => u.Product.DeleteProductAsync(It.IsAny<Product>())).Returns(Task.FromResult(product));

            //Act
            var result = _productService.DeleteProduct(1).Result;

            //Assert
            result.Should().BeNull();
        }

        #endregion

        [Fact]
        public void EventOccured_WhenCalled_ReturnUpdatedProduct()
        {
            //Arrange
            Product product = new()
            {
                ProductId = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1,
            };
            List<Product> products = new()
            {
                new()
                {
                    ProductId = 1,
                    Name = "First",
                    Price = 1,
                    CategoryId = 1,
                },
                new()
                {
                    ProductId = 2,
                    Name = "Second",
                    Price = 2,
                    CategoryId = 2,
                }
            };

            var mock = products.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Product.Products).Returns(mock);

            //Act
            var result = _productService.EventOccured(product, "Test message").Result;

            //Assert
            result.Name.Should().Be("First evt: Test message");
        }
    }
}
