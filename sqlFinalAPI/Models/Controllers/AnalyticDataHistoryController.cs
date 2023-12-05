using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using sqlFinalAPI.Models;

namespace sqlFinalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyticDataHistoryController : ControllerBase
    {
        private readonly string connectionString;

        public AnalyticDataHistoryController(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        [HttpGet]
        public ActionResult<List<AnalyticsDataHistory>> GetAllAnalyticsDataHistory()
        {

            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            var history = dbConnection.Query<AnalyticsDataHistory>(
                "SELECT * FROM TubeYou.AnalyticsDataHistory"
            );
            

            return Ok(history);
        }

        [HttpGet("{id}")]
        public ActionResult<AnalyticsDataHistory> GetAnalyticsDataHistoryById(int id)
        {
            using IDbConnection dbConnection = new SqlConnection(connectionString);
            dbConnection.Open();

            var history = dbConnection.QueryFirstOrDefault<AnalyticsDataHistory>(
                "SELECT * FROM TubeYou.AnalyticsDataHistory WHERE HistoryID = @Id",
                new { Id = id }
            );

            if (history == null)
            {
                return NoContent();
            }

            return Ok(history);
        }

     
    }
}
