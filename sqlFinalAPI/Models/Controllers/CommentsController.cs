using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using sqlFinalAPI.Models;
namespace sqlFinalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly string connectionString;

        public CommentsController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine(connectionString);
            Console.WriteLine("CommentsController Constructor");
        }

        [HttpGet]
        public ActionResult<List<Comments>> GetAllComments()
        {
            Console.WriteLine("GetAllComments");
            using SqlConnection connection = new SqlConnection(connectionString);
            IEnumerable<Comments> comments = connection.Query<Comments>("SELECT * FROM TubeYou.Comments");
            return Ok(comments);
        }

        [HttpGet("{id}")]
        public ActionResult<Comments> GetCommentById(int id)
        {
            Console.WriteLine("GetCommentById");
            using SqlConnection connection = new SqlConnection(connectionString);
            Comments comment = connection.QueryFirstOrDefault<Comments>(
                "SELECT * FROM TubeYou.Comments WHERE CommentID = @Id", new { Id = id });

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(comment);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult CreateUser(Comments comments)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO Users " +
                        "(CommentID, PostID, UserID, Content, CreationDateTime)" +
                        "(@CommentID, @PostID, @UserID, @Content, @CreationDateTime)";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@CommentID", comments.CommentID);
                        command.Parameters.AddWithValue("@PostID", comments.PostID);
                        command.Parameters.AddWithValue("@UserID", comments.UserID);
                        command.Parameters.AddWithValue("@Content", comments.Content);
                        command.Parameters.AddWithValue("@PostID", comments.PostID);

                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("Comments", "Sorry but we have an exception");
                return BadRequest(ModelState);
            }

            return Ok();
        }


        [HttpPut("{id}")]
        public ActionResult UpdateComment(int id, [FromBody] Comments comment)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            int affectedRows = connection.Execute(
                "UPDATE TubeYou.Comments SET PostID = @PostID, UserID = @UserID, Content = @Content, CreationDateTime = @CreationDateTime WHERE CommentID = @Id",
                new { Id = id, comment.PostID, comment.UserID, comment.Content, comment.CreationDateTime });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteComment(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            int affectedRows = connection.Execute(
                "DELETE FROM TubeYou.Comments WHERE CommentID = @Id", new { Id = id });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
