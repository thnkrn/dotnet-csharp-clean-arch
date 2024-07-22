using DotnetCleanArch.Application.interfaces;
using DotnetCleanArch.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DotnetCleanArch.API.Controllers;

    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductQueries _productQueries;

        public ProductController(IProductQueries productQueries)
        {
            _productQueries = productQueries;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productQueries.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var product = await _productQueries.GetByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            var createdProduct = await _productQueries.AddAsync(product);
            return CreatedAtAction(nameof(Get), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Product product)
        {
            if (id != product.Id)
                return BadRequest();

            await _productQueries.UpdateAsync(product);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _productQueries.DeleteAsync(id);
            return NoContent();
        }
        
        [HttpGet("/error/bussiness")]
        public async Task<IActionResult> MockBusinessException()
        {
            await _productQueries.MockBusinessException();
            return Ok();
        }
        
        [HttpGet("/error/internal")]
        public async Task<IActionResult> MockInternalException()
        {
            await _productQueries.MockInternalException();
            return Ok();
        }
        
        [HttpGet("/error/external")]
        public async Task<IActionResult> MockExternalException()
        {
            await _productQueries.MockExternalException();
            return Ok();
        }
}