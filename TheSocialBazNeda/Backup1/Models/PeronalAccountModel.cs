using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSocialBazNeda.Models
{
    public class PeronalAccountModel
    {
        public int userID { get; set; }
        public DateTime dateOfCreation { get; set; }
        public double balance { get; set; }
    }
}