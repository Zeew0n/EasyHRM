using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeeHRM.ApplicationService.ConnectDb
{
    class DbConnectHelper
    {
        private static string _getConnectionString = ConfigurationManager.AppSettings["ConnectionString"];

        public static SqlConnection GetConnection()
        {
            var con = new SqlConnection(_getConnectionString);
            return con;
        }


        private static string _getOleDbConnection = ConfigurationManager.AppSettings["OleDbConnection"];

        public static OleDbConnection GetOleDbConnection()
        {
            var oleconn = new OleDbConnection(_getOleDbConnection);
            return oleconn;

        }
    }
}
