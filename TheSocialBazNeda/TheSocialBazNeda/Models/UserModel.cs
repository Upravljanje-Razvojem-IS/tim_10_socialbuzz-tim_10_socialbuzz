using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSocialBazNeda.Models
{
    public class UserModel
    {
        public int userID { get; set; }
        public string userUsername { get; set; }
        public string userName { get; set; }
        public string userSurname { get; set; }
        public string userEmail { get; set; }
        public string userAddress { get; set; }
        public string userCity { get; set; }
        public string userMobile { get; set; }
        public string userPassword { get; set; }
    }
}