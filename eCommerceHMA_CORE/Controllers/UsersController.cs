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
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public UsersController(ApplicationDbContext appdb) => this.context = appdb;

        // GET: api/Users
        [HttpGet]
        public ActionResult<Response> GetUsers()
        {
            try
            {
                return new Response(context.Users.ToListAsync().Result);
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public ActionResult<Response> GetUser([FromRoute] int id)
        {
            try
            {
                return new Response(context.Users.SingleOrDefaultAsync(u => u.Id == id).Result);
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // POST: api/Users
        [HttpPost]
        public ActionResult<Response> PostUser([FromBody] User user)
        {
            try
            {
                context.Users.Add(user);
                context.SaveChangesAsync();
                return new Response(user, 200, "User added successfully");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public ActionResult<Response> PutUser([FromRoute] int id, [FromBody] User user)
        {
            try
            {
                var storedUser = context.Users.SingleOrDefault(u => u.Id == id);
                if (storedUser != null)
                {
                    storedUser.Username = user.Username;
                    storedUser.Email = user.Email;
                    storedUser.Password = user.Password;
                    storedUser.PurchaseHistory = user.PurchaseHistory;
                    storedUser.ShippingAddress = user.ShippingAddress;
                    context.SaveChanges();
                    return new Response(storedUser, 200, "User updated successfully");
                }
                else
                    return new Response(null, 404, "User doesn't exist");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<Response> DeleteUser([FromRoute] int id)
        {
            try
            {
                var user = context.Users.SingleOrDefaultAsync(u => u.Id == id);
                if (user.Result != null)
                {
                    context.Users.Remove(user.Result);
                    context.SaveChangesAsync();
                    return new Response(null, 200, "User deleted successfully");
                }
                else
                    return new Response(null, 404, "User doesn't exist");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }
    }
}
