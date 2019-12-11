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
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext context;
        public CommentsController(ApplicationDbContext appdb) => this.context = appdb;

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public ActionResult<Response> GetComment([FromRoute] int id)
        {
            try
            {
                int productId = id;
                var commentList = from comment in context.Comments.ToListAsync().Result
                                  where comment.ProductId == productId
                                  select comment;
                if (commentList.Count<Comment>() > 0)
                    return new Response(commentList);
                else
                    return new Response(null, 404, "This product has no comments");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // POST: api/Comments
        [HttpPost]
        public ActionResult<Response> PostComment([FromBody] Comment comment)
        {
            try
            {
                context.Comments.Add(comment);
                context.SaveChangesAsync();
                return new Response(comment, 200, "Comment added successfully");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public ActionResult<Response> PutComment([FromRoute] int id, [FromBody] Comment comment)
        {
            try
            {
                var storedComment = context.Comments.SingleOrDefault(c => c.Id == id);
                if (storedComment != null)
                {
                    storedComment.rating = comment.rating;
                    storedComment.ImageURL = comment.ImageURL;
                    storedComment.Text = comment.Text;
                    storedComment.ProductId = comment.ProductId;
                    storedComment.UserId = comment.UserId;
                    context.SaveChanges();
                    return new Response(storedComment, 200, "Comment updated successfully");
                }
                else
                    return new Response(null, 404, "Comment doesn't exist");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public ActionResult<Response> DeleteComment([FromRoute] int id)
        {
            try
            {
                var comment = context.Comments.SingleOrDefaultAsync(c => c.Id == id);
                if (comment.Result != null)
                {
                    context.Comments.Remove(comment.Result);
                    context.SaveChangesAsync();
                    return new Response(null, 200, "Comment deleted successfully");
                }
                else
                    return new Response(null, 404, "Comment doesn't exist");
            }
            catch (Exception e)
            {
                return new Response(null, 404, e.Message);
            }
        }
    }
}
