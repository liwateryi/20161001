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
    /// T_Rights 逻辑处理类
    /// </summary>
    public class T_RightsBLL
    {
        T_RightsDAL dal = new T_RightsDAL(ConnectionString.ConnectionStrings);
        T_RolesRightDAL rolesRightDal = new T_RolesRightDAL(ConnectionString.ConnectionStrings);
        /// <summary>
        /// 新增 T_Rights 表数据
        /// </summary>
        /// <param name=info>T_Rights 实体对象</param>
        public bool Insert(T_Rights info)
        {
            return dal.Insert(info);
        }

        /// <summary>
        /// 根据主键修改 T_Rights 表数据
        /// </summary>
        /// <param name=info>T_Rights 实体对象</param>
        public bool Update(T_Rights info)
        {
            return dal.Update(info);
        }


        /// <summary>
        /// 根据主键删除 T_Rights 表数据
        /// </summary>
        /// <param name=id>主键</param>
        public bool Delete(System.Int64 id)
        {
            T_Rights info = new T_Rights();
            info.Rid = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// 根据主键查询一个实体
        /// </summary>
        /// <param name=id>主键</param>
        public T_Rights SelectForID(System.Int64 id)
        {
            T_Rights info = new T_Rights();
            info.Rid = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// 获取 T_Rights 表所有数据
        /// </summary>
        public List<T_Rights> SelectAll()
        {
            return dal.SelectAll<T_Rights>();
        }

        public T_Rights AddRightToRoles(T_Rights right, List<long> rolesIds)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            if (right.Rid > 0)//修改权限表
            {
                if (dal.Update(right))//修改权限表
                {
                    //根据权限id删除权限角色管理表
                    rolesRightDal.DeleteForRightId(right.Rid);

                    //根据权限id重新生成权限角色关联
                    if (rolesIds != null && rolesIds.Count > 0)
                    {

                        //添加父节点
                        rolesRightDal.Insert(new T_RolesRight
                        {
                            RightId = right.ParentId,
                            RolesId = right.ParentId
                        });

                        foreach (var item in rolesIds)
                        {
                            rolesRightDal.Insert(new T_RolesRight
                            {
                                RightId = right.Rid,
                                RolesId = item
                            });
                        }
                    }
                }
            }
            else//新增权限表
            {
                //新增权限
                long newRightId = dal.InsertToID(right);
                right.Rid = newRightId;

                //添加父节点
                rolesRightDal.Insert(new T_RolesRight
                {
                    RightId = right.ParentId,
                    RolesId = newRightId
                });

                //根据权限id重新生成权限角色关联
                if (rolesIds != null && rolesIds.Count > 0)
                {
                    foreach (var item in rolesIds)
                    {
                        rolesRightDal.Insert(new T_RolesRight
                        {
                            RightId = right.Rid,
                            RolesId = item
                        });
                    }
                }
            }
            //    scope.Complete();
            //}
            return right;
        }
    }
}
