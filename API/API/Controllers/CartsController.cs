using API.Models;
using API.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHubContext<CartHub> _hubContext;

        public CartsController(IConfiguration configuration, IHubContext<CartHub> hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        [HttpGet("{userName}")]
        public JsonResult GetCartUserName(string userName)
        {
            string query = "SELECT * FROM ShoppingCart WHERE UserName = @UserName";
            string sqlDataScoure = _configuration.GetConnectionString("StoreDBCon");
            DataTable table = new DataTable();
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataScoure))
            {
                myConn.Open();
                using (SqlCommand command = new SqlCommand(query, myConn))
                {
                    command.Parameters.AddWithValue("@UserName", userName);
                    myReader = command.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost("AddProductToCart")]
        public async Task<IActionResult> AddProductToCart([FromBody] CartModel cart)
        {
            if (cart == null)
            {
                return BadRequest("Dữ liệu rỗng.");
            }

            // Truy vấn trực tiếp để lấy thông tin sản phẩm từ cơ sở dữ liệu
            string sqlDataSource = _configuration.GetConnectionString("StoreDBCon");
            string getProductQuery = "SELECT ProductID, Image, ProductName, Price, Origin FROM Product WHERE ProductName = @ProductName";

            ProductModel product = null;

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(getProductQuery, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@ProductName", cart.ProductName);
                        using (SqlDataReader reader = sqlCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                product = new ProductModel
                                {
                                    ProductID = reader["ProductID"] != DBNull.Value ? Convert.ToInt32(reader["ProductID"]) : 0,
                                    Image = reader["Image"] != DBNull.Value ? reader["Image"].ToString() : "default_image.jpg",
                                    ProductName = reader["ProductName"] != DBNull.Value ? reader["ProductName"].ToString() : "Unknown Product",
                                    Price = reader["Price"] != DBNull.Value ? Convert.ToDecimal(reader["Price"]) : 0.0m,
                                    Origin = reader.IsDBNull(reader.GetOrdinal("Origin")) ? "VIỆT NAM" : reader["Origin"].ToString() // Set default value to "VIỆT NAM"
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the exception
                Console.WriteLine($"Database error: {ex.Message}");
                return StatusCode(500, "Database error: " + ex.Message);
            }

            if (product == null)
            {
                return NotFound("Không có sản phẩm.");
            }

            var missingFields = new List<string>();
            if (product.ProductID == 0) missingFields.Add("ProductID");
            if (string.IsNullOrEmpty(product.Image)) missingFields.Add("Image");
            if (product.Price == 0) missingFields.Add("Price");

            if (missingFields.Count > 0)
            {
                return BadRequest("Thông tin sản phẩm không đầy đủ: " + string.Join(", ", missingFields));
            }

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
                {
                    sqlConnection.Open();
                    string checkQuery = "SELECT Quantity FROM ShoppingCart WHERE UserName = @UserName AND ProductID = @ProductID";
                    using (SqlCommand checkCommand = new SqlCommand(checkQuery, sqlConnection))
                    {
                        checkCommand.Parameters.AddWithValue("@UserName", cart.UserName);
                        checkCommand.Parameters.AddWithValue("@ProductID", product.ProductID);

                        object result = checkCommand.ExecuteScalar();
                        if (result != null)
                        {
                            int existingQuantity = Convert.ToInt32(result);
                            string updateQuery = "UPDATE ShoppingCart SET Quantity = @Quantity WHERE UserName = @UserName AND ProductID = @ProductID";
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, sqlConnection))
                            {
                                updateCommand.Parameters.AddWithValue("@Quantity", existingQuantity + cart.Quantity);
                                updateCommand.Parameters.AddWithValue("@UserName", cart.UserName);
                                updateCommand.Parameters.AddWithValue("@ProductID", product.ProductID);
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            string insertQuery = "INSERT INTO ShoppingCart (UserName, ProductID, Image, ProductName, Quantity, Price, Origin) " +
                                                 "VALUES (@UserName, @ProductID, @Image, @ProductName, @Quantity, @Price, @Origin)";
                            using (SqlCommand insertCommand = new SqlCommand(insertQuery, sqlConnection))
                            {
                                insertCommand.Parameters.AddWithValue("@UserName", cart.UserName);
                                insertCommand.Parameters.AddWithValue("@ProductID", product.ProductID);
                                insertCommand.Parameters.AddWithValue("@Image", product.Image);
                                insertCommand.Parameters.AddWithValue("@ProductName", product.ProductName);
                                insertCommand.Parameters.AddWithValue("@Quantity", cart.Quantity);
                                insertCommand.Parameters.AddWithValue("@Price", product.Price);
                                insertCommand.Parameters.AddWithValue("@Origin", product.Origin);
                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                // Gửi thông báo SignalR
                await _hubContext.Clients.User(cart.UserName).SendAsync("CartUpdated");
                return Ok("Sản phẩm được thêm vào giỏ hàng thành công!");
            }
            catch (SqlException ex)
            {
                // Log the exception
                Console.WriteLine($"Database error: {ex.Message}");
                return StatusCode(500, "Database error: " + ex.Message);
            }
        }



        [HttpDelete("RemoveCart")]
        public async Task<IActionResult> RemoveCart(string UserName, string ProductName)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(ProductName))
            {
                return BadRequest("Đầu vào không hợp lệ.");
            }

            string query = "DELETE FROM ShoppingCart WHERE UserName = @UserName AND ProductName = @ProductName";
            string sqlDataSource = _configuration.GetConnectionString("StoreDBCon");

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@UserName", UserName);
                        sqlCommand.Parameters.AddWithValue("@ProductName", ProductName);

                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            await _hubContext.Clients.User(UserName).SendAsync("CartUpdated");
                            return Ok("Sản phẩm đã được xóa khỏi giỏ hàng.");
                        }
                        else
                        {
                            return NotFound("Sản phẩm không tìm thấy trong giỏ hàng.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the exception
                Console.WriteLine($"Database error: {ex.Message}");
                return StatusCode(500, "Database error: " + ex.Message);
            }
        }

        [HttpDelete("ClearCart/{UserName}")]
        public async Task<IActionResult> ClearCart(string UserName)
        {
            if (string.IsNullOrEmpty(UserName))
            {
                return BadRequest("Tên người dùng là bắt buộc.");
            }

            string query = "DELETE FROM ShoppingCart WHERE UserName = @UserName";
            string sqlDataSource = _configuration.GetConnectionString("StoreDBCon");

            try
            {
                using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
                {
                    sqlConnection.Open();
                    using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                    {
                        sqlCommand.Parameters.AddWithValue("@UserName", UserName);

                        int rowsAffected = sqlCommand.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            await _hubContext.Clients.User(UserName).SendAsync("CartUpdated");
                            return Ok("Tất cả sản phẩm đã bị xóa khỏi giỏ hàng.");
                        }
                        else
                        {
                            return NotFound("Không tìm thấy sản phẩm nào trong giỏ hàng cho người dùng được chỉ định.");
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                // Log the exception
                Console.WriteLine($"Database error: {ex.Message}");
                return StatusCode(500, "Database error: " + ex.Message);
            }
        }

        [HttpPut("UpdateCartQuantity")]
        public async Task<IActionResult> UpdateCartQuantity(string UserName, string ProductName, int Quantity)
        {
            if (string.IsNullOrEmpty(UserName) || string.IsNullOrEmpty(ProductName) || Quantity <= 0)
            {
                return BadRequest("Đầu vào không hợp lệ.");
            }

            string query = "UPDATE ShoppingCart SET Quantity = @Quantity WHERE UserName = @UserName AND ProductName = @ProductName";
            string sqlDataSource = _configuration.GetConnectionString("StoreDBCon");

            using (SqlConnection sqlConnection = new SqlConnection(sqlDataSource))
            {
                sqlConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, sqlConnection))
                {
                    sqlCommand.Parameters.AddWithValue("@UserName", UserName);
                    sqlCommand.Parameters.AddWithValue("@ProductName", ProductName);
                    sqlCommand.Parameters.AddWithValue("@Quantity", Quantity);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    if (rowsAffected > 0)
                    {
                        await _hubContext.Clients.User(UserName).SendAsync("CartUpdated");
                        return Ok("Số lượng được cập nhật thành công.");
                    }
                    else
                    {
                        return NotFound("Sản phẩm không tìm thấy trong giỏ hàng.");
                    }
                }
            }
        }
    }
}
