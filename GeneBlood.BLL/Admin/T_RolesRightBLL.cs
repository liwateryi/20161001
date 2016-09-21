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
    /// T_RolesRight �߼�������
    /// </summary>
    public class T_RolesRightBLL
    {
        T_RolesRightDAL dal = new T_RolesRightDAL(ConnectionString.ConnectionStrings);

        /// <summary>
        /// ���� T_RolesRight ������
        /// </summary>
        /// <param name=info>T_RolesRight ʵ�����</param>
        public bool Insert(T_RolesRight info)
        {
            return dal.Insert(info);
        }

        /// <summary>
        /// ���������޸� T_RolesRight ������
        /// </summary>
        /// <param name=info>T_RolesRight ʵ�����</param>
        public bool Update(T_RolesRight info)
        {
            return dal.Update(info);
        }


        /// <summary>
        /// ��������ɾ�� T_RolesRight ������
        /// </summary>
        /// <param name=id>����</param>
        public bool Delete(System.Int64 id)
        {
            T_RolesRight info = new T_RolesRight();
            info.RRId = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// ����������ѯһ��ʵ��
        /// </summary>
        /// <param name=id>����</param>
        public T_RolesRight SelectForID(System.Int64 id)
        {
            T_RolesRight info = new T_RolesRight();
            info.RRId = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// ��ȡ T_RolesRight ����������
        /// </summary>
        public List<T_RolesRight> SelectAll()
        {
            return dal.SelectAll<T_RolesRight>();
        }
    }
}
