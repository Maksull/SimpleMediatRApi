using FluentAssertions;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using SimpleMediatr.Contracts.Controllers.Categories;
using SimpleMediatr.Mapster;
using SimpleMediatr.MediatR.Commands.Categories;
using SimpleMediatr.MediatR.Queries.Categories;
using SimpleMediatr.Models;
using SimpleMediatR.Endpoints;

namespace SimpleMediatR.Tests.Endpoints
{
    public sealed class CategoriesEndpointsTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly IMapper _mapper;

        public CategoriesEndpointsTests()
        {
            _mediator = new();
            _mapper = GetMapper();
        }

        #region GetCategories

        [Fact]
        public void GetCategories_WhenCalled_ReturnOk()
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

            _mediator.Setup(m => m.Send(It.IsAny<GetCategoriesQuery>(), default))
                .ReturnsAsync(categories);

            //Act
            var response = CategoriesEndpoints.GetCategories(_mediator.Object).Result;
            var result = response as Ok<IEnumerable<Category>>;

            //Assert
            result.Value.Should().BeOfType<List<Category>>();
            result.Value.Should().NotBeEmpty();
            result.Value.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public void GetCategories_WhenCalled_ReturnNotFound()
        {
            //Arrange
            List<Category> categories = new() { };

            _mediator.Setup(m => m.Send(It.IsAny<GetCategoriesQuery>(), default))
                .ReturnsAsync(categories);

            //Act
            var response = CategoriesEndpoints.GetCategories(_mediator.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void GetCategories_WhenException_ReturnProblem()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetCategoriesQuery>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = CategoriesEndpoints.GetCategories(_mediator.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion


        #region GetCategory

        [Fact]
        public void GetCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            Category category = new()
            {
                CategoryId = 1,
                Name = "First",
            };

            _mediator.Setup(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), default))
                .ReturnsAsync(category);

            //Act
            var response = CategoriesEndpoints.GetCategory(1, _mediator.Object).Result;
            var result = response as Ok<Category>;

            //Assert
            result.Value.Should().BeOfType<Category>();
            result.Value.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void GetCategory_WhenCalled_ReturnNotFound()
        {
            //Arrange
            Category category = new()
            {
                CategoryId = 1,
                Name = "First",
            };

            _mediator.Setup(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), default))
                .ReturnsAsync((Category)null);

            //Act
            var response = CategoriesEndpoints.GetCategory(1, _mediator.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void GetCategory_WhenException_ReturnProblem()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = CategoriesEndpoints.GetCategory(1, _mediator.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion

        #region CreateCategory

        [Fact]
        public void CreateCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            CreateCategoryDto createCategory = new()
            {
                Name = "First",
            };

            _mediator.Setup(m => m.Send(It.IsAny<CreateCategoryCommand>(), default))
                .ReturnsAsync(new Category()
                {
                    CategoryId = 1,
                    Name = "First",
                });

            //Act
            var response = CategoriesEndpoints.CreateCategory(createCategory, _mediator.Object, _mapper).Result;
            var result = response as Ok;

            //Assert
            result.Should().BeOfType<Ok>();
        }

        [Fact]
        public void CreateCategory_WhenException_ReturnProblem()
        {
            //Arrange
            CreateCategoryDto createCategory = new()
            {
                Name = "First",
            };

            _mediator.Setup(m => m.Send(It.IsAny<CreateCategoryCommand>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = CategoriesEndpoints.CreateCategory(createCategory, _mediator.Object, _mapper).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion


        #region UpdateCategory

        [Fact]
        public void UpdateCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            UpdateCategoryDto updateCategory = new()
            {
                CategoryId = 1,
                Name = "First",
            };

            _mediator.Setup(m => m.Send(It.IsAny<UpdateCategoryCommand>(), default))
                .ReturnsAsync(new Category()
                {
                    CategoryId = 1,
                    Name = "First",
                });

            //Act
            var response = CategoriesEndpoints.UpdateCategory(updateCategory, _mediator.Object, _mapper).Result;
            var result = response as Ok;

            //Assert
            result.Should().BeOfType<Ok>();
        }

        [Fact]
        public void UpdateCategory_WhenCalled_ReturnNotFound()
        {
            //Arrange
            UpdateCategoryDto updateCategory = new()
            {
                CategoryId = 1,
                Name = "First",
            };

            _mediator.Setup(m => m.Send(It.IsAny<UpdateCategoryCommand>(), default))
                .ReturnsAsync((Category)null);

            //Act
            var response = CategoriesEndpoints.UpdateCategory(updateCategory, _mediator.Object, _mapper).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void UpdateCategory_WhenException_ReturnProblem()
        {
            //Arrange
            UpdateCategoryDto updateCategory = new()
            {
                CategoryId = 1,
                Name = "First",
            };

            _mediator.Setup(m => m.Send(It.IsAny<UpdateCategoryCommand>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = CategoriesEndpoints.UpdateCategory(updateCategory, _mediator.Object, _mapper).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion


        #region DeleteCategory

        [Fact]
        public void DeleteCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<DeleteCategoryCommand>(), default))
                .ReturnsAsync(new Category()
                {
                    CategoryId = 1,
                    Name = "First",
                });

            //Act
            var response = CategoriesEndpoints.DeleteCategory(1, _mediator.Object).Result;
            var result = response as Ok;

            //Assert
            response.Should().BeOfType<Ok>();
        }

        [Fact]
        public void DeleteCategory_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<DeleteCategoryCommand>(), default))
                .ReturnsAsync((Category)null);

            //Act
            var response = CategoriesEndpoints.DeleteCategory(1, _mediator.Object).Result;
            var result = response as NotFound;

            //Assert
            response.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void DeleteCategory_WhenException_ReturnProblem()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<DeleteCategoryCommand>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = CategoriesEndpoints.DeleteCategory(1, _mediator.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            response.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion

        private Mapper GetMapper()
        {
            TypeAdapterConfig config = new TypeAdapterConfig();
            config.Apply(new MapsterRegister());

            return new Mapper(config);
        }
    }
}
