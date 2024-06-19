using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IConfiguration _configuration;
        public ProductsController(IConfiguration configuration)
        {
            _configuration=configuration;
        }

        [HttpGet]
        public JsonResult getProduct()
        {
            string query = "SELECT * FROM Product";
            DataTable table = new DataTable();
            string DataScoure = _configuration.GetConnectionString("StoreDBCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(DataScoure))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myCon.Close();
                    myReader.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("{productName}")]
        public JsonResult GetProductByName(string productName)
        {
            string query = "SELECT * FROM Product WHERE ProductName = @ProductName";
            DataTable table = new DataTable();
            string DataScoure = _configuration.GetConnectionString("StoreDBCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(DataScoure))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ProductName", productName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myCon.Close();
                    myReader.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpGet("Search")]
        public JsonResult SearchProduct(string searchTerm)
        {
            string query = "SELECT * FROM Product WHERE ProductName = @SearchTerm";
            DataTable table = new DataTable();
            string dataSource = _configuration.GetConnectionString("StoreDBCon");

            using (SqlConnection connection = new SqlConnection(dataSource))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@SearchTerm", searchTerm);
                    SqlDataReader reader = command.ExecuteReader();
                    table.Load(reader);
                    reader.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpPost]
        public JsonResult CreateProduct(ProductModel product)
        {
            string query = "SELECT MAX(ProductID) FROM Product";
            using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("StoreDBCon")))
            {
                myCon.Open();
                using(SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    int maxProductId = (int) myCommand.ExecuteScalar();
                    myCon.Close() ;

                    int newProductId = maxProductId + 1 ;

                    string insertQuery = "INSERT INTO Product (ProductID, Image, ProductName, Price, Sold, Stock, Imported) VALUES (@ProductID, @Image, @ProductName, @Price, @Sold, @Stock, @Imported)";

                    using(SqlCommand insertCommand = new SqlCommand(insertQuery, myCon))
                    {
                        insertCommand.Parameters.AddWithValue("@ProductID", newProductId);
                        insertCommand.Parameters.AddWithValue("@Image", product.Image);
                        insertCommand.Parameters.AddWithValue("@ProductName", product.ProductName);
                        insertCommand.Parameters.AddWithValue("@Price", product.Price);
                        insertCommand.Parameters.AddWithValue("@Sold", product.Sold);
                        insertCommand.Parameters.AddWithValue("@Stock", product.Stock);
                        insertCommand.Parameters.AddWithValue("@Imported", product.Imported);

                        myCon.Open();
                        int x = insertCommand.ExecuteNonQuery();
                        myCon.Close();

                        if (x > 0)
                        {
                            return new JsonResult(new { message = "Thêm sản phẩm thành công" });
                        }
                        else { return new JsonResult(new { message = "Thêm sản phẩm thất bại" }); }

                    }
                }
            }
        }

        [HttpDelete("{ProductId}")]
        public JsonResult DelProduct(int ProductId)
        {
            try
            {
                using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("StoreDBCon")))
                {
                    string query = "DELETE FROM Product WHERE ProductID = @ProductId";
                    myCon.Open();

                    using (SqlCommand command = new SqlCommand(query, myCon))
                    {
                        command.Parameters.AddWithValue("@ProductId", ProductId);
                      
                        if (command.ExecuteNonQuery() > 0)
                        {
                            return new JsonResult(new { message = "Xóa sản phẩm thành công" });
                        }
                        else
                        {
                            return new JsonResult(new { message = "Không tìm thấy sản phẩm để xóa" });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { message = $"Lỗi máy chủ nội bộ: {ex.Message}" }) { StatusCode = 500 };
            }
        }

        [HttpGet("TopSellingProducts")]
        public JsonResult GetTopSellingProducts()
        {
            string query = "SELECT TOP 12 ProductID, Image, ProductName, Origin, Price, Sold, Stock, Imported " +
                           "FROM dbo.Product ORDER BY Sold DESC";
            string sqlDataSource = _configuration.GetConnectionString("StoreDBCon");
            DataTable table = new DataTable();

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand command = new SqlCommand(query, myConn))
                {
                    SqlDataReader myReader = command.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
