using System.Data;
using System.Data.Common;
using System.Net;
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
					string sql = "select * from products join product_variants on product_variants.product_id = products.id;";
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

		[HttpGet("{id/int}")]
		[Route("GetProductById")]
		public JsonResult GetProductById(int id)
		{

			DataTable table = new DataTable();
			try
			{ 
			using(SqlConnection conn = new SqlConnection(connString))
				{
					string sql = "select * from products join product_variants on product_variants.product_id = products.id where product_variants.id =@id";
					using(SqlCommand cmd = conn.CreateCommand())
					{
						cmd.Parameters.AddWithValue("@id", id); 
						conn.Open();
						using(SqlDataReader reader = cmd.ExecuteReader()){
							// while (reader.Read())
							// 	{    
							// 		matchingPerson.firstName = oReader["FirstName"].ToString();
							// 		matchingPerson.lastName = oReader["LastName"].ToString();                       
							// 	}
							table.Load(reader);
						}
					}
				}
			}
			catch (SqlException ex)
			{
				throw ex;
			}

			return new JsonResult(table);
		}

		[HttpPost]
		[Route("AddItem")]
		public IHttpActionResult  AddItem(string[] employee){
			try{
			string firstName = "Ola";
			string lastName ="Hansen";
			string address = "ABC";
			string city = "Salzburg";

			using (SqlConnection connection = new SqlConnection(connString))
			using (SqlCommand command = connection.CreateCommand())
			{ 
				command.CommandText = "INSERT INTO Student (LastName, FirstName, Address, City) VALUES (@ln, @fn, @add, @cit)";
				command.Parameters.AddWithValue("@ln", lastName);
				command.Parameters.AddWithValue("@fn", firstName);
				command.Parameters.AddWithValue("@add", address);
				command.Parameters.AddWithValue("@cit", city);

				connection.Open();
				command.ExecuteNonQuery();
				connection.Close();
			} 
			}catch(SqlException ex){
  					return StatusCode(HttpStatusCode.BadRequest);
			}
			
			return StatusCode(HttpStatusCode.OK);

		}

        private IHttpActionResult StatusCode(HttpStatusCode notModified)
        {
            throw new NotImplementedException();
        }
    }

    public interface IHttpActionResult
    {
    }
}
