using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SurveySystem.DAL
{
    public class DBHelper
    {
        private static string connString = ConfigurationManager.ConnectionStrings["SurveyDB"].ConnectionString;

        public static DataTable ExecuteQuery(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(sql, conn);
                if (parameters != null)
                {
                    adapter.SelectCommand.Parameters.AddRange(parameters);
                }
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                return dt;
            }
        }

        public static int ExecuteNonQuery(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                return cmd.ExecuteNonQuery();
            }
        }

        public static object ExecuteScalar(string sql, SqlParameter[] parameters = null)
        {
            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters);
                }
                return cmd.ExecuteScalar();
            }
        }
    }
}