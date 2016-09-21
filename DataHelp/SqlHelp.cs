using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataHelp
{
    //静态类：只能有静态成员
    public class SqlHelp
    {
        private string _ConnectionString;

        public string ConnectionString
        {
            get { return _ConnectionString; }
            set { _ConnectionString = value; }
        }

        #region 执行SQL文本命令（不能带参数）
        //执行增删改的SQL命令
        public int ExecuteNonQuery(string sql)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand com = new SqlCommand(sql, con);
                    con.Open();
                    return com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (con.State == ConnectionState.Open || con.State == ConnectionState.Broken)
                    {
                        con.Close();
                    }
                }
            }
        }

        public object ExecuteScalar(string sql)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand com = new SqlCommand(sql, con);
                    con.Open();
                    return com.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (con.State == ConnectionState.Open || con.State == ConnectionState.Broken)
                    {
                        con.Close();
                    }
                }
            }
        }

        public SqlDataReader ExecuteReader(string sql)
        {
            SqlConnection con = new SqlConnection(ConnectionString);

            SqlCommand com = new SqlCommand(sql, con);
            con.Open();
            return com.ExecuteReader(CommandBehavior.CloseConnection);

        }

        public DataTable ExecuteFill(string sql)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(sql, con);
                DataTable table = new DataTable();
                da.Fill(table);
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open || con.State == ConnectionState.Broken)
                {
                    con.Close();
                }
            }
        }
        #endregion


        public int ExecuteNonQuery(SqlManager manager)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand com = new SqlCommand(manager.Sql, con);
                    com.CommandType = manager.Type;
                    if (manager.CommandTimeout != -1)
                        com.CommandTimeout = manager.CommandTimeout;
                    foreach (SqlParameter item in manager)
                    {
                        com.Parameters.Add(item);
                    }
                    con.Open();
                    return com.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (con.State == ConnectionState.Open || con.State == ConnectionState.Broken)
                    {
                        con.Close();
                    }
                }
            }
        }

        public object ExecuteScalar(SqlManager manager)
        {
            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                try
                {
                    SqlCommand com = new SqlCommand(manager.Sql, con);
                    com.CommandType = manager.Type;
                    if (manager.CommandTimeout != -1)
                        com.CommandTimeout = manager.CommandTimeout;
                    foreach (SqlParameter item in manager)
                    {
                        com.Parameters.Add(item);
                    }
                    con.Open();
                    return com.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (con.State == ConnectionState.Open || con.State == ConnectionState.Broken)
                    {
                        con.Close();
                    }
                }
            }
        }

        /// <summary>
        /// SqlDataReader(类似游标) 查询数据
        /// </summary>
        /// <param name="manager">查询参数</param>
        /// <returns></returns>
        public SqlDataReader ExecuteReader(SqlManager manager)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            SqlCommand com = new SqlCommand(manager.Sql, con);
            com.CommandType = manager.Type;
            if (manager.CommandTimeout != -1)
                com.CommandTimeout = manager.CommandTimeout;
            foreach (SqlParameter item in manager)
            {
                com.Parameters.Add(item);
            }
            con.Open();
            return com.ExecuteReader(CommandBehavior.CloseConnection);
        }

        /// <summary>
        /// 数据填充方式查询数据
        /// </summary>
        /// <param name="manager">查询参数</param>
        /// <returns>返回DataTable数据集</returns>
        public DataTable ExecuteFill(SqlManager manager)
        {
            SqlConnection con = new SqlConnection(ConnectionString);
            try
            {
                SqlCommand com = new SqlCommand(manager.Sql, con);
                if (manager.CommandTimeout != -1)
                    com.CommandTimeout = manager.CommandTimeout;
                com.CommandType = manager.Type;
                foreach (SqlParameter item in manager)
                {
                    com.Parameters.Add(item);
                }
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = com;
                DataTable table = new DataTable();
                da.Fill(table);
                return table;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (con.State == ConnectionState.Open || con.State == ConnectionState.Broken)
                {
                    con.Close();
                }
            }

        }
    }
}
