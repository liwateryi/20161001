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
    /// T_Rights �߼�������
    /// </summary>
    public class T_RightsBLL
    {
        T_RightsDAL dal = new T_RightsDAL(ConnectionString.ConnectionStrings);
        T_RolesRightDAL rolesRightDal = new T_RolesRightDAL(ConnectionString.ConnectionStrings);
        /// <summary>
        /// ���� T_Rights ������
        /// </summary>
        /// <param name=info>T_Rights ʵ�����</param>
        public bool Insert(T_Rights info)
        {
            return dal.Insert(info);
        }

        /// <summary>
        /// ���������޸� T_Rights ������
        /// </summary>
        /// <param name=info>T_Rights ʵ�����</param>
        public bool Update(T_Rights info)
        {
            return dal.Update(info);
        }


        /// <summary>
        /// ��������ɾ�� T_Rights ������
        /// </summary>
        /// <param name=id>����</param>
        public bool Delete(System.Int64 id)
        {
            T_Rights info = new T_Rights();
            info.Rid = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// ����������ѯһ��ʵ��
        /// </summary>
        /// <param name=id>����</param>
        public T_Rights SelectForID(System.Int64 id)
        {
            T_Rights info = new T_Rights();
            info.Rid = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// ��ȡ T_Rights ����������
        /// </summary>
        public List<T_Rights> SelectAll()
        {
            return dal.SelectAll<T_Rights>();
        }

        public T_Rights AddRightToRoles(T_Rights right, List<long> rolesIds)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            if (right.Rid > 0)//�޸�Ȩ�ޱ�
            {
                if (dal.Update(right))//�޸�Ȩ�ޱ�
                {
                    //����Ȩ��idɾ��Ȩ�޽�ɫ�����
                    rolesRightDal.DeleteForRightId(right.Rid);

                    //����Ȩ��id��������Ȩ�޽�ɫ����
                    if (rolesIds != null && rolesIds.Count > 0)
                    {

                        //��Ӹ��ڵ�
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
            else//����Ȩ�ޱ�
            {
                //����Ȩ��
                long newRightId = dal.InsertToID(right);
                right.Rid = newRightId;

                //��Ӹ��ڵ�
                rolesRightDal.Insert(new T_RolesRight
                {
                    RightId = right.ParentId,
                    RolesId = newRightId
                });

                //����Ȩ��id��������Ȩ�޽�ɫ����
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
