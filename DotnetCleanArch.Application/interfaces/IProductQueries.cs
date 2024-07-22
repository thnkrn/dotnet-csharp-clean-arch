using DotnetCleanArch.Domain;

namespace DotnetCleanArch.Application.interfaces;

public interface IProductQueries
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(int id);
    Task<Product> AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(int id);
    Task MockBusinessException();
    Task MockInternalException();
    Task MockExternalException();

}