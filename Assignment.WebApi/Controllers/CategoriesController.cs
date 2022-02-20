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
    public class CategoriesController : ControllerBase
    {
        private readonly SqlContext _context;

        public CategoriesController(SqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryModel>>> GetCategories()
        {
            var items = new List<CategoryModel>();
            foreach (var item in await _context.Categories.ToListAsync())
                items.Add(new CategoryModel(item.Id, item.Name));

            return items;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetCategoryEntity(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);

            if (categoryEntity == null)
                return NotFound();

            return new CategoryModel(categoryEntity.Id, categoryEntity.Name);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoryEntity(int id, CategoryModel categoryModel)
        {
            if (id != categoryModel.Id)
                return BadRequest();

            var categoryEntity = await _context.Categories.FindAsync(id);
            categoryEntity.Name = categoryModel.Name;

            _context.Entry(categoryEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryEntityExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<CategoryModel>> PostCategoryEntity(CategoryCreateModel model)
        {
            var result = await _context.Categories.FirstOrDefaultAsync(x => x.Name == model.Name);
            if (result != null)
                return BadRequest("Category already exist.");

            var categoryEntity = new CategoryEntity(model.Name);
            _context.Categories.Add(categoryEntity);
            await _context.SaveChangesAsync();

            return new CategoryModel(categoryEntity.Id, categoryEntity.Name);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryEntity(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);
            if (categoryEntity == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(categoryEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryEntityExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
