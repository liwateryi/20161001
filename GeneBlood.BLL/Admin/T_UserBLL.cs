using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneBlood.Model;
using GeneBlood.DAL;
using DataHelp;

namespace GeneBlood.BLL
{
    /// <summary>
    /// T_User 逻辑处理类
    /// </summary>
    public class T_UserBLL
    {
        T_UserDAL dal = new T_UserDAL(ConnectionString.ConnectionStrings);
        T_UserRoleDAL userRoleDal = new T_UserRoleDAL(ConnectionString.ConnectionStrings);

        /// <summary>
        /// 新增 T_User 表数据
        /// </summary>
        /// <param name=info>T_User 实体对象</param>
        public bool Insert(T_User info)
        {
            return dal.Insert(info);
        }

        /// <summary>
        /// 根据主键修改 T_User 表数据
        /// </summary>
        /// <param name=info>T_User 实体对象</param>
        public bool Update(T_User info)
        {
            return dal.Update(info);
        }


        /// <summary>
        /// 根据主键删除 T_User 表数据
        /// </summary>
        /// <param name=id>主键</param>
        public bool Delete(System.Int64 id)
        {
            T_User info = new T_User();
            info.Id = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// 根据主键查询一个实体
        /// </summary>
        /// <param name=id>主键</param>
        public T_User SelectForID(System.Int64 id)
        {
            T_User info = new T_User();
            info.Id = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// 获取 T_User 表所有数据
        /// </summary>
        public List<T_User> SelectAll()
        {
            return dal.SelectAll<T_User>();
        }


        /// <summary>
        /// 通过登录名获取用户信息(登录)
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public T_User GetUserForUserName(string loginName)
        {
            return dal.GetUserForUserName(loginName);
        }


        /// <summary>
        /// 根据条件查询指定条数用户数据
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<T_User> GetUserForWhere(T_User user, int topNum)
        {
            return dal.GetUserForWhere(user, topNum);
        }


        /// <summary>
        /// 获取登录用户权限
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<T_Rights> GetUserRights(T_User user)
        {
            return dal.GetUserRights(user);
        }


        /// <summary>
        /// 检测用户名是否存在
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public T_User ExistsUserName(T_User user)
        {
            return dal.ExistsUserName(user);
        }

        /// <summary>
        /// 新增，修改用分配角色
        /// </summary>
        /// <param name="info">用户实体</param>
        /// <param name="roleIds">角色id集合</param>
        /// <returns></returns>
        public bool Edit(T_User info, List<long> roleIds)
        {
            if (info.Id == default(long))
            {
                info.Id = dal.InsertToID(info);
            }
            else
            {
                dal.Update(info);
            }
            //分配角色
            if (roleIds != null && roleIds.Count > 0)
            {
                //删除当前用户角色关联
                userRoleDal.DeleteUserRoleForUserId(info.Id);
                //重新生成当前用户角色关联
                foreach (var item in roleIds)
                {
                    userRoleDal.Insert(new T_UserRole
                    {
                        RoleId = item,
                        UserId = info.Id
                    });
                }
            }
            return true;
        }


        /// <summary>
        /// 查询数据分页
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<T_User> SelectForPage(PageClass page)
        {
            page.ConnectionString = dal.sqlHelp.ConnectionString;
            return page.GetPageData<T_User>();
        }

        /// <summary>
        /// 获取最大ID
        /// </summary>
        /// <returns></returns>
        public long GetMaxId()
        {
            return Convert.ToInt64(dal.GetMaxId(new T_User()));
        }
    }
}
