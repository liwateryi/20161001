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
    /// T_Roles �߼�������
    /// </summary>
    public class T_RolesBLL
    {
        T_RolesDAL dal = new T_RolesDAL(ConnectionString.ConnectionStrings);

        /// <summary>
        /// ��ɫȨ�޹���������
        /// </summary>
        T_RolesRightDAL rolesRightDal = new T_RolesRightDAL(ConnectionString.ConnectionStrings);

        /// <summary>
        /// ���� T_Roles ������
        /// </summary>
        /// <param name=info>T_Roles ʵ�����</param>
        public bool Insert(T_Roles info)
        {
            return dal.Insert(info);
        }

        /// <summary>
        /// ���������޸� T_Roles ������
        /// </summary>
        /// <param name=info>T_Roles ʵ�����</param>
        public bool Update(T_Roles info)
        {
            return dal.Update(info);
        }


        /// <summary>
        /// ��������ɾ�� T_Roles ������
        /// </summary>
        /// <param name=id>����</param>
        public bool Delete(System.Int64 id)
        {
            T_Roles info = new T_Roles();
            info.RoleId = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// ����������ѯһ��ʵ��
        /// </summary>
        /// <param name=id>����</param>
        public T_Roles SelectForID(System.Int64 id)
        {
            T_Roles info = new T_Roles();
            info.RoleId = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// ��ȡ T_Roles ����������
        /// </summary>
        public List<T_Roles> SelectAll()
        {
            return dal.SelectAll<T_Roles>();
        }


        /// <summary>
        /// ��ɫȨ�޷���
        /// </summary>
        /// <param name="roles">��ǰ�����Ľ�ɫ</param>
        /// <param name="RightsIds">Ȩ��id����</param>
        /// <returns></returns>
        public T_Roles AddRolesForRights(T_Roles roles, List<long> RightsIds)
        {
            //using (TransactionScope scope = new TransactionScope())
            //{
            if (roles.RoleId > 0)//�޸����н�ɫȨ��
            {
                //�޸Ľ�ɫ��
                dal.Update(roles);

                //���ݽ�ɫidɾ����ɫȨ�޹��������
                rolesRightDal.DeleteForRolesId(roles.RoleId);

                //���ݽ�ɫid�������ɽ�ɫȨ�޹���
                //�����ɫ����
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
            else//������ɫֱ�ӷ���Ȩ�޹���
            {
                //������ɫ����
                int newRolesId = dal.InsertToID(roles);

                roles.RoleId = newRolesId;

                //�����ɫ����
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
