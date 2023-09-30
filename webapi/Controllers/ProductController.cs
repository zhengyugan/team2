using System.Data;
using System.Data.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace webapi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private IConfiguration _configuration;

		private String connString =" ";

        public ProductController(IConfiguration configuration)
        {
            _configuration = configuration;
			connString = _configuration.GetConnectionString("SqlConnString");
        }

        [HttpGet]
		[Route("GetAllProducts")]
		public JsonResult GetAllProducts()
		{
			DataTable table = new DataTable();
			try
			{ 
			using(SqlConnection conn = new SqlConnection(connString))
				{
					conn.Open();
					string sql = "select * from products";
					using(SqlCommand cmd = new SqlCommand(sql, conn))
					{
						SqlDataReader reader = cmd.ExecuteReader();
						table.Load(reader);
						
					}
				}
			}
			catch (SqlException ex)
			{
				throw ex;
			}

			return new JsonResult(table);
		}
	}
}
