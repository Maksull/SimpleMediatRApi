using FluentAssertions;
using Microsoft.Extensions.Caching.Memory;
using MockQueryable.Moq;
using Moq;
using SimpleMediatr.Data.UnitOfWorks;
using SimpleMediatr.Models;

namespace SimpleMediatr.Services.Tests
{
    public sealed class CategoryServiceTests
    {
        private readonly Mock<IUnitOfWork> _unitOfWork;
        private readonly MemoryCache _cache;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _unitOfWork = new();
            _cache = new(new MemoryCacheOptions());
            _categoryService = new(_unitOfWork.Object, _cache);
        }

        #region GetCategories

        [Fact]
        public void GetCategories_WhenCalled_ReturnCategories()
        {
            //Arrange
            List<Category> categories = new() {
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
            _unitOfWork.Setup(u => u.Category.Categories).Returns(categories.AsQueryable());

            //Act
            var result = _categoryService.GetCategories()?.ToList();

            //Assert
            result.Should().BeOfType<List<Category>>();
            result.Should().HaveCount(2);
        }

        [Fact]
        public void GetCategories_WhenThereIsCache_ReturnCategories()
        {
            //Arrange
            List<Category> categories = new() {
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
            _unitOfWork.Setup(u => u.Category.Categories).Returns(categories.AsQueryable());
            _cache.Set("categoriesList", categories);

            //Act
            var result = _categoryService.GetCategories()?.ToList();

            //Assert
            result.Should().BeOfType<List<Category>>();
            result.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public void GetCategories_WhenCalled_ReturnEmpty()
        {
            //Arrange
            List<Category> categories = new() { };
            _unitOfWork.Setup(u => u.Category.Categories).Returns(categories.AsQueryable());

            //Act
            var result = _categoryService.GetCategories();

            //Assert
            result.Should().BeEmpty();
        }

        #endregion

        #region GetCategory

        [Fact]
        public void GetCategory_WhenCalled_ReturnCategory()
        {
            //Arrange
            List<Category> categories = new() {
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
            var mock = categories.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Category.Categories).Returns(mock);

            //Act
            var result = _categoryService.GetCategory(1).Result;

            //Assert
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(categories[0]);
        }

        [Fact]
        public void GetCategory_WhenThereIsCache_ReturnCategory()
        {
            //Arrange
            List<Category> categories = new() {
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
            var mock = categories.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Category.Categories).Returns(mock);
            _cache.Set("CategoryId=1", categories[0]);

            //Act
            var result = _categoryService.GetCategory(1).Result;

            //Assert
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(categories[0]);
        }

        [Fact]
        public void GetCategory_WhenCalled_ReturnNull()
        {
            //Arrange
            List<Category> categories = new() { };
            var mock = categories.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Category.Categories).Returns(mock);

            //Act
            var result = _categoryService.GetCategory(1).Result;

            //Assert
            result.Should().BeNull();
        }

        #endregion

        #region CreateCategory

        [Fact]
        public void CreateCategory_WhenCalled_ReturnCategory()
        {
            //Arrange
            Category category = new()
            {
                CategoryId = 1,
                Name = "First",
            };

            _unitOfWork.Setup(u => u.Category.CreateCategoryAsync(It.IsAny<Category>())).Returns(Task.FromResult(category));

            //Act
            var result = _categoryService.CreateCategory(category).Result;

            //Assert
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(category);
        }

        #endregion

        #region UpdateCategory

        [Fact]
        public void UpdateCategroy_WhenCalled_IfCategoryExist_ReturnCategory()
        {
            //Arrange
            Category category = new()
            {
                CategoryId = 1,
                Name = "First",
            };

            List<Category> categories = new() {
                category,
                new()
                {
                    CategoryId = 2,
                    Name = "Second",
                }
            };
            var mock = categories.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Category.Categories).Returns(mock);
            _unitOfWork.Setup(u => u.Category.UpdateCategoryAsync(It.IsAny<Category>())).Returns(Task.FromResult(category));

            //Act
            var result = _categoryService.UpdateCategory(category).Result;

            //Assert
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void UpdateCategory_WhenCalled_IfCategoryNotExist_ReturnNull()
        {
            //Arrange
            Category category = new()
            {
                CategoryId = 1,
                Name = "First",
            };

            List<Category> categories = new();
            var mock = categories.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Category.Categories).Returns(mock);
            _unitOfWork.Setup(u => u.Category.UpdateCategoryAsync(It.IsAny<Category>())).Returns(Task.FromResult(category));

            //Act
            var result = _categoryService.UpdateCategory(category).Result;

            //Assert
            result.Should().BeNull();
        }

        #endregion

        #region DeleteCategory

        [Fact]
        public void DeleteCategory_WhenCalled_IfCategoryExist_ReturnCategory()
        {
            //Arrange
            Category category = new()
            {
                CategoryId = 1,
                Name = "First",
            };

            List<Category> categories = new() {
                category,
                new()
                {
                    CategoryId = 2,
                    Name = "Second",
                }
            };
            var mock = categories.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Category.Categories).Returns(mock);
            _unitOfWork.Setup(u => u.Category.DeleteCategoryAsync(It.IsAny<Category>())).Returns(Task.FromResult(category));

            //Act
            var result = _categoryService.DeleteCategory(1).Result;

            //Assert
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void DeleteCategory_WhenCalled_IfCategoryNotExist_ReturnNull()
        {
            //Arrange
            Category category = new()
            {
                CategoryId = 1,
                Name = "First",
            };

            List<Category> categories = new();
            var mock = categories.AsQueryable().BuildMock();

            _unitOfWork.Setup(u => u.Category.Categories).Returns(mock);
            _unitOfWork.Setup(u => u.Category.DeleteCategoryAsync(It.IsAny<Category>())).Returns(Task.FromResult(category));

            //Act
            var result = _categoryService.DeleteCategory(1).Result;

            //Assert
            result.Should().BeNull();
        }

        #endregion
    }
}
