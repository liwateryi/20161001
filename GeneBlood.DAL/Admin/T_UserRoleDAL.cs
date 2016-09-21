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
    /// T_UserRole ���ݷ�����
    /// </summary>
    public class T_UserRoleDAL:DALBase
    {
        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        public T_UserRoleDAL(string connectionString)
        {
            base.sqlHelp.ConnectionString = connectionString;
        }

        /// <summary>
        /// ɾ���û���ɫ������
        /// </summary>
        /// <param name="userId">�û�id</param>
        /// <returns></returns>
        public bool DeleteUserRoleForUserId(long userId)
        {
            string sql = "delete from t_userrole where UserId=@UserId";
            SqlManager manager = new SqlManager(sql);
            manager.Add("@UserId", userId);
            return sqlHelp.ExecuteNonQuery(manager) > 0;
        }
    }
}
