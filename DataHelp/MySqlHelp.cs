using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataHelp
{
    public class MySqlHelp
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
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                try
                {
                    MySqlCommand com = new MySqlCommand(sql, con);
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
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                try
                {
                    MySqlCommand com = new MySqlCommand(sql, con);
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

        public MySqlDataReader ExecuteReader(string sql)
        {
            MySqlConnection con = new MySqlConnection(ConnectionString);

            MySqlCommand com = new MySqlCommand(sql, con);
            con.Open();
            return com.ExecuteReader(CommandBehavior.CloseConnection);

        }

        public DataTable ExecuteFill(string sql)
        {
            MySqlConnection con = new MySqlConnection(ConnectionString);
            try
            {
                MySqlDataAdapter da = new MySqlDataAdapter(sql, con);
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


        public int ExecuteNonQuery(MySqlManager manager)
        {
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                try
                {
                    MySqlCommand com = new MySqlCommand(manager.Sql, con);
                    com.CommandType = manager.Type;
                    if (manager.CommandTimeout != -1)
                        com.CommandTimeout = manager.CommandTimeout;
                    foreach (MySqlParameter item in manager)
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

        public object ExecuteScalar(MySqlManager manager)
        {
            using (MySqlConnection con = new MySqlConnection(ConnectionString))
            {
                try
                {
                    MySqlCommand com = new MySqlCommand(manager.Sql, con);
                    com.CommandType = manager.Type;
                    if (manager.CommandTimeout != -1)
                        com.CommandTimeout = manager.CommandTimeout;
                    foreach (MySqlParameter item in manager)
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
        public MySqlDataReader ExecuteReader(MySqlManager manager)
        {
            MySqlConnection con = new MySqlConnection(ConnectionString);
            MySqlCommand com = new MySqlCommand(manager.Sql, con);
            com.CommandType = manager.Type;
            if (manager.CommandTimeout != -1)
                com.CommandTimeout = manager.CommandTimeout;
            foreach (MySqlParameter item in manager)
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
        public DataTable ExecuteFill(MySqlManager manager)
        {
            MySqlConnection con = new MySqlConnection(ConnectionString);
            try
            {
                MySqlCommand com = new MySqlCommand(manager.Sql, con);
                if (manager.CommandTimeout != -1)
                    com.CommandTimeout = manager.CommandTimeout;
                com.CommandType = manager.Type;
                foreach (MySqlParameter item in manager)
                {
                    com.Parameters.Add(item);
                }
                MySqlDataAdapter da = new MySqlDataAdapter();
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
