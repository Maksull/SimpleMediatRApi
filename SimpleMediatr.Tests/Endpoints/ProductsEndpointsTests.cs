using FluentAssertions;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Moq;
using SimpleMediatr.Contracts.Controllers.Products;
using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.MediatR.Queries.Products;
using SimpleMediatr.Models;
using SimpleMediatR.Endpoints;

namespace SimpleMediatR.Tests.Endpoints
{
    public sealed class ProductsEndpointsTests
    {
        private readonly Mock<IMediator> _mediator;
        private readonly Mock<IMapper> _mapper;

        public ProductsEndpointsTests()
        {
            _mediator = new();
            _mapper = new();
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
            var response = ProductsEndpoints.GetProducts(_mediator.Object).Result;
            var result = response as Ok<IEnumerable<Product>>;

            //Assert
            result.Value.Should().BeOfType<List<Product>>();
            result.Value.Should().NotBeEmpty();
            result.Value.Should().BeEquivalentTo(products);
        }

        [Fact]
        public void GetProducts_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetProductsQuery>(), default))
                .ReturnsAsync(Enumerable.Empty<Product>());

            //Act
            var response = ProductsEndpoints.GetProducts(_mediator.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void GetProducts_WhenException_ReturnProblem()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetProductsQuery>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = ProductsEndpoints.GetProducts(_mediator.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
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
            var response = ProductsEndpoints.GetProduct(1, _mediator.Object).Result;
            var result = response as Ok<Product>;

            //Assert
            result.Value.Should().BeOfType<Product>();
            result.Value.Should().BeEquivalentTo(product);
        }

        [Fact]
        public void GetProduct_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), default))
                .ReturnsAsync((Product)null);

            //Act
            var response = ProductsEndpoints.GetProduct(1, _mediator.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void GetProduct_WhenException_ReturnProblem()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<GetProductByIdQuery>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = ProductsEndpoints.GetProduct(1, _mediator.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
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
            var response = ProductsEndpoints.CreateProduct(createProduct, _mediator.Object, _mapper.Object).Result;
            var result = response as Ok;

            //Assert
            result.Should().BeOfType<Ok>();
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
            var response = ProductsEndpoints.CreateProduct(createProduct, _mediator.Object, _mapper.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
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
            var response = ProductsEndpoints.UpdateProduct(updateProduct, _mediator.Object, _mapper.Object).Result;
            var result = response as Ok;


            //Assert
            result.Should().BeOfType<Ok>();
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
            var response = ProductsEndpoints.UpdateProduct(updateProduct, _mediator.Object, _mapper.Object).Result;
            var result = response as NotFound;


            //Assert
            result.Should().BeOfType<NotFound>();
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
            var response = ProductsEndpoints.UpdateProduct(updateProduct, _mediator.Object, _mapper.Object).Result;
            var result = response as ProblemHttpResult;


            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
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
            var response = ProductsEndpoints.DeleteProduct(1, _mediator.Object).Result;
            var result = response as Ok;


            //Assert
            result.Should().BeOfType<Ok>();
        }

        [Fact]
        public void DeleteProduct_WhenCalled_ReturnNotFound()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default))
                .ReturnsAsync((Product)null);

            //Act
            var response = ProductsEndpoints.DeleteProduct(1, _mediator.Object).Result;
            var result = response as NotFound;

            //Assert
            result.Should().BeOfType<NotFound>();
        }

        [Fact]
        public void DeleteProduct_WhenException_ReturnProblem()
        {
            //Arrange
            _mediator.Setup(m => m.Send(It.IsAny<DeleteProductCommand>(), default))
                .Throws(new Exception("Test Exception"));

            //Act
            var response = ProductsEndpoints.DeleteProduct(1, _mediator.Object).Result;
            var result = response as ProblemHttpResult;

            //Assert
            result.Should().BeOfType<ProblemHttpResult>();
            result.Should().Match<ProblemHttpResult>(r => r.StatusCode == StatusCodes.Status500InternalServerError
                                                  && r.ProblemDetails.Detail == "Test Exception");
        }

        #endregion
    }
}
