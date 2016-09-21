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
    /// T_UserRole 数据访问类
    /// </summary>
    public class T_UserRoleDAL:DALBase
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public T_UserRoleDAL(string connectionString)
        {
            base.sqlHelp.ConnectionString = connectionString;
        }

        /// <summary>
        /// 删除用户角色关联表
        /// </summary>
        /// <param name="userId">用户id</param>
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
