using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace WpfZhihui
{
    class SQLHelper
    {
        public SQLHelper()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static int ExecuteNonQuery(string sql, params SqlParameter[] parameters)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static object ExecuteScalar(string sql, params SqlParameter[] parameters)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    return cmd.ExecuteScalar();
                }
            }
        }//object i = SQLHelper.ExecuteScalar("select count(*) from Port_Emp");

        public static DataTable ExecuteDataTable(string sql, params SqlParameter[] parameters)
        {
            string connStr = ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString;
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = sql;
                    foreach (SqlParameter parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                    DataSet m_dataset = new DataSet();
                    SqlDataAdapter m_adapter = new SqlDataAdapter(cmd);
                    m_adapter.Fill(m_dataset);
                    return m_dataset.Tables[0];
                }
            }
        }
        public static void getPointsByCategory(int Category, out DataTable dt)
        {
            dt = SQLHelper.ExecuteDataTable("select ilng,ilat,Name,Description from T_Mappoint where T_Mappoint.icategory=@Category", new SqlParameter("Category", Category));
        }
    }
}
