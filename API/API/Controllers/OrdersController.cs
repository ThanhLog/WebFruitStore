using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using API.Models;
using Microsoft.AspNetCore.SignalR;
using API.Hubs;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IHubContext<OrderHub> _hubContext;

        public OrdersController(IConfiguration configuration, IHubContext<OrderHub> hubContext)
        {
            _configuration = configuration;
            _hubContext = hubContext;
        }

        [HttpGet]
        public JsonResult GetAllOrders()
        {
            string query = @"
                SELECT O.OrderID, O.UserName, O.OrderDate, O.FullName, O.TotalPriceOfOrder, O.Status,
                       I.ProductID, I.Image, I.ProductName, I.Quantity, I.Price, I.TotalPricePerProduct
                FROM Orders O
                INNER JOIN OrderItems I ON O.OrderID = I.OrderID";
            string sqlDataSource = _configuration.GetConnectionString("StoreDBCon");
            DataTable table = new DataTable();
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand command = new SqlCommand(query, myConn))
                {
                    myReader = command.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpGet("{UserName}")]
        public JsonResult GetOrdersByUserName(string UserName)
        {
            string query = @"
        SELECT O.OrderID, O.UserName, O.OrderDate, O.FullName, O.TotalPriceOfOrder, O.Status,
               I.ProductID, I.Image, I.ProductName, I.Quantity, I.Price, I.TotalPricePerProduct
        FROM Orders O
        INNER JOIN OrderItems I ON O.OrderID = I.OrderID
        WHERE O.UserName = @UserName";

            string sqlDataSource = _configuration.GetConnectionString("StoreDBCon");
            DataTable table = new DataTable();
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand command = new SqlCommand(query, myConn))
                {
                    command.Parameters.AddWithValue("@UserName", UserName);
                    myReader = command.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost]
        public IActionResult PostOrder(OrderModel order)
        {
            order.OrderDate = DateTime.Now; // Gán thời gian hiện tại

            string sqlDataSource = _configuration.GetConnectionString("StoreDBCon");

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();

                // Thêm đơn hàng vào bảng Orders
                string orderQuery = @"
            INSERT INTO Orders (UserName, OrderDate, FullName, TotalPriceOfOrder, Status)
            VALUES (@UserName, @OrderDate, @FullName, @TotalPriceOfOrder, @Status);
            SELECT SCOPE_IDENTITY();"; // Lấy OrderID vừa tạo

                using (SqlCommand command = new SqlCommand(orderQuery, myConn))
                {
                    command.Parameters.AddWithValue("@UserName", order.UserName);
                    command.Parameters.AddWithValue("@OrderDate", order.OrderDate);
                    command.Parameters.AddWithValue("@FullName", order.FullName);
                    command.Parameters.AddWithValue("@TotalPriceOfOrder", order.TotalPriceOfOrder);
                    command.Parameters.AddWithValue("@Status", order.Status);

                    int orderID = Convert.ToInt32(command.ExecuteScalar());

                    // Thêm các sản phẩm vào bảng OrderItems
                    foreach (var product in order.Products)
                    {
                        string productQuery = @"
                    INSERT INTO OrderItems (OrderID, ProductID, Image, ProductName, Quantity, Price, TotalPricePerProduct)
                    VALUES (@OrderID, @ProductID, @Image, @ProductName, @Quantity, @Price, @TotalPricePerProduct)";

                        using (SqlCommand productCommand = new SqlCommand(productQuery, myConn))
                        {
                            productCommand.Parameters.AddWithValue("@OrderID", orderID);
                            productCommand.Parameters.AddWithValue("@ProductID", product.ProductID);
                            productCommand.Parameters.AddWithValue("@Image", product.Image);
                            productCommand.Parameters.AddWithValue("@ProductName", product.ProductName);
                            productCommand.Parameters.AddWithValue("@Quantity", product.Quantity);
                            productCommand.Parameters.AddWithValue("@Price", product.Price);
                            productCommand.Parameters.AddWithValue("@TotalPricePerProduct", product.TotalPricePerProduct);

                            productCommand.ExecuteNonQuery();
                        }
                    }
                }

                myConn.Close();
            }

            return Ok("Order Created Successfully");
        }

        // Phương thức để lấy giá trị OrderID mới
        private int GetNextOrderID(SqlConnection connection)
        {
            int nextID = 0;
            string query = "SELECT ISNULL(MAX(OrderID), 0) + 1 FROM Orders";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                nextID = Convert.ToInt32(command.ExecuteScalar());
            }
            return nextID;
        }

        [HttpDelete("{UserName}/{OrderID}")]
        public JsonResult DeleteOrderByUserNameAndOrderID(string UserName, int OrderID)
        {
            string deleteOrderItemsQuery = @"
        DELETE FROM OrderItems 
        WHERE OrderID = @OrderID AND OrderID IN (SELECT OrderID FROM Orders WHERE UserName = @UserName)";

            string deleteOrderQuery = "DELETE FROM Orders WHERE UserName = @UserName AND OrderID = @OrderID";
            string sqlDataSource = _configuration.GetConnectionString("StoreDBCon");

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();

                using (SqlTransaction transaction = myConn.BeginTransaction())
                {
                    try
                    {
                        // Delete related OrderItems first
                        using (SqlCommand deleteOrderItemsCommand = new SqlCommand(deleteOrderItemsQuery, myConn, transaction))
                        {
                            deleteOrderItemsCommand.Parameters.AddWithValue("@UserName", UserName);
                            deleteOrderItemsCommand.Parameters.AddWithValue("@OrderID", OrderID);
                            deleteOrderItemsCommand.ExecuteNonQuery();
                        }

                        // Delete the Order
                        using (SqlCommand deleteOrderCommand = new SqlCommand(deleteOrderQuery, myConn, transaction))
                        {
                            deleteOrderCommand.Parameters.AddWithValue("@UserName", UserName);
                            deleteOrderCommand.Parameters.AddWithValue("@OrderID", OrderID);
                            deleteOrderCommand.ExecuteNonQuery();
                        }

                        // Commit transaction
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // Rollback transaction in case of error
                        transaction.Rollback();
                        return new JsonResult($"Error: {ex.Message}");
                    }
                }

                myConn.Close();
            }

            return new JsonResult("Order Deleted Successfully");
        }

        [HttpPut("{userName}/{orderId}")]
        public async Task<IActionResult> UpdateOrderStatus(string userName, int orderId, [FromBody] string status)
        {
            // Update order status in the database
            string updateQuery = "UPDATE Orders SET Status = @Status WHERE UserName = @UserName AND OrderID = @OrderID";

            string sqlDataSource = _configuration.GetConnectionString("StoreDBCon");

            using (SqlConnection connection = new SqlConnection(sqlDataSource))
            {
                using (SqlCommand command = new SqlCommand(updateQuery, connection))
                {
                    command.Parameters.AddWithValue("@Status", status);
                    command.Parameters.AddWithValue("@UserName", userName);
                    command.Parameters.AddWithValue("@OrderID", orderId);

                    connection.Open();
                    int rowsAffected = await command.ExecuteNonQueryAsync();
                    connection.Close();

                    if (rowsAffected > 0)
                    {
                        // Send notification to clients
                        await SendOrderUpdateNotification();
                        return Ok("Order status updated successfully");
                    }
                    else
                    {
                        return NotFound("Order not found or status not updated");
                    }
                }
            }
        }

        private async Task SendOrderUpdateNotification()
        {
            // Send a notification to clients
            await _hubContext.Clients.All.SendAsync("ReceiveOrderUpdate", "A new order has been created or updated");
        }
    }
}
