using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Common
{
    public class SqlRepository
    {
        /// <summary>
        /// private static SqlConnection
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// SqlRepository constructor
        /// </summary>
        public SqlRepository()
        {


            this.connection = new SqlConnection();
            string connectionstring = ConfigurationManager.ConnectionStrings["SimimNBRConnectionString"].ToString();
            this.connection.ConnectionString = connectionstring.Substring(connectionstring.IndexOf("connection string=", StringComparison.CurrentCulture)).Replace("connection string=", string.Empty).Replace("\"", string.Empty); //// System.Configuration.ConfigurationManager.ConnectionStrings["SIMIM"].ConnectionString;

        }

        /// <summary>
        /// Get DataTable
        /// </summary>
        /// <param name="sqlQuery">Select query</param>
        /// <returns>Returns DataTable</returns>
        public DataTable GetDataTable(string sqlQuery)
        {
            return this.GetDataset(sqlQuery).Tables[0];
        }

        /// <summary>
        /// Get DataSet
        /// </summary>
        /// <param name="sqlQuery">Select query</param>
        /// <returns>Returns DataSet</returns>
        public DataSet GetDataset(string sqlQuery)
        {
            this.connection.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, this.connection);
            da.SelectCommand.CommandTimeout = 5000;
            DataSet ds = new DataSet();
            da.Fill(ds);
            da.Dispose();
            this.connection.Close();
            this.connection.Dispose();
            return ds;
        }

        /// <summary>
        /// Data Reader of select query
        /// </summary>
        /// <param name="sqlQuery">Select query</param>
        /// <returns>Returns SqlDataReader</returns>
        public SqlDataReader SqlDataReader(string sqlQuery)
        {
            this.connection.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery, this.connection);
            SqlDataReader reader = cmd.ExecuteReader();
            cmd.Dispose();
            this.connection.Close();
            this.connection.Dispose();
            return reader;
        }

        /// <summary>
        /// Execute Sql query
        /// </summary>
        /// <param name="sqlQuery">SQL query</param>
        /// <returns>Returns int as success or failed</returns>
        public int ExecuteSql(string sqlQuery)
        {
            this.connection.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery, this.connection);

            try
            {
                return cmd.ExecuteNonQuery();
            }
            catch
            {
                return -1;
            }
            finally
            {
                cmd.Dispose();
                this.connection.Close();
                this.connection.Dispose();
            }
        }

        /// <summary>
        /// Executes the given query in scalar method
        /// </summary>
        /// <param name="sqlQuery">SQL query</param>
        /// <returns>Returns 1st column as string showing the query executed or not</returns>
        public string ExecuteScalar(string sqlQuery)
        {
            this.connection.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery, this.connection);

            try
            {
                return cmd.ExecuteScalar().ToString();
            }
            catch
            {
                return "-1";
            }
            finally
            {
                cmd.Dispose();
                this.connection.Close();
                this.connection.Dispose();
            }
        }

        /// <summary>
        /// Data Reader that returns if it has rows or not.
        /// </summary>
        /// <param name="sqlQuery">Select query</param>
        /// <returns>Returns true or false</returns>
        public bool HasRows(string sqlQuery)
        {
            bool returnValue = false;
            SqlDataReader reader = this.SqlDataReader(sqlQuery);

            if (reader.HasRows)
            {
                returnValue = true;
            }
            else
            {
                returnValue = false;
            }

            reader.Dispose();
            return returnValue;
        }
    }
}
