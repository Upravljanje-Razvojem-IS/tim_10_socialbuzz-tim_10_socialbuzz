using System;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using TheSocialBazNeda.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Http.Headers;
using TheSocialBazNeda.Authentication;
using TheSocialBazNeda.MessageOutput;
using System.Threading;
using System.Security.Principal;

namespace TheSocialBazNeda.Controllers
{
    public class UserController : ApiController
    {
        public static string username;
        [Route("api/user/login")]
        [HttpGet]
        public IHttpActionResult LoginUser() {
            UserAuthentication authentication = new UserAuthentication();
            MessageDisplay messageDisplay = new MessageDisplay();
            HttpRequestHeaders headers = Request.Headers;

            string [] userinfo = authentication.GetUserLoginInfo(headers);
            if (userinfo == null)
            {
                return Content(HttpStatusCode.Unauthorized, messageDisplay.Message(HttpStatusCode.Unauthorized, "The Authorization header is incorrect!"));
            }

            string mainconn = ConfigurationManager.ConnectionStrings["TheSocialBaz"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconn);
            sqlConnection.Open();
            string query = "select * from [dbo].[user] where (userEmail = '" + userinfo[0] + "' or userUsername = '" + userinfo[0] + "') and userPassword =" + "'" + userinfo[1] + "'";
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sdr = sqlCommand.ExecuteReader();


            if (sdr.HasRows)
            {
                Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(userinfo[0]), null);
                username = Thread.CurrentPrincipal.Identity.Name;
                return Content(HttpStatusCode.OK, messageDisplay.Message(HttpStatusCode.OK, "The user is logged in successfully!"));
            }
            else {
                return Content(HttpStatusCode.Unauthorized, messageDisplay.Message(HttpStatusCode.Unauthorized, "The user authentiaction failed!")); 
            }
        }

        [Route("api/user/search")]
        [HttpGet]
        public IHttpActionResult SearchUsers()
        {
            MessageDisplay messageDisplay = new MessageDisplay();
            List<UserModel> users = new List<UserModel>();
            string mainconn = ConfigurationManager.ConnectionStrings["TheSocialBaz"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconn);
            sqlConnection.Open();
            string queryID = "select userID from dbo.[user] usr where userUsername = " + "'" + username + "'";
            SqlCommand sqlCommandID = new SqlCommand(queryID, sqlConnection);
            SqlDataReader sdrID = sqlCommandID.ExecuteReader();

            if (!sdrID.HasRows)
            {
                return Content(HttpStatusCode.NotFound, messageDisplay.Message(HttpStatusCode.NotFound, "The user is not an existing user!"));
            }
            else if (sdrID.Read())
            {
                string querySearch = "select * " +
                       "from dbo.[user] usr " +
                       "where usr.userID not in (select blockedUserID from blockedUsers where userID = " + Convert.ToInt32(sdrID.GetValue(0)) + ") " +
                       "and usr.userID not in (select userID from blockedUsers where blockedUserID = " + Convert.ToInt32(sdrID.GetValue(0)) + ")" +
                       "and userid != " + Convert.ToInt32(sdrID.GetValue(0));
                sdrID.Close();
                SqlCommand sqlCommandSearch = new SqlCommand(querySearch, sqlConnection);
                SqlDataReader sdrSearch = sqlCommandSearch.ExecuteReader();
                while (sdrSearch.Read())
                {
                    users.Add(new UserModel()
                    {
                        userID = Convert.ToInt32(sdrSearch.GetValue(0)),
                        userUsername = sdrSearch.GetValue(1).ToString(),
                        userName = sdrSearch.GetValue(2).ToString(),
                        userSurname = sdrSearch.GetValue(3).ToString(),
                        userEmail = sdrSearch.GetValue(4).ToString(),
                        userAddress = sdrSearch.GetValue(5).ToString(),
                        userCity = sdrSearch.GetValue(6).ToString(),
                        userMobile = sdrSearch.GetValue(7).ToString(),
                        userPassword = sdrSearch.GetValue(8).ToString()
                    });
                }
                sdrSearch.Close();
            }
            else 
            {
                sqlConnection.Close();
                return Content(HttpStatusCode.BadRequest, messageDisplay.Message(HttpStatusCode.BadRequest, "There was an issue executing the request!"));
            }
            sqlConnection.Close();
            return Ok(users);
        }

        [Route("api/user/{id}")]
        [HttpGet]
        public IHttpActionResult GetUser(int id)
        {
            List<UserModel> user = new List<UserModel>();
            string mainconn = ConfigurationManager.ConnectionStrings["TheSocialBaz"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconn);
            sqlConnection.Open();
            string query = "select * from [dbo].[user] where userID = " + id;
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sdr = sqlCommand.ExecuteReader();
            while (sdr.Read())
            {
                user.Add(new UserModel()
                {
                    userID = Convert.ToInt32(sdr.GetValue(0)),
                    userUsername = sdr.GetValue(1).ToString(),
                    userName = sdr.GetValue(2).ToString(),
                    userSurname = sdr.GetValue(3).ToString(),
                    userEmail = sdr.GetValue(4).ToString(),
                    userAddress = sdr.GetValue(5).ToString(),
                    userCity = sdr.GetValue(6).ToString(),
                    userMobile = sdr.GetValue(7).ToString(),
                    userPassword = sdr.GetValue(8).ToString()
                });
            }
            sqlConnection.Close();
            sdr.Close();
            return Ok(user);
        }

        [Route("api/user/{id}")]
        [HttpDelete]
        public IHttpActionResult DeleteUser(int id)
        {
            MessageDisplay messageDisplay = new MessageDisplay();
            List<UserModel> user = new List<UserModel>();
            string mainconn = ConfigurationManager.ConnectionStrings["TheSocialBaz"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconn);
            sqlConnection.Open();
            string query = "select userUsername from [dbo].[user] where userID = " + id;
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sdr = sqlCommand.ExecuteReader();

            if (!sdr.HasRows) {
                return Content(HttpStatusCode.NotFound, messageDisplay.Message(HttpStatusCode.NotFound, "The user is not an existing user!"));
            }
            else if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString().Equals(username) || username.Equals("admin"))
                {
                    sdr.Close();
                    string queryDelete = "delete from [dbo].[user] where userID = " + id;
                    SqlCommand sqlCommandDelete = new SqlCommand(queryDelete, sqlConnection);
                    SqlDataReader sdrDelete = sqlCommandDelete.ExecuteReader();
                    sqlConnection.Close();
                    sdrDelete.Close();
                    return Content(HttpStatusCode.OK, messageDisplay.Message(HttpStatusCode.OK, "You have successfully deleted an account!"));
                }
                else {
                    sqlConnection.Close();
                    return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "You are not authorized to delete this user!"));
                }
            }
            else
            {
                sqlConnection.Close();
                return Content(HttpStatusCode.BadRequest, messageDisplay.Message(HttpStatusCode.BadRequest, "There was an issue executing the request!"));
            }
        }

        [Route("api/user/{id}")]
        [HttpPut]
        public IHttpActionResult UpdateUser(int id, UserModel user)
        {
            MessageDisplay messageDisplay = new MessageDisplay();
            string mainconn = ConfigurationManager.ConnectionStrings["TheSocialBaz"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconn);
            sqlConnection.Open();
            string query = "select userUsername from [dbo].[user] where userID = " + id;
            SqlCommand sqlCommand = new SqlCommand(query, sqlConnection);
            SqlDataReader sdr = sqlCommand.ExecuteReader();

            if (!sdr.HasRows)
            {
                return Content(HttpStatusCode.NotFound, messageDisplay.Message(HttpStatusCode.NotFound, "The user is not an existing user!"));
            }
            else if (sdr.Read())
            {
                if (sdr.GetValue(0).ToString().Equals(username))
                {
                    sdr.Close();
                    string queryupdate = "update [dbo].[user]  set userUsername = @userUsername, userName = @userName, userSurname = @userSurname, " +
                        "userEmail = @userEmail, userAddress = @userAddress, userCity = @userCity, userMobile = @userMobile, userPassword = @userPassword where userid = " + id;
                    SqlCommand sqlCommandUpdate = new SqlCommand(queryupdate, sqlConnection);
                    sqlCommandUpdate.Parameters.AddWithValue("@userUsername", user.userUsername);
                    sqlCommandUpdate.Parameters.AddWithValue("@userName", user.userName);
                    sqlCommandUpdate.Parameters.AddWithValue("@userSurname", user.userSurname);
                    sqlCommandUpdate.Parameters.AddWithValue("@userEmail", user.userEmail);
                    sqlCommandUpdate.Parameters.AddWithValue("@userAddress", user.userAddress);
                    sqlCommandUpdate.Parameters.AddWithValue("@userCity", user.userCity);
                    sqlCommandUpdate.Parameters.AddWithValue("@userMobile", user.userMobile);
                    sqlCommandUpdate.Parameters.AddWithValue("@userPassword", user.userPassword);
                    try
                    {
                        SqlDataReader sdrUpdate = sqlCommandUpdate.ExecuteReader();
                        sdrUpdate.Close();
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message.Contains("uq_username"))
                        {
                            return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "User with the given username already exists!"));
                        }
                        else if (ex.Message.Contains("uq_email"))
                        {
                            return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "User with the given email already exists!"));
                        }
                        else if (ex.Message.Contains("uq_mobile"))
                        {
                            return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "User with the given mobile number already exists!"));
                        }
                        else if (ex.Message.Contains("chk_user_email"))
                        {
                            return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "The email is incorrect format! It musy be in the format example@example.com"));
                        }
                        else if (ex.Message.Contains("chk_user_email"))
                        {
                            return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "The mobile phone number is incorrect format! It can only contain numbers"));
                        }
                    }
                }
                else {
                    sqlConnection.Close();
                    return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "You are not authorized to update this user!"));
                }
                return Content(HttpStatusCode.Accepted, messageDisplay.Message(HttpStatusCode.Accepted, "The user was successfully updated!"));
            }
            else
            {
                sqlConnection.Close();
                return Content(HttpStatusCode.BadRequest, messageDisplay.Message(HttpStatusCode.BadRequest, "There was an issue executing the request!"));
            }
        }

        [Route("api/user/register")]
        [HttpPost]
        public IHttpActionResult RegisterUser(UserModel user)
        {
            MessageDisplay messageDisplay = new MessageDisplay();
            string mainconn = ConfigurationManager.ConnectionStrings["TheSocialBaz"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconn);
            string queryInsert = "insert into [dbo].[user] (userUsername, userName, userSurname, userEmail, userAddress, userCity, userMobile, userPassword)" +
                    " values (@userUsername, @userName, @userSurname, @userEmail, @userAddress, @userCity, @userMobile, @userPassword)";
            SqlCommand sqlCommandInsert = new SqlCommand(queryInsert, sqlConnection);
            sqlConnection.Open();
            sqlCommandInsert.Parameters.AddWithValue("@userUsername", user.userUsername);
            sqlCommandInsert.Parameters.AddWithValue("@userName", user.userName);
            sqlCommandInsert.Parameters.AddWithValue("@userSurname", user.userSurname);
            sqlCommandInsert.Parameters.AddWithValue("@userEmail", user.userEmail);
            sqlCommandInsert.Parameters.AddWithValue("@userAddress", user.userAddress);
            sqlCommandInsert.Parameters.AddWithValue("@userCity", user.userCity);
            sqlCommandInsert.Parameters.AddWithValue("@userMobile", user.userMobile);
            sqlCommandInsert.Parameters.AddWithValue("@userPassword", user.userPassword);

            if (username == null)
            {
                try
                {
                    sqlCommandInsert.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("uq_username"))
                    {
                        return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "User with the given username already exists!"));
                    }
                    else if (ex.Message.Contains("uq_email"))
                    {
                        return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "User with the given email already exists!"));
                    }
                    else if (ex.Message.Contains("uq_mobile"))
                    {
                        return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "User with the given mobile number already exists!"));
                    }
                    else if (ex.Message.Contains("chk_user_email"))
                    {
                        return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "The email is incorrect format! It musy be in the format example@example.com"));
                    }
                    else if (ex.Message.Contains("chk_user_email"))
                    {
                        return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "The mobile phone number is incorrect format! It can only contain numbers"));
                    }
                }
                sqlConnection.Close();
                return Content(HttpStatusCode.Created, messageDisplay.Message(HttpStatusCode.Created, "The user was successfully registered!"));
            }
            else {
                return Content(HttpStatusCode.Forbidden, messageDisplay.Message(HttpStatusCode.Forbidden, "You are already signed in as another user!"));
            }

        }
    }
}
