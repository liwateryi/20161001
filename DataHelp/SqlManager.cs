using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace DataHelp
{
    public class SqlManager:List<SqlParameter>
    {
        public SqlManager()
        { }

        public SqlManager(string sql)
        { this.sql = sql; }

        public SqlManager(string sql, CommandType type)
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
            SqlParameter p = new SqlParameter();
            p.ParameterName = name;
            p.SqlValue = value;
            this.Add(p);
        }

        public void Add(string name, object value, string likeValue)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = name;
            p.SqlValue = value;
            p.Value = likeValue;
            this.Add(p);
        }

        public void AddOutput(string name, SqlDbType dbType)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = name;
            p.SqlDbType = dbType;
            p.Direction = ParameterDirection.Output;
            this.Add(p);
        }

        public void AddOutput(string name, SqlDbType dbType, int size)
        {
            SqlParameter p = new SqlParameter();
            p.ParameterName = name;
            p.SqlDbType = dbType;
            p.Direction = ParameterDirection.Output;
            p.Size = size;
            this.Add(p);
        }
    }
}
