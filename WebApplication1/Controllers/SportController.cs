using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class SportController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"select SportId, SportName from dbo.Sport";
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
        public string Post(Sport sp)
        {
            try
            {
                string query = @"insert into dbo.Sport values('" + sp.SportName + @"')";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["SportsClubAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Added Successfully";
            }
            catch (Exception)
            {
                return "Could not be added!!";
            }
        }

        public string Put(Sport sp)
        {
            try
            {
                string query = @"update dbo.Sport set SportName = '" + sp.SportName + @"' where SportID=" + sp.SportId + @"";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["SportsClubAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Updated Successfully";
            }
            catch (Exception)
            {
                return "Could not be updated!!";
            }
        }
        public string Delete(int id)
        {
            try
            {
                string query = @"delete from dbo.Sport where SportID=" + id + @"";
                DataTable table = new DataTable();
                using (var con = new SqlConnection(ConfigurationManager.ConnectionStrings["SportsClubAppDB"].ConnectionString))
                using (var cmd = new SqlCommand(query, con))
                using (var da = new SqlDataAdapter(cmd))
                {
                    cmd.CommandType = CommandType.Text;
                    da.Fill(table);
                }
                return "Deleted Successfully";
            }
            catch (Exception)
            {
                return "Could not be deleted!!";
            }
        }
    }
}
