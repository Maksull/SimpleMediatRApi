using FluentAssertions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using SimpleMediatr.Contracts.Controllers.Products;
using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.MediatR.Queries.Products;
using SimpleMediatr.Models;
using SimpleMediatR.Controllers;

namespace SimpleMediatr.Tests.Controllers
{
    public sealed class ProductsControllerTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IMapper> _mapper;
        private readonly ProductsController _controller;


        public ProductsControllerTests()
        {
            _mediator = new();
            _mapper = new();
            _controller = new ProductsController(_mediator.Object, _mapper.Object);
        }

        #region GetProducts

        [Fact]
        public void GetProducts_WhenCalled_ReturnOk()
        {
            //Arrange
            var products = new List<Product>() {
                new(){
                    ProductId = 1,
                    Name = "First",
                    Price = 1,
                    CategoryId = 1
                },
                new(){
                    ProductId = 2,
                    Name = "Second",
                    Price = 1,
                    CategoryId = 1
                },
            };
            _mediator.Setup(m => m.Send(It.IsAny<GetProductsQuery>(), default))
                .ReturnsAsync(products);

            //Act
            var response = _controller.GetProducts().Result as OkObjectResult;
            var result = response?.Value as List<Product>;

            //Assert
            response.Should().BeOfType<OkObjectResult>();
            result.Should().BeOfType<List<Product>>();
            result.Should().NotBeNullOrEmpty();
            result.Should().BeEquivalentTo(products);
        }

        [Fact]
        public void GetProducts_WhenCalled_ReturnNotFound()
        {
            //Arrange
            var products = new List<Product>();
            _mediator.Setup(m => m.Send(It.IsAny<GetProductsQuery>(), default))
                .ReturnsAsync(products);

            //Act
            var response = _controller.GetProducts().Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetProducts_WhenException_ReturnProblem()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetProductsQuery>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.GetProducts().Result as ObjectResult;
            var result = response.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion


        #region GetProduct

        [Fact]
        public void GetProduct_WhenCalled_ReturnOk()
        {
            //Arrange
            Product product = new()
            {
                ProductId = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1
            };
            _mediator.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), default))
                .ReturnsAsync(product);

            //Act
            var response = _controller.GetProduct(1).Result as OkObjectResult;
            var result = response?.Value as Product;

            //Assert
            response.Should().BeOfType<OkObjectResult>();
            result.Should().BeOfType<Product>();
            result.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void GetProduct_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), default))
                .ReturnsAsync((Product)null);

            //Act
            var response = _controller.GetProduct(1).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void GetProduct_WhenException_ReturnProblem()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.GetProduct(1).Result as ObjectResult;
            var result = response.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion


        #region CreateProduct

        [Fact]
        public void CreateProduct_WhenCalled_ReturnOk()
        {
            //Arrange
            CreateProductDto createProduct = new()
            {
                Name = "First",
                Price = 1,
                CategoryId = 1
            };

            _mediator.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), default))
                .ReturnsAsync(new Product()
                {
                    ProductId = 1,
                    Name = "First",
                    Price = 1,
                    CategoryId = 1
                });

            //Act
            var response = _controller.CreateProduct(createProduct).Result;
            var result = response as OkResult;

            //Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void CreateProduct_WhenException_ReturnProblem()
        {
            //Arrange
            CreateProductDto createProduct = new()
            {
                Name = "First",
                Price = 1,
                CategoryId = 1
            };

            _mediator.Setup(m => m.Send(It.IsAny<CreateProductCommand>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.CreateProduct(createProduct).Result as ObjectResult;
            var result = response.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion


        #region UpdateProduct

        [Fact]
        public void UpdateProduct_WhenCalled_ReturnOk()
        {
            //Arrange
            UpdateProductDto updateProduct = new()
            {
                ProductId = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1
            };

            _mediator.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), default))
                .ReturnsAsync(new Product()
                {
                    ProductId = 1,
                    Name = "First",
                    Price = 1,
                    CategoryId = 1
                });

            //Act
            var response = _controller.UpdateProduct(updateProduct).Result;
            var result = response as OkResult;


            //Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void UpdateProduct_WhenCalled_ReturnNotFound()
        {
            //Arrange
            UpdateProductDto updateProduct = new()
            {
                ProductId = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1
            };

            _mediator.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), default))
                .ReturnsAsync((Product)null);


            //Act
            var response = _controller.UpdateProduct(updateProduct).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void UpdateProduct_WhenException_ReturnProblem()
        {
            //Arrange
            UpdateProductDto updateProduct = new()
            {
                ProductId = 1,
                Name = "First",
                Price = 1,
                CategoryId = 1
            };

            _mediator.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.UpdateProduct(updateProduct).Result as ObjectResult;
            var result = response.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion


        #region DeleteProduct

        [Fact]
        public void DeleteProduct_WhenCalled_ReturnOk()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default))
                .ReturnsAsync(new Product()
                {
                    ProductId = 1,
                    Name = "First",
                    Price = 1,
                    CategoryId = 1
                });

            //Act
            var response = _controller.DeleteProduct(1).Result;
            var result = response as OkResult;

            //Assert
            result.Should().BeOfType<OkResult>();
        }

        [Fact]
        public void DeleteProduct_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default))
                .ReturnsAsync((Product)null);

            //Act
            var response = _controller.DeleteProduct(1).Result;
            var result = response as NotFoundResult;

            //Assert
            result.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void DeleteProduct_WhenException_ReturnProblem()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = _controller.DeleteProduct(1).Result as ObjectResult;
            var result = response.Value as ProblemDetails;

            //Assert
            result.Should().BeOfType<ProblemDetails>();
            result.Should().Match<ProblemDetails>(r => r.Status == StatusCodes.Status500InternalServerError
                                                  && r.Detail == "Test Exception");
        }

        #endregion
    }
}