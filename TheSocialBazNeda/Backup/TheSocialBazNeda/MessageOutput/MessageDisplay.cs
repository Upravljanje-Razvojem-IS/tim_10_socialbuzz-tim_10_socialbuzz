using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace TheSocialBazNeda.MessageOutput
{
    public class Message
    {
        public HttpStatusCode Code;
        public string ResponseMessage;
    }
    public class MessageDisplay
    {
        public JToken Message(HttpStatusCode httpStatusCode, string message)
        {
            var jsonObj = new Message
            {
                Code = httpStatusCode,
                ResponseMessage = message
            };
            return JToken.Parse(JsonConvert.SerializeObject(jsonObj));
        }
    }
}