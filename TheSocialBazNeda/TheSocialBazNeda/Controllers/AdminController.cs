using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Threading;
using System.Web.Http;
using TheSocialBazNeda.Authentication;
using TheSocialBazNeda.MessageOutput;
using TheSocialBazNeda.Models;

namespace TheSocialBazNeda.Controllers
{
    public class AdminController : ApiController
    {
        [Route("api/admin/login")]
        [HttpGet]
        public IHttpActionResult LoginAdmin()
        {
            UserAuthentication authentication = new UserAuthentication();
            MessageDisplay messageDisplay = new MessageDisplay();
            HttpRequestHeaders headers = Request.Headers;

            string[] userinfo = authentication.GetUserLoginInfo(headers);
            if (userinfo == null)
            {
                return Content(HttpStatusCode.Unauthorized, messageDisplay.Message(HttpStatusCode.Unauthorized, "The Base64 encoded message in AUthorization header is not in correct format!"));
            }

            string mainconn = ConfigurationManager.ConnectionStrings["TheSocialBaz"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconn);
            sqlConnection.Open();
            string query = "select * from [dbo].[administrator] where (adminEmail = '" + userinfo[0] + "' or adminUsername = '" + userinfo[0] + "') and adminPassword =" + "'" + userinfo[1] + "'";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sdr = sqlCommand.ExecuteReader();


            if (sdr.HasRows)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userinfo[0]), null);
                UserAuthentication.Username = Thread.CurrentPrincipal.Identity.Name;
                return Content(HttpStatusCode.OK, messageDisplay.Message(HttpStatusCode.OK, "The admin is logged in successfully!"));
            }
            else
            {
                return Content(HttpStatusCode.Unauthorized, messageDisplay.Message(HttpStatusCode.Unauthorized, "The admin authentiaction failed!"));
            }
        }

        [Route("api/admin/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateAdmin(int id, AdministratorModel admin)
        {
            MessageDisplay messageDisplay = new MessageDisplay();
            string mainconn = ConfigurationManager.ConnectionStrings["TheSocialBaz"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconn);
            sqlConnection.Open();
            string query = "select adminUsername from [dbo].[administrator] where adminID = " + id;
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sdr = sqlCommand.ExecuteReader();

            if (!sdr.HasRows)
            {
                return Content(HttpStatusCode.NotFound, messageDisplay.Message(HttpStatusCode.NotFound, "The admin is not an existing user!"));
            }
            else if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString().Equals(UserAuthentication.Username))
                {
                    sdr.Close();
                    string queryupdate = "update [dbo].[administrator]  set adminUsername = @adminUsername, adminName = @adminName, adminSurname = @adminSurname, " +
                        "adminEmail = @adminEmail, adminPassword = @adminPassword where adminID = " + id;
                    SqlCommand sqlCommandUpdate = new SqlCommand(queryupdate, sqlConnection);
                    sqlCommandUpdate.Parameters.AddWithValue("@adminUsername", admin.adminUsername);
                    sqlCommandUpdate.Parameters.AddWithValue("@adminName", admin.adminName);
                    sqlCommandUpdate.Parameters.AddWithValue("@adminSurname", admin.adminSurname);
                    sqlCommandUpdate.Parameters.AddWithValue("@adminEmail", admin.adminEmail);
                    sqlCommandUpdate.Parameters.AddWithValue("@adminPassword", admin.adminPassword);
                    try
                    {
                        SqlDataReader sdrUpdate = sqlCommandUpdate.ExecuteReader();
                        sdrUpdate.Close();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("uq_admin_username"))
                        {
                            return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "Admin with the given username already exists!"));
                        }
                        else if (ex.Message.Contains("uq_admin_email"))
                        {
                            return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "Admin with the given email already exists!"));
                        }
                        else if (ex.Message.Contains("chk_admin_email"))
                        {
                            return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "The email is incorrect format! It musy be in the format example@example.com"));
                        }
                    }
                }
                else
                {
                    sqlConnection.Close();
                    return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "You are not authorized to update this admin!"));
                }
                return Content(HttpStatusCode.Accepted, messageDisplay.Message(HttpStatusCode.Accepted, "The admin was successfully updated!"));
            }
            else
            {
                sqlConnection.Close();
                return Content(HttpStatusCode.BadRequest, messageDisplay.Message(HttpStatusCode.BadRequest, "There was an issue executing the request!"));
            }
        }

        [Route("api/admin/register")]
        [HttpPost]
        public IHttpActionResult RegisterUser(AdministratorModel admin)
        {
            MessageDisplay messageDisplay = new MessageDisplay();
            AccountController accountController = new AccountController();
            string mainconn = ConfigurationManager.ConnectionStrings["TheSocialBaz"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconn);
            string queryInsert = "insert into [dbo].[administrator] (adminUsername, adminName, adminSurname, adminPassword, adminEmail)" +
                    " values (@adminUsername, @adminName, @adminSurname, @adminPassword, @adminEmail)";
            SqlCommand sqlCommandInsert = new SqlCommand(queryInsert, sqlConnection);
            sqlConnection.Open();
            sqlCommandInsert.Parameters.AddWithValue("@adminUsername", admin.adminUsername);
            sqlCommandInsert.Parameters.AddWithValue("@adminName", admin.adminName);
            sqlCommandInsert.Parameters.AddWithValue("@adminSurname", admin.adminSurname);
            sqlCommandInsert.Parameters.AddWithValue("@adminPassword", admin.adminPassword);
            sqlCommandInsert.Parameters.AddWithValue("@adminEmail", admin.adminEmail);

            if (UserAuthentication.Username == null)
            {
                try
                {
                    sqlCommandInsert.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("uq_admin_username"))
                    {
                        return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "Admin with the given username already exists!"));
                    }
                    else if (ex.Message.Contains("uq_admin_email"))
                    {
                        return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "Admin with the given email already exists!"));
                    }
                    else if (ex.Message.Contains("chk_admin_email"))
                    {
                        return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "The email is incorrect format! It musy be in the format example@example.com"));
                    }
                }
                sqlConnection.Close();
                return Content(HttpStatusCode.Created, messageDisplay.Message(HttpStatusCode.Created, "The admin was successfully registered!"));
            }
            else
            {
                return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "You are already signed in as another admin!"));
            }

        }
    }
}
