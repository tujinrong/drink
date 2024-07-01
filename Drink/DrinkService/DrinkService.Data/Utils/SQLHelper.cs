using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace DrinkService.Data
{
    public class SQLHelper
    {
        public static string connstr = ConfigurationManager.ConnectionStrings["DySMAT"].ConnectionString;

        public static DataSet GetDataSet(string sqlstr)
        {
            DataSet ds = new DataSet();
            using (var conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlDataAdapter ada = new SqlDataAdapter(sqlstr, conn);
                ada.Fill(ds);
                return ds;
            }
        }
        public static DataSet GetDataSet(string sqlstr, string tableName)
        {
            using (var conn = new SqlConnection(connstr))
            {
                conn.Open();
                SqlDataAdapter ada = new SqlDataAdapter(sqlstr, conn);
                DataSet ds = new DataSet();
                ada.Fill(ds, tableName);
                return ds;
            }
        }
        public static DataTable GetDataTable(string cmdText)
        {

            SqlDataReader reader;
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(connstr))
            {
                conn.Open();
                var cmd = new SqlCommand(cmdText, conn);
                reader = cmd.ExecuteReader();
                dt.Load(reader);
                reader.Close();
                return dt;
            }
        }
        public static DataTable GetDataTable(string cmdText, SqlConnection conn)
        {
            DataTable dt = new DataTable();
            var cmd = new SqlCommand(cmdText, conn);
            using (var reader = cmd.ExecuteReader())
            {
                dt.Load(reader);
            }
            return dt;
        }

        public static DataTable GetDataTable(string cmdText, CommandType cmdType, SqlParameter[] cmdParms)
        {
            SqlDataReader reader;
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(connstr))
            {
                conn.Open();
                var cmd = new SqlCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms, conn);
                reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                dt.Load(reader);
                return dt;
            }
        }
        public static int ExecuteNonQuery(string cmdText)
        {
            using (var conn = new SqlConnection(connstr))
            {
                conn.Open();
                var cmd = new SqlCommand(cmdText, conn);
                return cmd.ExecuteNonQuery();
            }
        }
        public static int ExecuteNonQuery(string cmdText, SqlConnection conn)
        {
            var cmd = new SqlCommand(cmdText, conn);
            return cmd.ExecuteNonQuery();
        }

        public static int ExecuteNonQuery(string cmdText, CommandType cmdType, SqlParameter[] cmdParms)
        {
            using (var conn = new SqlConnection(connstr))
            {
                conn.Open();
                var cmd = new SqlCommand();
                SetCommand(cmd, cmdText, cmdType, cmdParms, conn);
                var count = cmd.ExecuteNonQuery();
                cmd.Parameters.Clear();
                conn.Close();
                return count;
            }
        }
        private static void SetCommand(SqlCommand cmd, string cmdText, CommandType cmdType, SqlParameter[] cmdParms, SqlConnection conn)
        {
            cmd.Connection = conn;
            cmd.CommandText = cmdText;
            cmd.CommandType = cmdType;
            if (cmdParms != null)
            {
                cmd.Parameters.AddRange(cmdParms);
            }
        }

    }
}
        