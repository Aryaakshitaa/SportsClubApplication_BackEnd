using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class UsersController : ApiController
    {

        public string Post(Users user)
        {
            try
            {
                string query = @"insert into dbo.Users values(
                '" + user.email + @"'
                ,'" + user.password + @"'
                )";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["SportsClubAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Signed Up Successfully";
            }
            catch (Exception)
            {
                return "Sign Up Failed!!";
            }
        }

        [Route("api/Users/GetAllUsers")]
        [HttpGet]
        public HttpResponseMessage GetAllUsers()
        {
            string query = @"select email, password from dbo.Users";
            DataTable table = new DataTable();
            using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["SportsClubAppDB"].ConnectionString))
            using (var cmd = new SqlCommand(query, con))
            using (var da = new SqlDataAdapter(cmd))
            {
                cmd.CommandType = CommandType.Text;
                da.Fill(table);
            }
            return Request.CreateResponse(HttpStatusCode.OK, table);
        }

    }
}
