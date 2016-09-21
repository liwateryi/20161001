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
    /// T_User 数据访问类
    /// </summary>
    public class T_UserDAL:DALBase
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public T_UserDAL(string connectionString)
        {
            base.sqlHelp.ConnectionString = connectionString;
        }


        /// <summary>
        /// 通过登录名获取用户信息(登录)
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public T_User GetUserForUserName(string loginName)
        {
            string sql = "select * from dbo.T_User where loginname=@loginname and (State is null or State=0) ";
            SqlManager manager = new SqlManager(sql);
            manager.Add("@loginname", loginName);
            var data = Select<T_User>(manager);
            if (data != null)
                return data.FirstOrDefault();
            else
                return null;
        }


        /// <summary>
        /// 根据条件查询指定条数用户数据
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<T_User> GetUserForWhere(T_User user, int topNum)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.AppendFormat("select top {0} * from dbo.T_User where 1=1 ", topNum);
            SqlManager manager = new SqlManager();
            if (!string.IsNullOrEmpty(user.LoginName))
            {
                strSql.Append(" and LoginName like @UserName+'%'");
                manager.Add("@UserName", user.LoginName, user.LoginName + "%");
            }

            if (user.UserType != -1)
            {
                strSql.Append(" and UserType=@UserType");
                manager.Add("@UserType", user.UserType);
            }
            strSql.Append(" order by CreateDate desc ");
            manager.Sql = strSql.ToString();
            return Select<T_User>(manager);
        }


        /// <summary>
        /// 获取登录用户权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<T_Rights> GetUserRights(T_User user)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select * from dbo.T_Rights where Rid in( ");
            strSql.Append("select RightId from dbo.t_rolesright where RolesId in( ");
            strSql.Append("select RoleId from dbo.t_userrole where UserId in(select Id from dbo.T_User where Id=@UserId)))order by OrderByNum desc;");
            SqlManager manager = new SqlManager(strSql.ToString());
            manager.Add("@UserId", user.Id);
            return Select<T_Rights>(manager);
        }


        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public T_User ExistsUserName(T_User user)
        {
            string sql = "select * from dbo.T_User  where loginName=@loginName";
            SqlManager manager = new SqlManager(sql);
            manager.Add("@loginName", user.LoginName);
            List<T_User> list = Select<T_User>(manager);
            if (list != null && list.Count > 0)
                return list.FirstOrDefault();
            else
                return null;
        }
    }
}
