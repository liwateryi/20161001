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
    /// T_RolesRight 逻辑处理类
    /// </summary>
    public class T_RolesRightBLL
    {
        T_RolesRightDAL dal = new T_RolesRightDAL(ConnectionString.ConnectionStrings);

        /// <summary>
        /// 新增 T_RolesRight 表数据
        /// </summary>
        /// <param name=info>T_RolesRight 实体对象</param>
        public bool Insert(T_RolesRight info)
        {
            return dal.Insert(info);
        }

        /// <summary>
        /// 根据主键修改 T_RolesRight 表数据
        /// </summary>
        /// <param name=info>T_RolesRight 实体对象</param>
        public bool Update(T_RolesRight info)
        {
            return dal.Update(info);
        }


        /// <summary>
        /// 根据主键删除 T_RolesRight 表数据
        /// </summary>
        /// <param name=id>主键</param>
        public bool Delete(System.Int64 id)
        {
            T_RolesRight info = new T_RolesRight();
            info.RRId = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// 根据主键查询一个实体
        /// </summary>
        /// <param name=id>主键</param>
        public T_RolesRight SelectForID(System.Int64 id)
        {
            T_RolesRight info = new T_RolesRight();
            info.RRId = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// 获取 T_RolesRight 表所有数据
        /// </summary>
        public List<T_RolesRight> SelectAll()
        {
            return dal.SelectAll<T_RolesRight>();
        }
    }
}
