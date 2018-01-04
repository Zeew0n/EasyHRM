using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.Helper
{
    public class SqlHelper
    {
        /// <summary>
        /// property to establish connection
        /// </summary>
        private SqlConnection connection;

        /// <summary>
        /// default constructor, to initialize all member variables when an object of this class is created
        /// </summary>
        /// <param name="connection">Connection string</param>
        public SqlHelper(string connection)
        {
            ////this.ConnectionString = connection;
            this.ConnectionString = connection.Substring(connection.IndexOf("connection string=", StringComparison.CurrentCulture)).Replace("connection string=", string.Empty).Replace("\"", string.Empty); //// System.Configuration.ConfigurationManager.ConnectionStrings["SIMIM"].ConnectionString;
            this.Connection = new SqlConnection();
        }

        /// <summary>
        /// Established connection with database
        /// </summary>
        public SqlConnection Connection
        {
            get
            {
                if (this.connection.State.Equals(ConnectionState.Closed))
                {
                    this.connection.ConnectionString = this.ConnectionString;
                    this.connection.Open();
                }

                return this.connection;
            }

            set
            {
                this.connection = value;
            }
        }

        /// <summary>
        /// property to get connection string
        /// </summary>
        private string ConnectionString { get; set; }

        /// <summary>
        /// Executes commands for acessing and manipulating data from database
        /// </summary>
        /// <returns>returns sql command</returns>
        public SqlCommand GetCommand()
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = this.Connection;
            sqlCmd.CommandTimeout = 800;
            return sqlCmd;
        }

        /// <summary>
        /// Returns the SQL data reader object
        /// </summary>
        /// <param name="query"> query to be executed </param>
        /// <returns> returns typed data reader object </returns>
        public SqlDataReader GetReader(string query)
        {
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.Connection = this.Connection;
            sqlCmd.CommandTimeout = 800;
            sqlCmd.CommandText = query;
            return sqlCmd.ExecuteReader();
        }

        /// <summary>
        /// Returns the data of given query
        /// </summary>
        /// <param name="query">select query</param>
        /// <returns>returns datatable</returns>
        public DataTable Select(string query)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, this.Connection);
            da.SelectCommand.CommandTimeout = 800;
            da.Fill(dt);
            da.Dispose();
            return dt;
        }

        /// <summary>
        /// executes the given sql query
        /// </summary>
        /// <param name="sql">sql query</param>
        /// <returns>returns int</returns>
        public int ExecuteSql(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, this.Connection);
            cmd.CommandTimeout = 800;
            int rowsAffected = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return rowsAffected;
        }

        /// <summary>
        /// Exectues sql
        /// </summary>
        /// <param name="sql">sql query</param>
        /// <param name="connection">sql connection</param>
        /// <param name="tran">sql transalation</param>
        /// <returns>returns int</returns>
        public int ExecuteFormSql(string sql, SqlConnection connection, SqlTransaction tran)
        {
            SqlCommand cmd = new SqlCommand(sql, connection);
            cmd.CommandTimeout = 800;
            cmd.Transaction = tran;
            int rowsAffected = cmd.ExecuteNonQuery();
            cmd.Dispose();
            return rowsAffected;
        }

        /// <summary>
        /// Returns the data of given query
        /// </summary>
        /// <param name="query">select query</param>
        /// <param name="connection">sql connection</param>
        /// <param name="tran">sql transalation</param>
        /// <returns>returns datatable</returns>
        public DataTable SelectSqlForForm(string query, SqlConnection connection, SqlTransaction tran)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(query, connection);
            da.SelectCommand.Transaction = tran;
            da.SelectCommand.CommandTimeout = 800;
            da.Fill(dt);
            da.Dispose();
            return dt;
        }
        
        /// <summary>
        /// Executes query and returns single value
        /// </summary>
        /// <param name="sql">sql query</param>
        /// <returns>returns single string</returns>
        public string ExecuteScalar(string sql)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(sql, this.Connection);
                cmd.CommandTimeout = 800;
                string value = cmd.ExecuteScalar().ToString();
                cmd.Dispose();
                return value;
            }
            catch
            {
                return "0";
            }
        }
    }
}
