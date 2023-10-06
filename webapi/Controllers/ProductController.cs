// ﻿using System.Data;
// using System.Data.Common;
// using System.Net;
// using Microsoft.AspNetCore.Http;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.Data.SqlClient;

// namespace webapi.Controllers
// {
// 	[Route("api/[controller]")]
// 	[ApiController]
// 	public class ProductController : ControllerBase
// 	{
// 		private IConfiguration _configuration;

// 		private String connString =" ";

//         public ProductController(IConfiguration configuration)
//         {
//             _configuration = configuration;
// 			connString = _configuration.GetConnectionString("SqlConnString");
//         }

       
﻿using Microsoft.AspNetCore.Mvc;
using webapi.Models;

namespace webapi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private DataContext _context;
        public ProductController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult getProducts()
        {
            var products = _context.products.ToList();

            return Ok(new ApiResponseWrapper("", products.ToArray()));
        }

        [HttpGet("{id}", Name = "getProduct")]
        public IActionResult getProduct(int id)
        {
            var product = _context.products.Where(product => product.id == id).ToList();

            if(product.Count > 0) 
            {
                return Ok(new ApiResponseWrapper("", product.ToArray()));
            }
            else
            {
                return NotFound(new ApiResponseWrapper("Product not found!", product.ToArray()));
            }
        }

        [HttpPost]
        public IActionResult storeProduct([FromBody] Products product)
        {
            _context.products.Add(product);
            _context.SaveChanges();

            List<Products> objList = new List<Products>();
            objList.Add(product);

            return Ok(new ApiResponseWrapper("", objList.ToArray()));
        }

		 [HttpGet]
		[Route("GetAllProducts")]
		public IActionResult GetAllProducts()
		{
			List<Products> objList = new List<Products>();
			// DataTable table = new DataTable();
			// try
			// { 
			// using(SqlConnection conn = new SqlConnection(connString))
			// 	{
			// 		conn.Open();
			// 		string sql = "select * from products join product_variants on product_variants.product_id = products.id;";
			// 		using(SqlCommand cmd = new SqlCommand(sql, conn))
			// 		{
			// 			SqlDataReader reader = cmd.ExecuteReader();
			// 			table.Load(reader);
						
			// 		}
			// 	}
			// }
			// catch (SqlException ex)
			// {
			// 	throw ex;
			// }

			// return new JsonResult(table);
			return Ok(new ApiResponseWrapper("", objList.ToArray()));
		}

		[HttpGet("{id/int}")]
		[Route("GetProductById")]
		public IActionResult GetProductById(int id)
		{
			List<Products> objList = new List<Products>();
			// DataTable table = new DataTable();
			// try
			// { 
			// using(SqlConnection conn = new SqlConnection(connString))
			// 	{
			// 		string sql = "select * from products join product_variants on product_variants.product_id = products.id where product_variants.id =@id";
			// 		using(SqlCommand cmd = conn.CreateCommand())
			// 		{
			// 			cmd.Parameters.AddWithValue("@id", id); 
			// 			conn.Open();
			// 			using(SqlDataReader reader = cmd.ExecuteReader()){
			// 				// while (reader.Read())
			// 				// 	{    
			// 				// 		matchingPerson.firstName = oReader["FirstName"].ToString();
			// 				// 		matchingPerson.lastName = oReader["LastName"].ToString();                       
			// 				// 	}
			// 				table.Load(reader);
			// 			}
			// 		}
			// 	}
			// }
			// catch (SqlException ex)
			// {
			// 	throw ex;
			// }

			// return new JsonResult({});
			return Ok(new ApiResponseWrapper("", objList.ToArray()));
		}

		[HttpPost]
		[Route("AddItem")]
		public IActionResult  AddItem(string[] employee){
			List<Products> objList = new List<Products>();
			// try{
			// string firstName = "Ola";
			// string lastName ="Hansen";
			// string address = "ABC";
			// string city = "Salzburg";

			// using (SqlConnection connection = new SqlConnection(connString))
			// using (SqlCommand command = connection.CreateCommand())
			// { 
			// 	command.CommandText = "INSERT INTO Student (LastName, FirstName, Address, City) VALUES (@ln, @fn, @add, @cit)";
			// 	command.Parameters.AddWithValue("@ln", lastName);
			// 	command.Parameters.AddWithValue("@fn", firstName);
			// 	command.Parameters.AddWithValue("@add", address);
			// 	command.Parameters.AddWithValue("@cit", city);

			// 	connection.Open();
			// 	command.ExecuteNonQuery();
			// 	connection.Close();
			// } 
			// }catch(SqlException ex){
  			// 		return StatusCode(HttpStatusCode.BadRequest);
			// }
			
			// return StatusCode(HttpStatusCode.OK);
			return Ok(new ApiResponseWrapper("", objList.ToArray()));

		}

    }
}
