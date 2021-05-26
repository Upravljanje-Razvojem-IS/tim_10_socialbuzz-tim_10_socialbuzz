using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using TheSocialBazNeda.Authentication;
using TheSocialBazNeda.Models;

namespace TheSocialBazNeda.Controllers
{
    public class AccountController : ApiController
    {
        public int CreateCorporateAccount(int userID, CorporateAccountModel corporateAccountModel)
        {
            string mainconn = ConfigurationManager.ConnectionStrings["TheSocialBaz"].ConnectionString;
            SqlConnection sqlConnection = new SqlConnection(mainconn);
            string queryInsert = "insert into [dbo].[corporateAccount] (ownerID, pib, companyName, companyCity, companyAddress, companyEmail, companyMobile)" +
                    " values (@ownerID, @pib, @companyName, @companyCity, @companyAddress, @companyEmail, @companyMobile)";
            SqlCommand sqlCommandInsert = new SqlCommand(queryInsert, sqlConnection);
            sqlConnection.Open();
            sqlCommandInsert.Parameters.AddWithValue("@ownerID", userID);
            sqlCommandInsert.Parameters.AddWithValue("@pib", corporateAccountModel.pib);
            sqlCommandInsert.Parameters.AddWithValue("@companyName", corporateAccountModel.companyName);
            sqlCommandInsert.Parameters.AddWithValue("@companyCity", corporateAccountModel.companyCity);
            sqlCommandInsert.Parameters.AddWithValue("@companyAddress", corporateAccountModel.companyAddress);
            sqlCommandInsert.Parameters.AddWithValue("@companyEmail", corporateAccountModel.companyEmail);
            sqlCommandInsert.Parameters.AddWithValue("@companyMobile", corporateAccountModel.companyMobile);

            try
            {
                sqlCommandInsert.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("chk_pib_constraint"))
                {
                    return 1;
                }
                else if (ex.Message.Contains("chk_mobile_company_constraint"))
                {
                    return 2;
                }
                else if (ex.Message.Contains("chk_company_email"))
                {
                    return 3;
                }
                else if (ex.Message.Contains("uq_company_pib"))
                {
                    return 4;
                }
                else if (ex.Message.Contains("uq_company_email"))
                {
                    return 5;
                }
                else if (ex.Message.Contains("uq_company_mobile"))
                {
                    return 6;
                }
            }
            sqlConnection.Close();
            return 0;

        }
    }
}
