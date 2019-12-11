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
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public ProductsController(ApplicationDbContext appdb) => this.context = appdb;

        // GET: api/Products
        [HttpGet]
        public ActionResult<Response> GetProducts()
        {
            try
            {
                return new Response(context.Products.ToListAsync().Result);
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public ActionResult<Response> GetProduct([FromRoute] int id)
        {
            try
            {
                return new Response(context.Products.SingleOrDefaultAsync(p => p.Id == id).Result);
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // POST: api/Products
        [HttpPost]
        public ActionResult<Response> PostProduct([FromBody] Product product)
        {
            try
            {
                context.Products.Add(product);
                context.SaveChangesAsync();
                return new Response(product, 200, "Product added successfully");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // PUT: api/Products/5
        [HttpPut("{id}")]
        public ActionResult<Response> PutProduct([FromRoute] int id, [FromBody] Product product)
        {
            try
            {
                var storedProduct = context.Products.SingleOrDefault(p => p.Id == id);
                if (storedProduct != null)
                {
                    storedProduct.Description = product.Description;
                    storedProduct.Price = product.Price;
                    storedProduct.ShippingCost = product.ShippingCost;
                    storedProduct.ImageURL = product.ImageURL;
                    context.SaveChanges();
                    return new Response(storedProduct, 200, "Product updated successfully");
                }
                else
                    return new Response(null, 404, "Product doesn't exist");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<Response> DeleteProduct([FromRoute] int id)
        {
            try
            {
                var product = context.Products.SingleOrDefaultAsync(p => p.Id == id);
                if (product.Result != null)
                {
                    context.Products.Remove(product.Result);
                    context.SaveChangesAsync();
                    return new Response(null, 200, "Product deleted successfully");
                }
                else
                    return new Response(null, 404, "Product doesn't exist");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }
    }
}
