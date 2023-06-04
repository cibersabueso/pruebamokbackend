using Microsoft.AspNetCore.Mvc;
using NorthwindAPI.Models;
using NorthwindAPI.Repositories;
using System;
using System.Collections.Generic;

namespace NorthwindAPI.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController : ControllerBase
    {
        private readonly IRepository<Product> _productRepository;

        public ProductsController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        [HttpGet("{id}")]
        public Product GetProductById(int id)
        {
            return _productRepository.GetById(id);
        }

        [HttpPost]
        public IActionResult CreateProduct(Product product)
        {
            _productRepository.Create(product);
            return Ok();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, Product product)
        {
            var existingProduct = _productRepository.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.ProductName = product.ProductName;
            existingProduct.UnitPrice = product.UnitPrice;
            existingProduct.QuantityPerUnit = product.QuantityPerUnit;
            existingProduct.UnitsInStock = product.UnitsInStock;
            existingProduct.UnitsOnOrder = product.UnitsOnOrder;
            existingProduct.ReorderLevel = product.ReorderLevel;
            existingProduct.Discontinued = product.Discontinued;

            _productRepository.Update(existingProduct);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var existingProduct = _productRepository.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            _productRepository.Delete(existingProduct);
            return Ok();
        }

        [HttpGet("paged")]
        public IActionResult GetPagedProducts(int page = 1, int pageSize = 10)
        {
            var totalCount = _productRepository.Count();
            var totalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            page = Math.Max(1, Math.Min(page, totalPages));

            var startIndex = (page - 1) * pageSize;
            var count = Math.Min(pageSize, totalCount - startIndex);

            var products = _productRepository.GetPaged(startIndex, count);

            return Ok(new
            {
                TotalCount = totalCount,
                TotalPages = totalPages,
                CurrentPage = page,
                PageSize = pageSize,
                Products = products
            });
        }
    }
}
