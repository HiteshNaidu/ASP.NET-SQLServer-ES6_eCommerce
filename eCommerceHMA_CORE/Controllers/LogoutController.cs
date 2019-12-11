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
    public class LogoutController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public LogoutController(ApplicationDbContext appdb) => this.context = appdb;

        [HttpGet]
        public async Task<Response> Get()
        {
            try
            {
                if (HttpContext.Session.GetString("is_login") == "true")
                {
                    await HttpContext.Session.LoadAsync();
                    HttpContext.Session.SetString("is_login", "false");
                    int logoutDefault = 0;
                    HttpContext.Session.SetString("userid", logoutDefault.ToString());
                    return new Response(null, 200, "Logout successful");
                }
                else
                    return new Response(null, 200, "Logout failed");
            }
            catch(Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }
    }
}