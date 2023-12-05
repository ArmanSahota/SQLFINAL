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
    public class UsersController : ControllerBase
    {
        private readonly string connectionString;

        public UsersController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine(connectionString);
            Console.WriteLine("UsersController Constructor");
        }

        [HttpGet]
        public ActionResult<List<UserZ>> GetAllUsers()
        {
            Console.WriteLine("GetAllUsers");
            using SqlConnection connection = new SqlConnection(connectionString);
            IEnumerable<UserZ> users = connection.Query<UserZ>("SELECT * FROM TubeYou.Users");
            return Ok(users);
        }

        [HttpGet("{id}")]
        public ActionResult<UserZ> GetUserById(int id)
        {
            Console.WriteLine("GetUserById");
            using SqlConnection connection = new SqlConnection(connectionString);
            UserZ user = connection.QueryFirstOrDefault<UserZ>(
                "SELECT * FROM TubeYou.Users WHERE UserID = @Id", new { Id = id });

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser(UserZ user) 
        {
            try 
            { 
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO Users " +
                        "(UserId, Username, FirstName, LastName, Email)" +
                        "(@UserID, @Username, @FirstName, @LastName, @Email)";
                    using (var command = new SqlCommand(sql, connection)) 
                    {
                        command.Parameters.AddWithValue("@UserId", user.UserID);
                        command.Parameters.AddWithValue("@Username", user.Username);
                        command.Parameters.AddWithValue("@FirstName", user.FirstName);
                        command.Parameters.AddWithValue("@LastName", user.LastName);
                        command.Parameters.AddWithValue("@Email", user.Email);

                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("User", "Sorry but we have an exception");
                return BadRequest(ModelState);
            }

            return Ok();
        }
        

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] UserZ user)
        {
            Console.WriteLine("UpdateUser");
            using SqlConnection connection = new SqlConnection(connectionString);
            int affectedRows = connection.Execute(
                "UPDATE TubeYou.Users SET Username = @Username, FirstName = @FirstName, LastName = @LastName, Email = @Email WHERE UserID = @Id",
                new { Id = id, user.Username, user.FirstName, user.LastName, user.Email });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            Console.WriteLine("DeleteUser");
            using SqlConnection connection = new SqlConnection(connectionString);
            int affectedRows = connection.Execute(
                "DELETE FROM TubeYou.Users WHERE UserID = @Id", new { Id = id });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
