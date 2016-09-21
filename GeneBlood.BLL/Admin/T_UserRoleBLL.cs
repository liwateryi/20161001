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
    /// T_UserRole 逻辑处理类
    /// </summary>
    public class T_UserRoleBLL
    {
        T_UserRoleDAL dal = new T_UserRoleDAL(ConnectionString.ConnectionStrings);

        /// <summary>
        /// 新增 T_UserRole 表数据
        /// </summary>
        /// <param name=info>T_UserRole 实体对象</param>
        public bool Insert(T_UserRole info)
        {
            return dal.Insert(info);
        }

        /// <summary>
        /// 根据主键修改 T_UserRole 表数据
        /// </summary>
        /// <param name=info>T_UserRole 实体对象</param>
        public bool Update(T_UserRole info)
        {
            return dal.Update(info);
        }


        /// <summary>
        /// 根据主键删除 T_UserRole 表数据
        /// </summary>
        /// <param name=id>主键</param>
        public bool Delete(System.Int64 id)
        {
            T_UserRole info = new T_UserRole();
            info.URId = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// 根据主键查询一个实体
        /// </summary>
        /// <param name=id>主键</param>
        public T_UserRole SelectForID(System.Int64 id)
        {
            T_UserRole info = new T_UserRole();
            info.URId = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// 获取 T_UserRole 表所有数据
        /// </summary>
        public List<T_UserRole> SelectAll()
        {
            return dal.SelectAll<T_UserRole>();
        }
    }
}
