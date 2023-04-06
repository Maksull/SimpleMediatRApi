using SimpleMediatr.Data.Repository.Interfaces;

namespace SimpleMediatr.Data.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IProductRepository Product { get; }
        ICategoryRepository Category { get; }
    }
}