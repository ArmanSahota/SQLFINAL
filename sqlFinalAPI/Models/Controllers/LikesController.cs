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
    public class LikesController : ControllerBase
    {
        private readonly string connectionString;

        public LikesController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine(connectionString);
            Console.WriteLine("LikesController Constructor");
        }

        [HttpGet]
        public ActionResult<List<Likes>> GetAllLikes()
        {
            Console.WriteLine("GetAllLikes");
            using SqlConnection connection = new SqlConnection(connectionString);
            IEnumerable<Likes> likes = connection.Query<Likes>("SELECT * FROM TubeYou.Likes");
            return Ok(likes);
        }

        [HttpGet("{id}")]
        public ActionResult<Likes> GetLikeById(int id)
        {
            Console.WriteLine("GetLikeById");
            using SqlConnection connection = new SqlConnection(connectionString);
            Likes like = connection.QueryFirstOrDefault<Likes>(
                "SELECT * FROM Likes WHERE TubeYou.LikeID = @Id", new { Id = id });

            if (like == null)
            {
                return NotFound();
            }

            return Ok(like);
        }

        [HttpPost]
        public IActionResult CreateUser(Likes likes)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO Users " +
                        "(LikeID, PostID, UserID, CreationDateTime)" +
                        "(@LikeID, @PostID, @UserID, @CreationDateTime)";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@LikeID", likes.LikeID);
                        command.Parameters.AddWithValue("@PostID", likes.PostID);
                        command.Parameters.AddWithValue("@UserID", likes.UserID);
                        command.Parameters.AddWithValue("@CreationDateTime", likes.CreationDateTime);

                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("Likes", "Sorry but we have an exception");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateLike(int id, [FromBody] Likes like)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            int affectedRows = connection.Execute(
                "UPDATE TubeYou.Likes SET PostID = @PostID, UserID = @UserID, CreationDateTime = @CreationDateTime WHERE LikeID = @Id",
                new { Id = id, like.PostID, like.UserID, like.CreationDateTime });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteLike(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            int affectedRows = connection.Execute(
                "DELETE FROM TubeYou.Likes WHERE LikeID = @Id", new { Id = id });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
