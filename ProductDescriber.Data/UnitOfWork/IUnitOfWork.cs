using ProductDescriber.Data.Entities;
using ProductDescriber.Data.Repositories.Interfaces;

namespace ProductDescriber.Data.UnitOfWork;

public interface IUnitOfWork
{
    IGenericRepository<Product> ProductRepository { get; }

    Task<int> SaveChangesAsync();
}
