using MockQueryable.Moq;
using SimpleMediatr.Data.Database;
using SimpleMediatr.Data.Repository;
using SimpleMediatr.Models;

namespace SimpleMediatr.Data.Tests.Repository
{
    public sealed class ProductRepositoryTests
    {
        private readonly Mock<SimpleMediatrDataContext> _context;
        private readonly ProductRepository _repository;

        public ProductRepositoryTests()
        {
            _context = new Mock<SimpleMediatrDataContext>();
            _repository = new(_context.Object);
        }

        [Fact]
        public void Products_WhenCalled_ReturnProducts()
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
            var mock = products.AsQueryable().BuildMock().BuildMockDbSet();

            _context.Setup(c => c.Products).Returns(mock.Object);

            //Act
            var result = _repository.Products.ToList();

            //Assert
            result.Should().BeEquivalentTo(products);
        }

        [Fact]
        public async void CreateProductAsync_WhenCalled_AddProduct()
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
                product,
                new()
                {
                    ProductId = 2,
                    Name = "Second",
                    Price = 2,
                    CategoryId = 2,
                }
            };
            var mock = products.AsQueryable().BuildMock().BuildMockDbSet();

            _context.Setup(c => c.Products).Returns(mock.Object);

            //Act
            await _repository.CreateProductAsync(product);

            //Assert
            _context.Verify(c => c.Products.AddAsync(product, It.IsAny<CancellationToken>()), Times.Once());
            _context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void UpdateProduct_WhenCalled_UpdateProduct()
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
                product,
                new()
                {
                    ProductId = 2,
                    Name = "Second",
                    Price = 2,
                    CategoryId = 2,
                }
            };
            var mock = products.AsQueryable().BuildMockDbSet();

            _context.Setup(c => c.Products).Returns(mock.Object);

            //Act
            await _repository.UpdateProductAsync(product);

            //Assert
            _context.Verify(c => c.Products.Update(product), Times.Once());
            _context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void DeleteProduct_WhenCalled_DeleteProduct()
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
                product,
                new()
                {
                    ProductId = 2,
                    Name = "Second",
                    Price = 2,
                    CategoryId = 2,
                }
            };
            var mock = products.AsQueryable().BuildMockDbSet();

            _context.Setup(c => c.Products).Returns(mock.Object);

            //Act
            await _repository.DeleteProductAsync(product);

            //Assert
            _context.Verify(c => c.Products.Remove(product), Times.Once());
            _context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
