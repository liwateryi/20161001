using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneBlood.Model;
using DataHelp;
using System.Data.SqlClient;
using System.Data;

namespace GeneBlood.DAL
{
    /// <summary>
    /// T_Roles 数据访问类
    /// </summary>
    public class T_RolesDAL:DALBase
    {
       /// <summary>
       /// 数据库连接字符串
       /// </summary>
       public T_RolesDAL(string connectionString)
       {
            base.sqlHelp.ConnectionString = connectionString;
       }
    }
}
