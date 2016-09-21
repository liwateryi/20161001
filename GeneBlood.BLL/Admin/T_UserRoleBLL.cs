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
    /// T_UserRole �߼�������
    /// </summary>
    public class T_UserRoleBLL
    {
        T_UserRoleDAL dal = new T_UserRoleDAL(ConnectionString.ConnectionStrings);

        /// <summary>
        /// ���� T_UserRole ������
        /// </summary>
        /// <param name=info>T_UserRole ʵ�����</param>
        public bool Insert(T_UserRole info)
        {
            return dal.Insert(info);
        }

        /// <summary>
        /// ���������޸� T_UserRole ������
        /// </summary>
        /// <param name=info>T_UserRole ʵ�����</param>
        public bool Update(T_UserRole info)
        {
            return dal.Update(info);
        }


        /// <summary>
        /// ��������ɾ�� T_UserRole ������
        /// </summary>
        /// <param name=id>����</param>
        public bool Delete(System.Int64 id)
        {
            T_UserRole info = new T_UserRole();
            info.URId = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// ����������ѯһ��ʵ��
        /// </summary>
        /// <param name=id>����</param>
        public T_UserRole SelectForID(System.Int64 id)
        {
            T_UserRole info = new T_UserRole();
            info.URId = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// ��ȡ T_UserRole ����������
        /// </summary>
        public List<T_UserRole> SelectAll()
        {
            return dal.SelectAll<T_UserRole>();
        }
    }
}
