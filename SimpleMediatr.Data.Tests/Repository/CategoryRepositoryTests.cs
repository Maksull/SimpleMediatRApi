using MockQueryable.Moq;
using SimpleMediatr.Data.Database;
using SimpleMediatr.Data.Repository;
using SimpleMediatr.Models;

namespace SimpleMediatr.Data.Tests.Repository
{
    public sealed class CategoryRepositoryTests
    {
        private readonly Mock<SimpleMediatrDataContext> _context;
        private readonly CategoryRepository _repository;

        public CategoryRepositoryTests()
        {
            _context = new();
            _repository = new(_context.Object);
        }

        [Fact]
        public void Categories_WhenCalled_ReturnCategories()
        {
            //Arrange
            List<Category> categories = new()
            {
                new()
                {
                    CategoryId = 1,
                    Name = "First",
                },
                new()
                {
                    CategoryId = 2,
                    Name = "Second",
                }
            };
            var mock = categories.AsQueryable().BuildMockDbSet();

            _context.Setup(c => c.Categories).Returns(mock.Object);

            //Act
            var result = _repository.Categories.ToList();

            //Assert
            result.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public async void CreateCategory_WhenCalled_CreateCategory()
        {
            //Arrange
            Category category = new()
            {
                CategoryId = 3,
                Name = "Third",
            };
            List<Category> categories = new()
            {
                category,
                new()
                {
                    CategoryId = 2,
                    Name = "Second",
                }
            };
            var mock = categories.AsQueryable().BuildMockDbSet();

            _context.Setup(c => c.Categories).Returns(mock.Object);

            //Act
            await _repository.CreateCategoryAsync(category);

            //Assert
            _context.Verify(c => c.Categories.AddAsync(category, It.IsAny<CancellationToken>()), Times.Once());
            _context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }


        [Fact]
        public async void UpdateCategory_WhenCalled_UpdateCategory()
        {
            //Arrange
            Category category = new()
            {
                CategoryId = 1,
                Name = "First",
            };
            List<Category> categories = new()
            {
                category,
                new()
                {
                    CategoryId = 2,
                    Name = "Second",
                }
            };
            var mock = categories.AsQueryable().BuildMockDbSet();

            _context.Setup(c => c.Categories).Returns(mock.Object);

            //Act
            await _repository.UpdateCategoryAsync(category);

            //Assert
            _context.Verify(c => c.Categories.Update(category), Times.Once());
            _context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }

        [Fact]
        public async void DeleteCategory_WhenCalled_DeleteCategory()
        {
            //Arrange
            Category category = new()
            {
                CategoryId = 1,
                Name = "First",
            };
            List<Category> categories = new()
            {
                category,
                new()
                {
                    CategoryId = 2,
                    Name = "Second",
                }
            };
            var mock = categories.AsQueryable().BuildMockDbSet();

            _context.Setup(c => c.Categories).Returns(mock.Object);

            //Act
            await _repository.DeleteCategoryAsync(category);

            //Assert
            _context.Verify(c => c.Categories.Remove(category), Times.Once());
            _context.Verify(c => c.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once());
        }
    }
}
