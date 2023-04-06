using SimpleMediatr.MediatR.Commands.Categories;
using SimpleMediatr.MediatR.Commands.Products;
using SimpleMediatr.MediatR.Handlers.Categories;
using SimpleMediatr.MediatR.Handlers.Products;
using SimpleMediatr.Models;
using SimpleMediatr.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMediatr.MediatR.Tests.Handlers.Categories
{
    public sealed class CreateCategoryHandlerTests
    {
        private readonly Mock<ICategoryService> _service;
        private readonly CreateCategoryHandler _handler;

        public CreateCategoryHandlerTests()
        {
            _service = new();
            _handler = new(_service.Object);
        }

        [Fact]
        public void Handle_WhenCalled_ReturnProduct()
        {
            //Arrange
            Category category = new()
            {
                CategoryId = 1,
                Name = "First",
            };
            _service.Setup(s => s.CreateCategory(It.IsAny<Category>()))
                .ReturnsAsync(category);

            //Act
            var result = _handler.Handle(new CreateCategoryCommand(category), CancellationToken.None).Result;

            //Assert
            result.Should().BeOfType<Category>();
            result.Should().BeEquivalentTo(category);
        }
    }
}
