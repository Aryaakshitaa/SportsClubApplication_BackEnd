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
    public class PlayerController : ApiController
    {
        public HttpResponseMessage Get()
        {
            string query = @"select PlayerId, PlayerName, Sport, convert(varchar(10), DateOfJoining,120) as DateOfJoining, PhotoFileName from dbo.Player";
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
        public string Post(Player pl)
        {
            try
            {
                string query = @"insert into dbo.Player values(
                '" + pl.PlayerName + @"'
                ,'" + pl.Sport + @"'
                ,'" + pl.DateOfJoining + @"'
                ,'" + pl.PhotoFileName + @"'
                )";
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

        public string Put(Player pl)
        {
            try
            {
                string query = @"update dbo.Player set 
                PlayerName = '" + pl.PlayerName + @"' 
                ,Sport = '" + pl.Sport + @"' 
                ,DateOfJoining = '" + pl.DateOfJoining + @"' 
                ,PhotoFileName = '" + pl.PhotoFileName + @"' 
                where PlayerID=" + pl.PlayerId + @"";
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
                string query = @"delete from dbo.Player where PlayerID=" + id + @"";
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

        [Route("api/Player/GetAllSportNames")]
        [HttpGet]
        public HttpResponseMessage GetAllSportNames()
        {
            string query = @"select SportName from dbo.Sport";
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

        [Route("api/Player/SaveFile")]
        public string SaveFile()
        {
            try
            {
                var httpRequest = HttpContext.Current.Request;
                var PostedFile = httpRequest.Files[0];
                string filename = PostedFile.FileName;
                var pytsicalPath = HttpContext.Current.Server.MapPath("~/Photos/" + filename);
                PostedFile.SaveAs(pytsicalPath);
                return filename;
            }
            catch (Exception)
            {
                return "anonymous.png";
            }
        }
    }
}
