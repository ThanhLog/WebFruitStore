using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private IConfiguration _configuration;
        public AccountsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpGet]
        public JsonResult AccountGet()
        {
            string query = "SELECT * FROM Account";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("StoreDBCon");
            SqlDataReader myReader;

            using (SqlConnection myConn = new SqlConnection(sqlDataSource))
            {
                myConn.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myConn))
                {
                    myReader = sqlCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myConn.Close();
                }
            }

            return new JsonResult(table);
        }

        [HttpPost("check")]
        public IActionResult CheckAccountPost([FromBody] CheckModel model)
        {
            if (CheckCredentialsInDatabase(model.UserName, model.Password))
            {
                // Tạo token
                string token = GenerateToken(model.UserName);
                return Ok(new { token, user = new { UserName = model.UserName } });
            }
            else
            {
                return BadRequest(new { message = "Kiểm tra lại thông tin" });
            }
        }

        private string GenerateToken(string userName)
        {
            // Tạo token dựa trên thông tin người dùng, có thể sử dụng JWT hoặc các phương pháp khác
            // Trong ví dụ này, tạo một token ngẫu nhiên
            string token = Guid.NewGuid().ToString();
            return token;
        }

        private bool CheckCredentialsInDatabase(string UserName, string Password)
        {
            string sqlDataSource = _configuration.GetConnectionString("StoreDBCon");
            string query = "SELECT COUNT(*) FROM Account WHERE UserName = @UserName AND Password = @Password";

            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myCon))
                {
                    sqlCommand.Parameters.AddWithValue("@UserName", UserName);
                    sqlCommand.Parameters.AddWithValue("@Password", Password);

                    int count = (int)sqlCommand.ExecuteScalar();
                    myCon.Close();
                    return count > 0;
                }
            }
        }

        public class CheckModel
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

        [HttpPost("create")]
        public IActionResult CreateAccount([FromBody] AccountModel account)
        {
            // Kiểm tra tồn tại của UserName
            bool isUserNameExists = CheckUserNameExists(account.UserName);
            if (isUserNameExists)
            {
                return BadRequest(new { message = "UserName đã tồn tại" });
            }

            // Kiểm tra tồn tại của Email
            bool isEmailExists = CheckEmailExists(account.Email);
            if (isEmailExists)
            {
                return BadRequest(new { message = "Email đã tồn tại" });
            }

            string query = @"
        INSERT INTO Account (UserName, Email, Password, CreateDate) 
        VALUES (@UserName, @Email, @Password, GETDATE())";

            using (SqlConnection myCon = new SqlConnection(_configuration.GetConnectionString("StoreDBCon")))
            {
                myCon.Open();
                using (SqlCommand sqlCommand = new SqlCommand(query, myCon))
                {
                    sqlCommand.Parameters.AddWithValue("@UserName", account.UserName);
                    sqlCommand.Parameters.AddWithValue("@Email", account.Email);
                    sqlCommand.Parameters.AddWithValue("@Password", account.Password);

                    int rowsAffected = sqlCommand.ExecuteNonQuery();
                    myCon.Close();

                    if (rowsAffected > 0)
                    {
                        return Ok(new { message = "Tạo tài khoản thành công" });
                    }
                    else
                    {
                        return BadRequest(new { message = "Tạo tài khoản thất bại" });
                    }
                }
            }
        }


        private bool CheckUserNameExists(string userName)
        {
            string query = "SELECT COUNT(*) FROM Account WHERE UserName = @UserName";
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("StoreDBCon")))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserName", userName);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    connection.Close();
                    return count > 0;
                }
            }
        }

        private bool CheckEmailExists(string email)
        {
            string query = "SELECT COUNT(*) FROM Account WHERE Email = @Email";
            using (SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("StoreDBCon")))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Email", email);
                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    connection.Close();
                    return count > 0;
                }
            }
        }

        [HttpGet("{UserName}")]
        public JsonResult GetUserName(string UserName)
        {
            string query = "SELECT * FROM Users WHERE UserName = @UserName";
            DataTable table = new DataTable();
            string dataSource = _configuration.GetConnectionString("StoreDBCon");
            SqlDataReader myReader;

            using (SqlConnection myCon = new SqlConnection(dataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@UserName", UserName);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myCon.Close();
                    myReader.Close();
                }
            }

            return new JsonResult(table);
        }
    }
}
