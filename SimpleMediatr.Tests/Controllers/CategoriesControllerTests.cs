using FluentAssertions;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleMediatr.Contracts.Controllers.Categories;
using SimpleMediatr.Mapster;
using SimpleMediatr.MediatR.Commands.Categories;
using SimpleMediatr.MediatR.Queries.Categories;
using SimpleMediatr.Models;
using SimpleMediatR.Controllers;

namespace SimpleMediatr.Tests.Controllers
{
    public sealed class CategoriesControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly IMapper _mapper;
        private readonly CategoriesController _controller;

        public CategoriesControllerTests()
        {
            _mediator = new Mock<IMediator>();
            _mapper = GetMapper();

            _controller = new CategoriesController(_mediator.Object, _mapper);
        }

        #region GetCategories

        [Fact]
        public void GetCategories_WhenCalled_ReturnOk()
        {
            //Arrange
            var categories = new List<Category>()
            {
                new()
                {
                    CategoryId = 1, Name = "First"
                },
                new()
                {
                    CategoryId = 2, Name = "Second"
                },
                new()
                {
                    CategoryId = 3, Name = "Third"
                },
            };
            _mediator.Setup(m => m.Send(It.IsAny<GetCategoriesQuery>(), default))
                .ReturnsAsync(categories);

            //Act
            var response = _controller.GetCategories().Result as OkObjectResult;
            var result = response?.Value as List<Category>;

            //Assert
            response.Should().BeOfType<OkObjectResult>();
            result.Should().BeOfType<List<Category>>();
            result.Should().NotBeNullOrEmpty();
            result.Should().BeEquivalentTo(categories);
        }

        [Fact]
        public void GetCategories_WhenCalled_ReturnNotFound()
        {
            //Arrange
            var categories = new List<Category>();
            _mediator.Setup(m => m.Send(It.IsAny<GetCategoriesQuery>(), default))
                .ReturnsAsync(categories);


            //Act
            var response = _controller.GetCategories().Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetCategories_WhenException_ReturnProblem()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetCategoriesQuery>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.GetCategories().Result as ObjectResult;
            var result = response.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
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
                Name = "First"
            };
            _mediator.Setup(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), default))
                .ReturnsAsync(category);

            //Act
            var response = _controller.GetCategory(1).Result as OkObjectResult;
            var result = response?.Value as Category;

            //Assert
            response.Should().BeOfType<OkObjectResult>();
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(category);
        }

        [Fact]
        public void GetCategory_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), default))
                .ReturnsAsync((Category)null);

            //Act
            var response = _controller.GetCategory(1).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetCategory_WhenException_ReturnProblem()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetCategoryByIdQuery>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.GetCategory(1).Result as ObjectResult;
            var result = response.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion

        #region CreateCategory

        [Fact]
        public void CreateCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            CreateCategoryDto createCategory = new()
            {
                Name = "First"
            };

            _mediator.Setup(m => m.Send(It.IsAny<CreateCategoryCommand>(), default))
                .ReturnsAsync(new Category
                {
                    CategoryId = 1,
                    Name = "First"
                });

            //Act
            var response = _controller.CreateCategory(createCategory).Result;
            var result = response as OkResult;

            //Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void CreateCategory_WhenException_ReturnProblem()
        {
            //Arrange
            CreateCategoryDto createCategory = new()
            {
                Name = "First"
            };

            _mediator.Setup(m => m.Send(It.IsAny<CreateCategoryCommand>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.CreateCategory(createCategory).Result as ObjectResult;
            var result = response.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
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
                .ReturnsAsync(new Category
                {
                    CategoryId = 1,
                    Name = "First"
                });

            //Act
            var response = _controller.UpdateCategory(updateCategory).Result;
            var result = response as OkResult;

            //Assert
            result.Should().BeOfType<OkResult>();
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
            var response = _controller.UpdateCategory(updateCategory).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
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
            var response = _controller.UpdateCategory(updateCategory).Result as ObjectResult;
            var result = response.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion

        #region DeleteCategory

        [Fact]
        public void DeleteCategory_WhenCalled_ReturnOk()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<DeleteCategoryCommand>(), default))
                .ReturnsAsync(new Category
                {
                    CategoryId = 1,
                    Name = "First"
                });

            //Act
            var response = _controller.DeleteCategory(1).Result;
            var result = response as OkResult;

            //Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void DeleteCategory_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<DeleteCategoryCommand>(), default))
                .ReturnsAsync((Category)null);

            //Act
            var response = _controller.DeleteCategory(1).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void DeleteCategory_WhenException_ReturnProblem()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<DeleteCategoryCommand>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.DeleteCategory(1).Result as ObjectResult;
            var result = response.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
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
