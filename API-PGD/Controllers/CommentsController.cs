using API_PGD.Models;
using API_PGD.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;

namespace API_PGD.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentRepositorie _commentRepositorie;

        public CommentsController(CommentRepositorie commentRepositorie)
        {
            _commentRepositorie = commentRepositorie;
        }

        [HttpGet("{id}")]
        public ActionResult GetCommentId(Guid id)
        {
            try
            {
                List<Comment> lstComments = _commentRepositorie.GetCommentId(id);
                return Ok(lstComments);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);

            }
        }

        [HttpGet]
        public ActionResult GetAllComments()
        {
            try
            {
                List<Comment> lstComments = _commentRepositorie.GetAllComments();
                return Ok(lstComments);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        public ActionResult RegisterComment([FromBody] Comment comment)
        {
            try
            {
                Comment result = _commentRepositorie.InsertComment(comment);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpPut]
        public ActionResult UpdateComment([FromBody] Comment comment)
        {
            try
            {
                string result = _commentRepositorie.UpdateComment(comment);
                return Ok(result);
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteComment(Guid id)
        {
            try
            {
                _commentRepositorie.DeleteComment(id);
                return Ok();
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
