using FluentAssertions;
using Mapster;
using MapsterMapper;
using SimpleMediatr.Contracts.Controllers.Categories;
using SimpleMediatr.Contracts.Controllers.Products;
using SimpleMediatr.Models;

namespace SimpleMediatr.Mapster.Tests
{
    public sealed class MapsterRegisterTests
    {
        [Fact]
        public void Map_AllEntities()
        {
            //Arrange
            TypeAdapterConfig config = new();
            config.Apply(new MapsterRegister());

            var mapper = new Mapper(config);


            //Act
            var createProductDto = new CreateProductDto { Name = "test product" };
            var updateProductDto = new UpdateProductDto { Name = "updated product" };
            var createCategoryDto = new CreateCategoryDto { Name = "test category" };
            var updateCategoryDto = new UpdateCategoryDto { Name = "updated category" };

            var product = mapper.Map<Product>(createProductDto);
            var updatedProduct = mapper.Map<Product>(updateProductDto);
            var category = mapper.Map<Category>(createCategoryDto);
            var updatedCategory = mapper.Map<Category>(updateCategoryDto);

            //Assert

            updatedProduct.Name.Should().Be("updated product");
            category.Name.Should().Be("test category");
            updatedCategory.Name.Should().Be("updated category");
            product.Name.Should().Be("test productMAPPSER");
        }
    }
}