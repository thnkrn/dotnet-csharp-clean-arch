using DotnetCleanArch.Application.Exceptions;
using DotnetCleanArch.Application.interfaces;
using DotnetCleanArch.Domain;

namespace DotnetCleanArch.Application.Queries;

public class ProductQueries : IProductQueries
{
    private readonly IProductRepository _productRepository;

    public ProductQueries(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }
    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var result = await _productRepository.GetAllAsync();
        return result;
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var result = await _productRepository.GetByIdAsync(id);
        return result;
    }

    public async Task<Product> AddAsync(Product product)
    {
        var result = await _productRepository.AddAsync(product);
        return result;
    }

    public async Task UpdateAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }

    public async Task DeleteAsync(int id)
    {
        await _productRepository.DeleteAsync(id);
    }

    public Task MockBusinessException()
    {
        throw new BusinessException("Product price cannot be negative.");
    }
    
    public async Task MockInternalException()
    {
        await _productRepository.MockInternalException();
    }
    
    public async Task MockExternalException()
    {
        await _productRepository.MockExternalException();
    }
}