using CallerInfo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CallerInfo.Controllers
{
    public class HomeController : Controller
    {
       
        private static string conString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ToString();
        public ActionResult Index()
        {
            //ViewBag.ManagerEmail = new SelectList(getColumn("ManagerEmail"));
            //ViewBag.LimitHardOrSoft = new SelectList(getColumn("LimitHardOrSoft"));
            //ViewBag.LimitType = new SelectList(getColumn("LimitType"));
            //ViewBag.LimitCurrencyCode = new SelectList(getColumn("LimitCurrencyCode"));
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult GetMessages()
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            return PartialView("_MessagesList", _messageRepository.GetAllMessages());
        }

        public ActionResult ApplyFilter(string ManagerEmail, string LimitHardOrSoft, string LimitType, string LimitCurrencyCode)
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            return PartialView("_MessagesList", _messageRepository.GetFilteredMessages(ManagerEmail, LimitHardOrSoft, LimitType, LimitCurrencyCode));
        }

        public ActionResult GetMessagesEmailWiseFilter(String ManagerEmail)
        {
            MessagesRepository _messageRepository = new MessagesRepository();
            return PartialView("_MessagesList", _messageRepository.GetMessagesEmailWiseFilter(ManagerEmail));
        }

        public String Insert(ems_ust_TradingLimits Lcl_TradingLimits)
        {
            DateTime dt = DateTime.Now;
            String retMessage = System.String.Empty;
            SqlConnection sql = new SqlConnection(conString);
            sql.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sql;
            cmd.CommandText = "insert into [ems.ust_TradingLimits] (ManagerEmail,LimitHardOrSoft,LimitType,LimitCurrencyCode,LimitValue,LimitOverride,CreateDateTime,ModifiedDateTime) values (@ManagerEmail,@LimitHardOrSoft,@LimitType,@LimitCurrencyCode,@LimitValue,@LimitOverride,@CreateDateTime,@ModifiedDateTime)";

            cmd.Parameters.AddWithValue("@ManagerEmail", Lcl_TradingLimits.ManagerEmail);
            cmd.Parameters.AddWithValue("@LimitHardOrSoft", Lcl_TradingLimits.LimitHardOrSoft);
            cmd.Parameters.AddWithValue("@LimitType", Lcl_TradingLimits.LimitType);
            cmd.Parameters.AddWithValue("@LimitCurrencyCode", Lcl_TradingLimits.LimitCurrencyCode);
            cmd.Parameters.AddWithValue("@LimitValue", Lcl_TradingLimits.LimitValue);

            cmd.Parameters.AddWithValue("@LimitOverride", Lcl_TradingLimits.LimitOverride);
            cmd.Parameters.AddWithValue("@CreateDateTime", dt);
            cmd.Parameters.AddWithValue("@ModifiedDateTime", dt);  

            try
            {
                cmd.ExecuteNonQuery();
                sql.Close();
                return retMessage;
            }

            catch (Exception)
            {
                return retMessage;
                throw;
            }
            
        }

        public ActionResult Bind(String Lcl_EmailValue, String Lcl_LimitHardOrSoftValue, String Lcl_LimitTypeValue, String Lcl_LimitCurrencyCodeValue)
        {
            SqlConnection sql = new SqlConnection(conString);
            sql.Open();
           
            DataTable dt = new DataTable();
            List<ems_ust_TradingLimits> TradingLimitdetails = new List<ems_ust_TradingLimits>();
            object ret;
            using (SqlCommand cmd = new SqlCommand("Select ManagerEmail,LimitHardOrSoft,LimitType,LimitCurrencyCode,LimitValue,LimitOverride,CreateDateTime,ModifiedDateTime from [ems.ust_TradingLimits] where ManagerEmail='" + Lcl_EmailValue + "' and LimitHardOrSoft='"+ Lcl_LimitHardOrSoftValue + "' and LimitType='"+ Lcl_LimitTypeValue + "' and LimitCurrencyCode='"+ Lcl_LimitCurrencyCodeValue + "' ORDER BY ManagerEmail DESC", sql))
            {
                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                ret = new {
                    ManagerEmail= dt.Rows[0]["ManagerEmail"].ToString(),
                    LimitHardOrSoft = dt.Rows[0]["LimitHardOrSoft"].ToString(),
                    LimitType = dt.Rows[0]["LimitType"].ToString(),
                    LimitCurrencyCode = dt.Rows[0]["LimitCurrencyCode"].ToString(),
                    LimitValue = dt.Rows[0]["LimitValue"].ToString(),
                    LimitOverride = dt.Rows[0]["LimitOverride"].ToString()
                };

                sql.Close();
            }

            return Json(ret, JsonRequestBehavior.AllowGet);//TradingLimitdetails.ToArray();
        }


        public ActionResult Email()
        {
            SqlConnection sql = new SqlConnection(conString);
            sql.Open();
            Manager m = new Manager();
            DataTable dt = new DataTable();
            List<ems_ust_TradingLimits> TradingLimitdetails = new List<ems_ust_TradingLimits>();
            object ret;
            using (SqlCommand cmd = new SqlCommand("Select ManagerEmail from [ems.ust_TradingLimits] ORDER BY ManagerEmail ASC", sql))
            {

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);

                foreach (DataRow item in dt.Rows)
                {
                    m.ManagerEmail = item["ManagerEmail"].ToString();
                    //Gbl_SenderHost = item["SenderHost"].ToString();
                    //Gbl_Port = item["SenderPort"].ToString();
                    //Gbl_SSL = item["SenderSSL"].ToString();

                }
                //m.ManagerEmail = dt.Rows[0]["ManagerEmail"].ToString();
                

                sql.Close();
            }

            return Json(m, JsonRequestBehavior.AllowGet);//TradingLimitdetails.ToArray();
        }

        public class Manager
        {
           public String ManagerEmail;
        }

        public Manager[] ManagerList()
        {


            SqlConnection sql = new SqlConnection(conString);
            sql.Open();
            DataTable dt = new DataTable();
            List<Manager> Managerdetails = new List<Manager>();

            using (SqlCommand cmd = new SqlCommand("Select ManagerEmail from [ems.ust_TradingLimits] ORDER BY ManagerEmail ASC", sql))
            {

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                foreach (DataRow dtrow in dt.Rows)
                {
                    Manager KID_Manager = new Manager();
                    KID_Manager.ManagerEmail = dtrow["ManagerEmail"].ToString();
                   
                    Managerdetails.Add(KID_Manager);
                }
                sql.Close();
            }

            return Managerdetails.ToArray();
        }

        public String Delete(String Lcl_EmailValue, String Lcl_LimitHardOrSoft, String Lcl_LimitType, String Lcl_LimitCurrencyCode)
        {           
            String retMessage = System.String.Empty;
            SqlConnection sql = new SqlConnection(conString);
            sql.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sql;
            cmd.CommandText = "delete from [ems.ust_TradingLimits] where ManagerEmail='" + Lcl_EmailValue + "' and LimitHardOrSoft='"+ Lcl_LimitHardOrSoft + "' and LimitType='"+ Lcl_LimitType + "' and LimitCurrencyCode='"+ Lcl_LimitCurrencyCode + "'";
            try { 
            cmd.ExecuteNonQuery();
                sql.Close();
            return retMessage;
            }
            catch(Exception ex)
            {
                return retMessage;
                throw;
            }
        }

        public String Update(ems_ust_TradingLimits Lcl_TradingLimits, String editingEmail, String editingLimitHardOrSoft, String editingLimitType, String editingLimitCurrencyCode)
        {
            DateTime dt = DateTime.Now;
            String retMessage = System.String.Empty;
            SqlConnection sql = new SqlConnection(conString);
            sql.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = sql;
            cmd.CommandText = "UPDATE [ems.ust_TradingLimits] SET ManagerEmail=@ManagerEmail , LimitHardOrSoft=@LimitHardOrSoft, LimitType=@LimitType, LimitCurrencyCode=@LimitCurrencyCode, LimitValue=@LimitValue, LimitOverride=@LimitOverride, ModifiedDateTime=@ModifiedDateTime WHERE ManagerEmail=@editingEmail and LimitHardOrSoft=@editingLimitHardOrSoft and LimitType=@editingLimitType and LimitCurrencyCode=@editingLimitCurrencyCode";
         
            cmd.Parameters.AddWithValue("@ManagerEmail", Lcl_TradingLimits.ManagerEmail);
            cmd.Parameters.AddWithValue("@editingEmail", editingEmail);
            cmd.Parameters.AddWithValue("@LimitHardOrSoft", Lcl_TradingLimits.LimitHardOrSoft);
            cmd.Parameters.AddWithValue("@editingLimitHardOrSoft", editingLimitHardOrSoft);
            cmd.Parameters.AddWithValue("@LimitType", Lcl_TradingLimits.LimitType);
            cmd.Parameters.AddWithValue("@editingLimitType", editingLimitType);
            cmd.Parameters.AddWithValue("@LimitCurrencyCode", Lcl_TradingLimits.LimitCurrencyCode);
            cmd.Parameters.AddWithValue("@editingLimitCurrencyCode", editingLimitCurrencyCode);
            cmd.Parameters.AddWithValue("@LimitValue", Lcl_TradingLimits.LimitValue);
            cmd.Parameters.AddWithValue("@LimitOverride", Lcl_TradingLimits.LimitOverride);
            cmd.Parameters.AddWithValue("@ModifiedDateTime", dt);

            try
            {
                cmd.ExecuteNonQuery();
                sql.Close();
                return retMessage;
            }

            catch (Exception)
            {
                return retMessage;
                throw;
            }

        }
       
        public ActionResult getSelect(string col)
        {
            return Json(getColumn(col), JsonRequestBehavior.AllowGet);
        }

        private List<string> getColumn(string col)
        {
            List<string> ret = new List<string>();
            string sqlExpression = @"SELECT distinct ["+col+"] FROM [dbo].[ems.ust_TradingLimits]";
            using (SqlConnection connection = new SqlConnection(conString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        ret.Add(reader.GetValue(0) as string);
                    }
                }

                reader.Close();
            }
            return ret;
        }

        }
    }