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
    /// T_Logs �߼�������
    /// </summary>
    public class T_LogsBLL
    {
        T_LogsDAL dal = new T_LogsDAL(ConnectionString.ConnectionStrings);

        /// <summary>
        /// ���� T_Logs ������
        /// </summary>
        /// <param name=info>T_Logs ʵ�����</param>
        public bool Insert(T_Logs info)
        {
            return dal.Insert(info);
        }

        /// <summary>
        /// ���������޸� T_Logs ������
        /// </summary>
        /// <param name=info>T_Logs ʵ�����</param>
        public bool Update(T_Logs info)
        {
            return dal.Update(info);
        }


        /// <summary>
        /// ��������ɾ�� T_Logs ������
        /// </summary>
        /// <param name=id>����</param>
        public bool Delete(System.Int64 id)
        {
            T_Logs info = new T_Logs();
            info.Logid = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// ����������ѯһ��ʵ��
        /// </summary>
        /// <param name=id>����</param>
        public T_Logs SelectForID(System.Int64 id)
        {
            T_Logs info = new T_Logs();
            info.Logid = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// ��ȡ T_Logs ����������
        /// </summary>
        public List<T_Logs> SelectAll()
        {
            return dal.SelectAll<T_Logs>();
        }

        /// <summary>
        /// ��ѯ��־��ҳ
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public List<T_Logs> SelectForPage(PageClass page)
        {
            page.ConnectionString = dal.sqlHelp.ConnectionString;
            return page.GetPageData<T_Logs>();
        }
    }
}
