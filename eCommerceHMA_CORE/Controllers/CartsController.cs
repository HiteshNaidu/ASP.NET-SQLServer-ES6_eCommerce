using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceHMA_CORE.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eCommerceHMA_CORE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public CartsController(ApplicationDbContext appdb) => this.context = appdb;

        // GET: api/Carts
        [HttpGet]
        public ActionResult<Response> GetCarts()
        {
            try
            {
                int id = string.IsNullOrEmpty(HttpContext.Session.GetString("userid")) ? 1 : int.Parse(HttpContext.Session.GetString("userid"));
                return new Response(from cart in context.Carts.ToListAsync().Result
                                    where cart.UserId == id
                                    select cart);
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public ActionResult<Response> GetCart([FromRoute] int id)
        {
            try
            {
                int userid = id;
                var cartList = from cart in context.Carts.ToListAsync().Result
                               where cart.UserId == userid
                               select cart;
                if (cartList.Count<Cart>() > 0)
                    return new Response(cartList);
                else
                    return new Response(null, 404, "User's cart is empty");
            }
            catch (Exception ex)
            {
                return new Response(null, 404, ex.Message);
            }
        }

        // POST: api/Carts
        [HttpPost]
        public ActionResult<Response> PostCart([FromBody] Cart cart)
        {
            try
            {
                context.Carts.Add(cart);
                context.SaveChangesAsync();
                return new Response(cart, 200, "Cart added successfully");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // PUT: api/Carts/5
        [HttpPut("{id}")]
        public ActionResult<Response> PutCart([FromRoute] int id, [FromBody] Cart cart)
        {
            try
            {
                var storedCart = context.Carts.SingleOrDefault(c => c.Id == id);
                if (storedCart != null)
                {
                    storedCart.Quantity = cart.Quantity;
                    storedCart.UserId = cart.UserId;
                    storedCart.ProductId = cart.ProductId;
                    context.SaveChanges();
                    return new Response(storedCart, 200, "Cart updated successfully");
                }
                else
                    return new Response(null, 404, "Cart doesn't exist");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<Response> DeleteCart([FromRoute] int id)
        {
            try
            {
                var cart = context.Carts.SingleOrDefaultAsync(c => c.Id == id);
                if (cart.Result != null)
                {
                    context.Carts.Remove(cart.Result);
                    context.SaveChangesAsync();
                    return new Response(null, 200, "Cart deleted successfully");
                }
                else
                    return new Response(null, 404, "Cart doesn't exist");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }
    }
}
