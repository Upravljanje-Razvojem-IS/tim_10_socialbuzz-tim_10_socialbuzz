using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSocialBazNeda.Models
{
    public class AdministratorModel
    {
        public int adminID { get; set; }
        public string adminUsername { get; set; }
        public string adminName { get; set; }
        public string adminSurname { get; set; }
        public string adminEmail { get; set; }
        public string adminPassword { get; set; }
    }
}