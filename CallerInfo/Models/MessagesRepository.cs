using CallerInfo.Hubs;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;

namespace CallerInfo.Models
{
    public class MessagesRepository
    {
        readonly string _connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

        public IEnumerable<Messages> GetAllMessages()
        {
            var messages = new List<Messages>();
            using (var connection = new SqlConnection(_connString))
            {
                string UserName = HttpContext.Current.User.Identity.Name;
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [ManagerEmail], [LimitHardOrSoft], [LimitType], [LimitCurrencyCode], [LimitValue],[LimitOverride], [CreateDateTime], [ModifiedDateTime] FROM [dbo].[ems.ust_TradingLimits]", connection))
                {
                    command.Notification = null;
                    bool startResult = SqlDependency.Start(_connString);

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messages.Add(item: new Messages { ManagerEmail = (string)reader["ManagerEmail"], LimitHardOrSoft = (string)reader["LimitHardOrSoft"], LimitType = (string)(reader["LimitType"]), LimitCurrencyCode = (string)reader["LimitCurrencyCode"], LimitValue = (string)reader["LimitValue"], LimitOverride = (Boolean)(reader["LimitOverride"]), CreateDateTime = (DateTime)reader["CreateDateTime"], ModifiedDateTime = (DateTime)(reader["ModifiedDateTime"]) });
                    }
                }

            }

            return messages;
        }

        private void dependency_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Type == SqlNotificationType.Change)
            {
                TestHub.SendMessages();
            }
        }

        public IEnumerable<Messages> GetMessagesEmailWiseFilter(String ManagerEmail)
        {
            var messages = new List<Messages>();
            using (var connection = new SqlConnection(_connString))
            {
                string UserName = HttpContext.Current.User.Identity.Name;
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [ManagerEmail], [LimitHardOrSoft], [LimitType], [LimitCurrencyCode], [LimitValue],[LimitOverride], [CreateDateTime], [ModifiedDateTime] FROM [dbo].[ems.ust_TradingLimits] where [ManagerEmail]='" + ManagerEmail + "'", connection))
                {
                    command.Notification = null;


                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messages.Add(item: new Messages { ManagerEmail = (string)reader["ManagerEmail"], LimitHardOrSoft = (string)reader["LimitHardOrSoft"], LimitType = (string)(reader["LimitType"]), LimitCurrencyCode = (string)reader["LimitCurrencyCode"], LimitValue = (string)reader["LimitValue"], LimitOverride = (Boolean)(reader["LimitOverride"]), CreateDateTime = (DateTime)reader["CreateDateTime"], ModifiedDateTime = (DateTime)(reader["ModifiedDateTime"]) });
                    }
                }

            }

            return messages;
        }

        public IEnumerable<Messages> GetFilteredMessages(string ManagerEmail, string LimitHardOrSoft, string LimitType, string LimitCurrencyCode)
        {
            var messages = new List<Messages>();
            StringBuilder queryBuilder = new StringBuilder();
            if (!string.IsNullOrEmpty(ManagerEmail)) queryBuilder.Append(" AND [ManagerEmail]=N'" + ManagerEmail + "'");
            if (!string.IsNullOrEmpty(LimitHardOrSoft)) queryBuilder.Append(" AND [LimitHardOrSoft]=N'" + LimitHardOrSoft + "'");
            if (!string.IsNullOrEmpty(LimitType)) queryBuilder.Append(" AND [LimitType]=N'" + LimitType + "'");
            if (!string.IsNullOrEmpty(LimitCurrencyCode)) queryBuilder.Append(" AND [LimitCurrencyCode]=N'" + LimitCurrencyCode + "'");

            string queryStr = queryBuilder.ToString();
            if (!string.IsNullOrEmpty(queryStr) && queryStr.IndexOf("AND", 0, 4) >= 0) queryStr = queryStr.Substring(4);

            using (var connection = new SqlConnection(_connString))
            {
                string UserName = HttpContext.Current.User.Identity.Name;
                connection.Open();
                using (var command = new SqlCommand(@"SELECT [ManagerEmail], [LimitHardOrSoft], [LimitType], [LimitCurrencyCode], [LimitValue],[LimitOverride], [CreateDateTime], [ModifiedDateTime] FROM [dbo].[ems.ust_TradingLimits] " + (!string.IsNullOrEmpty(queryStr) ? " WHERE " + queryStr : ""), connection))
                {
                    command.Notification = null;
                    bool startResult = SqlDependency.Start(_connString);

                    var dependency = new SqlDependency(command);
                    dependency.OnChange += new OnChangeEventHandler(dependency_OnChange);

                    if (connection.State == ConnectionState.Closed)
                        connection.Open();

                    var reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        messages.Add(item: new Messages { ManagerEmail = (string)reader["ManagerEmail"], LimitHardOrSoft = (string)reader["LimitHardOrSoft"], LimitType = (string)(reader["LimitType"]), LimitCurrencyCode = (string)reader["LimitCurrencyCode"], LimitValue = (string)reader["LimitValue"], LimitOverride = (Boolean)(reader["LimitOverride"]), CreateDateTime = (DateTime)reader["CreateDateTime"], ModifiedDateTime = (DateTime)(reader["ModifiedDateTime"]) });
                    }
                }

            }

            return messages;
        }
    }
}