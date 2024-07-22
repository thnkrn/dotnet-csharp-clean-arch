using DotnetCleanArch.Application.interfaces;
using DotnetCleanArch.Domain;
using DotnetCleanArch.Infrastructure.Exceptions;

namespace DotnetCleanArch.Infrastructure;

public class ProductRepository : IProductRepository
{
    private readonly List<Product> _products = new List<Product>();
    
    public ProductRepository()
    {
        _products.Add(new Product { Id = 1, Name = "Laptop", Price = 1000 });
        _products.Add(new Product { Id = 2, Name = "Smartphone", Price = 500 });
    }
    
    public Task<IEnumerable<Product>> GetAllAsync()
    {
        return Task.FromResult(_products.AsEnumerable());
    }

    public Task<Product> GetByIdAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        return Task.FromResult(product);
    }

    public Task<Product> AddAsync(Product product)
    {
        product.Id = _products.Max(p => p.Id) + 1;
        _products.Add(product);
        return Task.FromResult(product);    }

    public Task UpdateAsync(Product product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        if (index != -1)
        {
            _products[index] = product;
        }
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var product = _products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            _products.Remove(product);
        }
        return Task.CompletedTask;
    }
    
    public Task MockInternalException()
    {
        throw new InternalException("Database error");
    }
    
    public Task MockExternalException()
    {
        throw new ExternalException("3rd party error");
    }
}