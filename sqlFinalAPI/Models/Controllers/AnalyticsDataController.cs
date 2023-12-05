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
    public class AnalyticsDataController : ControllerBase
    {
        private readonly string connectionString;

        public AnalyticsDataController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
            Console.WriteLine(connectionString);
            Console.WriteLine("AnalyticsDataController Constructor");
        }

        [HttpGet]
        public ActionResult<List<AnalyticsData>> GetAllAnalyticsData()
        {
            Console.WriteLine("GetAllAnalyticsData");
            using SqlConnection connection = new SqlConnection(connectionString);
            IEnumerable<AnalyticsData> analyticsData = connection.Query<AnalyticsData>("SELECT * FROM TubeYou.AnalyticsData");
            return Ok(analyticsData);
        }

        [HttpGet("{id}")]
        public ActionResult<AnalyticsData> GetAnalyticsDataById(int id)
        {
            Console.WriteLine("GetAnalyticsDataById");
            using SqlConnection connection = new SqlConnection(connectionString);
            AnalyticsData data = connection.QueryFirstOrDefault<AnalyticsData>(
                "SELECT * FROM TubeYou.AnalyticsData WHERE AnalyticsID = @Id", new { Id = id });

            if (data == null)
            {
                return NotFound();
            }

            return Ok(data);
        }

        [HttpPost]
        public IActionResult CreateUser(AnalyticsData analyticsData)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sql = "INSERT INTO Users " +
                        "(AnalyticsID, PostID, UserID, CreationDateTime, ViewsCount,ShareCount)" +
                        "(@AnalyticsID, @PostID, @UserID, @CreationDateTime, @ViewsCount, @ShareCount)";
                    using (var command = new SqlCommand(sql, connection))
                    {
                        command.Parameters.AddWithValue("@AnalyticsID", analyticsData.AnalyticsID);
                        command.Parameters.AddWithValue("@PostID", analyticsData.PostID);
                        command.Parameters.AddWithValue("@UserID", analyticsData.UserID);
                        command.Parameters.AddWithValue("@CreationDateTime", analyticsData.CreationDateTime);
                        command.Parameters.AddWithValue("@ViewsCount", analyticsData.ViewsCount);
                        command.Parameters.AddWithValue("@ShareCount", analyticsData.ShareCount);

                        command.ExecuteNonQuery();
                    }
                }
            }

            catch (Exception ex)
            {
                ModelState.AddModelError("AnalyticsData", "Sorry but we have an exception");
                return BadRequest(ModelState);
            }

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult UpdateAnalyticsData(int id, [FromBody] AnalyticsData data)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            int affectedRows = connection.Execute(
                "UPDATE TubeYou.AnalyticsData SET PostID = @PostID, UserID = @UserID, ViewsCount = @ViewsCount, SharesCount = @SharesCount WHERE AnalyticsID = @Id",
                new { Id = id, data.PostID, data.UserID, data.ViewsCount, data.ShareCount });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteAnalyticsData(int id)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            int affectedRows = connection.Execute(
                "DELETE FROM TubeYou.AnalyticsData WHERE AnalyticsID = @Id", new { Id = id });

            if (affectedRows == 0)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
