using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.ConnectDb;
using BeeHRM.ApplicationService.Common;
namespace BeeHRM.ApplicationService.Common
{
    public class CommonMethods
    {
        /// <summary>
        /// private static SqlConnection
        /// </summary>
        private static SqlConnection conn = new SqlConnection();

        /// <summary>
        /// private static history SqlConnection
        /// </summary>
        private static SqlConnection historyConn = new SqlConnection();


        /// <summary>
        /// Gets DataTable of given query
        /// </summary>
        /// <param name="sqlQuery">query to be executed</param>
        /// <returns>returns datatable</returns>
        public static DataTable GetDataTable(string sqlQuery)
        {
            return GetDataset(sqlQuery).Tables["table"];
        }

        /// <summary>
        /// Gets DataSet of given query
        /// </summary>
        /// <param name="sqlQuery">query to be executed</param>
        /// <returns>returns DataSet</returns>
        public static DataSet GetDataset(string sqlQuery)
        {
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, GetConnection());
            da.SelectCommand.CommandTimeout = 5000;
            DataSet ds = new DataSet();
            da.Fill(ds, "table");
            da.Dispose();
            return ds;
        }

        /// <summary>
        /// ExecuteScalar method
        /// </summary>
        /// <param name="sqlQuery">query to be executed</param>
        /// <returns>returns string</returns>
        public static string ExecuteScalar(string sqlQuery)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sqlQuery, GetConnection());
                cmd.CommandTimeout = 5000;
                string value = cmd.ExecuteScalar().ToString();
                cmd.Dispose();
                return value;
            }
            catch
            {
                return "0";
            }
        }

        /// <summary>
        /// Executes given sql query
        /// </summary>
        /// <param name="sql">query to be executed</param>
        /// <returns>returns int</returns>
        public static int ExecuteSql(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, GetConnection());
            cmd.CommandTimeout = 800;
            int rowsAffected = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return rowsAffected;
        }

        /// <summary>
        /// Gets App setting string
        /// </summary>
        /// <param name="key">Key name</param>
        /// <returns>returns string</returns>
        private static string GetAppSetting(string key)
        {
            return System.Configuration.ConfigurationManager.AppSettings[key].ToString();
        }

        /// <summary>
        /// Method that returns sql connection
        /// </summary>
        /// <returns>returns sql connection</returns>
        public static SqlConnection GetConnection()
        {

            conn.Close();
            conn.Dispose();
            string connectionstring = ConfigurationManager.ConnectionStrings["SimimNBR"].ToString();
            // conn.ConnectionString = connectionstring.Substring(connectionstring.IndexOf("connection string=", StringComparison.CurrentCulture)).Replace("connection string=", string.Empty).Replace("\"", string.Empty); //// System.Configuration.ConfigurationManager.ConnectionStrings["SIMIM"].ConnectionString;

            conn.Open();
            return conn;
        }

        /// <summary>
        /// MEthod tha returns connections string
        /// </summary>
        /// <param name="key">key name</param>
        /// <returns>returns connections string</returns>
        public static string GetConnectionString(string key)
        {
            return System.Configuration.ConfigurationManager.ConnectionStrings[key].ToString();
        }


        /// <summary>
        /// Used by Repayment page
        /// </summary>
        /// <returns>returns connection string</returns>
        public static string GetConnectionString()
        {


            conn.Close();
            conn.Dispose();
            ////conn.ConnectionString = ConfigurationManager.ConnectionStrings["SimimNBRConnectionString"].ConnectionString;
            conn.ConnectionString = GetConnectionString("SimimNBR");

            return conn.ConnectionString;
        }


    }
}
