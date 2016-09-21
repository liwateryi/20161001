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
    /// T_User �߼�������
    /// </summary>
    public class T_UserBLL
    {
        T_UserDAL dal = new T_UserDAL(ConnectionString.ConnectionStrings);
        T_UserRoleDAL userRoleDal = new T_UserRoleDAL(ConnectionString.ConnectionStrings);

        /// <summary>
        /// ���� T_User ������
        /// </summary>
        /// <param name=info>T_User ʵ�����</param>
        public bool Insert(T_User info)
        {
            return dal.Insert(info);
        }

        /// <summary>
        /// ���������޸� T_User ������
        /// </summary>
        /// <param name=info>T_User ʵ�����</param>
        public bool Update(T_User info)
        {
            return dal.Update(info);
        }


        /// <summary>
        /// ��������ɾ�� T_User ������
        /// </summary>
        /// <param name=id>����</param>
        public bool Delete(System.Int64 id)
        {
            T_User info = new T_User();
            info.Id = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// ����������ѯһ��ʵ��
        /// </summary>
        /// <param name=id>����</param>
        public T_User SelectForID(System.Int64 id)
        {
            T_User info = new T_User();
            info.Id = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// ��ȡ T_User ����������
        /// </summary>
        public List<T_User> SelectAll()
        {
            return dal.SelectAll<T_User>();
        }


        /// <summary>
        /// ͨ����¼����ȡ�û���Ϣ(��¼)
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        public T_User GetUserForUserName(string loginName)
        {
            return dal.GetUserForUserName(loginName);
        }


        /// <summary>
        /// ����������ѯָ�������û�����
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<T_User> GetUserForWhere(T_User user, int topNum)
        {
            return dal.GetUserForWhere(user, topNum);
        }


        /// <summary>
        /// ��ȡ��¼�û�Ȩ��
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public List<T_Rights> GetUserRights(T_User user)
        {
            return dal.GetUserRights(user);
        }


        /// <summary>
        /// ����û����Ƿ����
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public T_User ExistsUserName(T_User user)
        {
            return dal.ExistsUserName(user);
        }

        /// <summary>
        /// �������޸��÷����ɫ
        /// </summary>
        /// <param name="info">�û�ʵ��</param>
        /// <param name="roleIds">��ɫid����</param>
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
            //�����ɫ
            if (roleIds != null && roleIds.Count > 0)
            {
                //ɾ����ǰ�û���ɫ����
                userRoleDal.DeleteUserRoleForUserId(info.Id);
                //�������ɵ�ǰ�û���ɫ����
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
        /// ��ѯ���ݷ�ҳ
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<T_User> SelectForPage(PageClass page)
        {
            page.ConnectionString = dal.sqlHelp.ConnectionString;
            return page.GetPageData<T_User>();
        }

        /// <summary>
        /// ��ȡ���ID
        /// </summary>
        /// <returns></returns>
        public long GetMaxId()
        {
            return Convert.ToInt64(dal.GetMaxId(new T_User()));
        }
    }
}
