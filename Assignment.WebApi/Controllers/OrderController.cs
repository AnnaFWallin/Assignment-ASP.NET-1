using Assignment.WebApi.Filters;
using Assignment.WebApi.Models;
using Assignment.WebApi.Models.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Assignment.WebApi.Controllers
{
    [ApiKeyAuth]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly SqlContext _context;

        public OrderController(SqlContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrders()
        {
            var orders = new List<OrderModel>();
            foreach (var order in await _context.Orders.ToListAsync())
                orders.Add(new OrderModel(order.Id, order.CustomerId, order.CustomerName, order.OrderDate, order.Totalprice));

            return orders;
        }

        [HttpGet("{customerId}/CustomerID")]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetCustomersOrders(string customerId)
        {         
            var orders = new List<OrderModel>();

            foreach (var order in await _context.Orders.Where(x => x.CustomerId == customerId).ToListAsync())
                orders.Add(new OrderModel(order.Id, order.CustomerId, order.CustomerName, order.OrderDate, order.Totalprice));

            if (orders == null)
                NotFound();

            return orders;
        }


        [HttpGet("{id}/Id")]
        public async Task<ActionResult<OrderModel>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);

            if (order == null)
                return NotFound();


            return new OrderModel(order.Id, order.CustomerId, order.CustomerName, order.OrderDate, order.Totalprice);
        }

        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrderEntity(OrderCreateModel model)
        {

            var orderEntity = new OrderEntity(model.CustomerId, model.CustomerName, model.OrderDate, model.TotalPrice);
            _context.Orders.Add(orderEntity);
            await _context.SaveChangesAsync();

            return new OrderModel(orderEntity.Id, orderEntity.CustomerId, orderEntity.CustomerName, orderEntity.OrderDate, orderEntity.Totalprice);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, OrderModel model)
        {
            if (id != model.Id)
                return BadRequest();

            var orderEntity = await _context.Orders.FindAsync(id);
            orderEntity.CustomerId = model.CustomerId;
            orderEntity.CustomerName = model.CustomerName;
            orderEntity.OrderDate = model.OrderDate;
            orderEntity.Totalprice = model.TotalPrice;
            

            _context.Entry(orderEntity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderEntityExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrderEntity(int id)
        {
            var orderRows = _context.OrderRows.Where(x => x.OrderId == id);
            foreach(var orderRow in orderRows)
            _context.OrderRows.Remove(orderRow);

            var orderEntity = await _context.Orders.FindAsync(id);
            if (orderEntity == null)
                return NotFound();

            _context.Orders.Remove(orderEntity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderEntityExists(int id)
        {
            return _context.Orders.Any(e => e.Id == id);
        }
    }
}
