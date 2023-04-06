using Mapster;
using SimpleMediatr.Contracts.Controllers.Categories;
using SimpleMediatr.Contracts.Controllers.Products;
using SimpleMediatr.Models;

namespace SimpleMediatr.Mapster
{
    public sealed class MapsterRegister : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<CreateProductDto, Product>()
                .Map(s => s.Name, d => d.Name + "MAPPSER");
            config.ForType<UpdateProductDto, Product>();


            config.ForType<CreateCategoryDto, Category>();
            config.ForType<UpdateCategoryDto, Category>();
        }
    }
}
