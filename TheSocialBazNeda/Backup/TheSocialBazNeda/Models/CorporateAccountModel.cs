using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSocialBazNeda.Models
{
    public class CorporateAccountModel
    {
        public int corporateID { get; set; }
        public int ownerID { get; set; }
        public DateTime dateOfCreation { get; set; }
        public string pib { get; set; }
        public string companyName { get; set; }
        public string companyCity { get; set; }
        public string companyAddress { get; set; }
        public string companyEmail { get; set; }
        public string companyMobile { get; set; }
    }
}