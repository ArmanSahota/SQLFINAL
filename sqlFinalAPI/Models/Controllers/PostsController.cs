using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using sqlFinalAPI.Models;
using System.Data;

namespace sqlFinalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostsController : ControllerBase
    {
        private readonly string connectionString;

        public PostsController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine(connectionString);
            Console.WriteLine("PostsController Constructor");
        }

        [HttpGet]
        public ActionResult<List<Posts>> GetAllPosts()
        {
            Console.WriteLine("GetAllPosts");
            using SqlConnection connection = new SqlConnection(connectionString);
            IEnumerable<Posts> posts = connection.Query<Posts>("SELECT * FROM TubeYou.Posts");
            return Ok(posts);
        }

        [HttpGet("{id}")]
        public ActionResult<Posts> GetPostById(int id)
        {
            Console.WriteLine("GetPostById");
            using SqlConnection connection = new SqlConnection(connectionString);
            Posts post = connection.QueryFirstOrDefault<Posts>(
                "SELECT * FROM TubeYou.Posts WHERE PostID = @Id", new { Id = id });

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }

        [HttpPost]
        public IActionResult CreateUser(Posts posts)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO Users " +
                        "(PostsID, UserID, Content, CreationDateTime, LikesCount,CommentsCount)" +
                        "(@PostsID, @UserID, @Content, @CreationDateTime, @LikesCount, @CommentsCount)";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@PostsID", posts.PostID);
                        command.Parameters.AddWithValue("@UserID", posts.UserID);
                        command.Parameters.AddWithValue("@Content", posts.Content);
                        command.Parameters.AddWithValue("@CreationDateTime", posts.CreationDateTime);
                        command.Parameters.AddWithValue("@LikesCount", posts.LikesCount);
                        command.Parameters.AddWithValue("@LikesCount", posts.CommentsCount);

                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("Posts", "Sorry but we have an exception");
                return BadRequest(ModelState);
            }

            return Ok();
        }


        [HttpPut("{id}")]
        public ActionResult UpdatePost(int id, [FromBody] Posts post)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            int affectedRows = connection.Execute(
                "UPDATE TubeYou.Posts SET UserID = @UserID, Content = @Content, CreationDateTime = @CreationDateTime, LikesCount = @LikesCount, CommentsCount = @CommentsCount WHERE PostID = @Id",
                new { Id = id, post.UserID, post.Content, post.CreationDateTime, post.LikesCount, post.CommentsCount });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePost(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            int affectedRows = connection.Execute(
                "DELETE FROM TubeYou.Posts WHERE PostID = @Id", new { Id = id });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
