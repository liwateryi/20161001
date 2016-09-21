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
    /// T_Roles 逻辑处理类
    /// </summary>
    public class T_RolesBLL
    {
        T_RolesDAL dal = new T_RolesDAL(ConnectionString.ConnectionStrings);

        /// <summary>
        /// 角色权限关联处理类
        /// </summary>
        T_RolesRightDAL rolesRightDal = new T_RolesRightDAL(ConnectionString.ConnectionStrings);

        /// <summary>
        /// 新增 T_Roles 表数据
        /// </summary>
        /// <param name=info>T_Roles 实体对象</param>
        public bool Insert(T_Roles info)
        {
            return dal.Insert(info);
        }

        /// <summary>
        /// 根据主键修改 T_Roles 表数据
        /// </summary>
        /// <param name=info>T_Roles 实体对象</param>
        public bool Update(T_Roles info)
        {
            return dal.Update(info);
        }


        /// <summary>
        /// 根据主键删除 T_Roles 表数据
        /// </summary>
        /// <param name=id>主键</param>
        public bool Delete(System.Int64 id)
        {
            T_Roles info = new T_Roles();
            info.RoleId = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// 根据主键查询一个实体
        /// </summary>
        /// <param name=id>主键</param>
        public T_Roles SelectForID(System.Int64 id)
        {
            T_Roles info = new T_Roles();
            info.RoleId = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// 获取 T_Roles 表所有数据
        /// </summary>
        public List<T_Roles> SelectAll()
        {
            return dal.SelectAll<T_Roles>();
        }


        /// <summary>
        /// 角色权限分配
        /// </summary>
        /// <param name="roles">当前操作的角色</param>
        /// <param name="RightsIds">权限id集合</param>
        /// <returns></returns>
        public T_Roles AddRolesForRights(T_Roles roles, List<long> RightsIds)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            if (roles.RoleId > 0)//修改已有角色权限
            {
                //修改角色表
                dal.Update(roles);

                //根据角色id删除角色权限管理表数据
                rolesRightDal.DeleteForRolesId(roles.RoleId);

                //根据角色id重新生成角色权限关联
                //分配角色关联
                if (RightsIds != null && RightsIds.Count > 0)
                {
                    foreach (var item in RightsIds)
                    {
                        rolesRightDal.Insert(new T_RolesRight
                        {
                            RightId = item,
                            RolesId = roles.RoleId
                        });
                    }
                }
            }
            else//新增角色直接分配权限关联
            {
                //新增角色数据
                int newRolesId = dal.InsertToID(roles);

                roles.RoleId = newRolesId;

                //分配角色关联
                if (RightsIds != null && RightsIds.Count > 0)
                {
                    foreach (var item in RightsIds)
                    {
                        rolesRightDal.Insert(new T_RolesRight
                        {
                            RightId = item,
                            RolesId = newRolesId
                        });
                    }
                }
            }
            //scope.Complete();
            //}
            return roles;
        }
    }
}
