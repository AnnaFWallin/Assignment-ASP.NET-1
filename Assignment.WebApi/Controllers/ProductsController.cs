using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Assignment.WebApi;
using Assignment.WebApi.Models.Entities;
using Assignment.WebApi.Models;
using Assignment.WebApi.Filters;

namespace Assignment.WebApi.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProductsController : ControllerBase
    {
        private readonly SqlContext _context;

        public ProductsController(SqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetProducts()
        {
            var items = new List<ProductModel>();
            foreach (var item in await _context.Products.ToListAsync())
                items.Add(new ProductModel(item.Id, item.Name, item.UrlLink, item.Description, item.Price, item.CategoryId));

            return items;
        }

        [HttpGet("{categoryId}/category")]
        public async Task<ActionResult<IEnumerable<ProductModel>>> GetBooks(int categoryId)
        {
            var result = await _context.Products.Where(x => x.CategoryId == categoryId).ToListAsync();
            var items = new List<ProductModel>();
            foreach (var i in result)
                items.Add(new ProductModel(i.Id, i.Name, i.UrlLink, i.Description, i.Price, i.CategoryId));

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>> GetProduct(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);

            if (productEntity == null)
                return NotFound();


            return new ProductModel(productEntity.Id, productEntity.Name, productEntity.UrlLink, productEntity.Description, productEntity.Price, productEntity.CategoryId);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductModel model)
        {
            if (id != model.Id)
                return BadRequest();

            var productEntity = await _context.Products.FindAsync(id);
            productEntity.Name = model.Name;
            productEntity.UrlLink = model.UrlLink;
            productEntity.Description = model.Description;
            productEntity.Price = model.Price;
            productEntity.CategoryId = model.CategoryId;

            _context.Entry(productEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductEntityExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<ProductModel>> PostProductEntity(ProductModel model)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Name == model.Name);
            if (result != null)
                return BadRequest("Product already exist.");

            var productEntity = new ProductEntity(model.Name, model.Description, model.Price, model.CategoryId);
            _context.Products.Add(productEntity);
            await _context.SaveChangesAsync();

            return new ProductModel(productEntity.Id, productEntity.Name, productEntity.UrlLink, productEntity.Description, productEntity.Price, productEntity.CategoryId);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProductEntity(int id)
        {
            var productEntity = await _context.Products.FindAsync(id);
            if (productEntity == null)
                return NotFound();

            _context.Products.Remove(productEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductEntityExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
