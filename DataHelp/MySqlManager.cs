using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;
using System.Data;

namespace DataHelp
{
    public class MySqlManager:List<MySqlParameter>
    {
        public MySqlManager()
        { }

        public MySqlManager(string sql)
        { this.sql = sql; }

        public MySqlManager(string sql, CommandType type)
        {
            this.sql = sql;
            this.type = type;
        }

        string sql;
        public string Sql
        {
            get { return sql; }
            set { sql = value; }
        }

        CommandType type= CommandType.Text;
        public CommandType Type
        {
            get { return type; }
            set { type = value; }
        }

        int commandTimeout=0;
        /// <summary>
        /// 数据库连接超时时间(单位:秒)
        /// </summary>
        public int CommandTimeout
        {
            get { return commandTimeout; }
            set { commandTimeout = value; }
        }

        public void Add(string name, object value)
        {
            MySqlParameter p = new MySqlParameter();
            p.ParameterName = name;
            p.Value = value;
            this.Add(p);
        }

        public void AddOutput(string name, MySqlDbType dbType)
        {
            MySqlParameter p = new MySqlParameter();
            p.ParameterName = name;
            p.MySqlDbType = dbType;
            p.Direction = ParameterDirection.Output;
            this.Add(p);
        }

        public void AddOutput(string name, MySqlDbType dbType, int size)
        {
            MySqlParameter p = new MySqlParameter();
            p.ParameterName = name;
            p.MySqlDbType = dbType;
            p.Direction = ParameterDirection.Output;
            p.Size = size;
            this.Add(p);
        }
    }
}
