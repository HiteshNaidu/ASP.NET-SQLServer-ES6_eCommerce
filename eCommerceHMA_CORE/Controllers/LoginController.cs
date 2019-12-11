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
    public class LoginController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LoginController(ApplicationDbContext appdb) => this.context = appdb;

        // POST: api/Login
        [HttpPost]
        public ActionResult<Response> Post([FromBody] User user)
        {
            try
            {
                var fetched_user = context.Users.SingleOrDefaultAsync(p => p.Email == user.Email);
                if (fetched_user.Result != null && user.Password == fetched_user.Result.Password)
                {
                    HttpContext.Session.SetString("is_login", "true");
                    HttpContext.Session.SetString("userid", fetched_user.Result.Id.ToString());
                    return new Response(null, 200, "Login successful");
                }
                else
                    return new Response(null, 200, "Login failed");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }
    }
}