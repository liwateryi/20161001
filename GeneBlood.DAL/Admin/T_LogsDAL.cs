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
    /// T_Logs ���ݷ�����
    /// </summary>
    public class T_LogsDAL:DALBase
    {
       /// <summary>
       /// ���ݿ������ַ���
       /// </summary>
       public T_LogsDAL(string connectionString)
       {
            base.sqlHelp.ConnectionString = connectionString;
       }
    }
}
