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

namespace TheSocialBazNeda.Controllers
{
    public class AdminController : ApiController
    {
        [Route("api/admin/login")]
        [HttpGet]
        public IHttpActionResult LoginUser()
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
    }
}
