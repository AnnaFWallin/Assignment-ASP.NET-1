using Assignment.WebApi.Filters;
using Assignment.WebApi.Models;
using Assignment.WebApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Assignment.WebApi.Controllers
{
    //[ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderRowController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrderRowController(SqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderRowModel>>> GetOrderRows()
        {
            var items = new List<OrderRowModel>();
            foreach (var item in await _context.OrderRows.ToListAsync())
                items.Add(new OrderRowModel(item.Id, item.OrderId, item.ProductId, item.Amount, item.ProductName, item.Price));

            return items;
        }
        [HttpGet("{orderId}/OrderId")]
        public async Task<ActionResult<IEnumerable<OrderRowModel>>> GetOrderRowFromOneOrder(int orderId)
        {
            var rows = new List<OrderRowModel>();
            foreach (var i in await _context.OrderRows.Where(x => x.OrderId == orderId).ToListAsync())
                rows.Add(new OrderRowModel(i.Id, i.OrderId, i.ProductId, i.Amount, i.ProductName, i.Price));

            return rows;
        }

        [HttpPost]
        public async Task<ActionResult<OrderRowModel>> PostOrderRowEntity(OrderRowModel model)
        {
            var order = new OrderEntity();
            order = await _context.Orders.OrderBy(x => x.OrderDate).LastOrDefaultAsync();
            if (order == null)
                return NotFound();

            var orderModel = new OrderModel(order.Id, order.CustomerId, order.CustomerName, order.OrderDate, order.Totalprice);

            var orderRowEntity = new OrderRowEntity(order.Id, model.ProductId, model.Amount, model.ProductName, model.Price);
            _context.OrderRows.Add(orderRowEntity);
            await _context.SaveChangesAsync();

            return new OrderRowModel(orderRowEntity.Id, orderRowEntity.OrderId, orderRowEntity.ProductId, model.Amount, model.ProductName, model.Price);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrderRowModel>> UpdateOrderRow(OrderRowModel model, int id)
        {
            if (id != model.Id)
                return BadRequest();

            var orderRowEntity = await _context.OrderRows.FindAsync(id);
            orderRowEntity.OrderId = model.OrderId;
            orderRowEntity.ProductId = model.ProductId;
            orderRowEntity.Amount = model.Amount;
            orderRowEntity.ProductName = model.ProductName;
            orderRowEntity.Price = model.Price;

            _context.Entry(orderRowEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderRowEntityExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderRowEntity(int id)
        {
            var orderRowEntity = await _context.OrderRows.FindAsync(id);
            if (orderRowEntity == null)
                return NotFound();

            _context.OrderRows.Remove(orderRowEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderRowEntityExists(int id)
        {
            return _context.OrderRows.Any(e => e.Id == id);
        }
    }
}
