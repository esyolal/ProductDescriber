using ProductDescriber.Data.Entities;
using ProductDescriber.Data.Repositories;
using ProductDescriber.Data.Repositories.Interfaces;

namespace ProductDescriber.Data.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        ProductRepository = new GenericRepository<Product>(_context);
    }

    public IGenericRepository<Product> ProductRepository { get; }

    public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
}
