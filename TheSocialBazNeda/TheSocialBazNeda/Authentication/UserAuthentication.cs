using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace TheSocialBazNeda.Authentication
{
    public class UserAuthentication
    {
        public static string Username { get; set; } = null;
        public string[] GetUserLoginInfo(HttpRequestHeaders headersInput) {
            HttpRequestHeaders headers = headersInput;
            string[] usernamePassword = null;
            if (!headers.Contains("Authorization"))
            {
                return null;
            }
            else
            {
                string authenticationToken = headers.Authorization.Parameter;
                try
                {
                    string decodedAUthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authenticationToken));
                    usernamePassword = decodedAUthenticationToken.Split(':');
                }
                catch (Exception ex) {
                    return null;
                }
                return usernamePassword;
            }
        }
    }
}