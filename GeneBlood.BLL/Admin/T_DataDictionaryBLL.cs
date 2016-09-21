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
    /// T_DataDictionary �߼�������
    /// </summary>
    public class T_DataDictionaryBLL
    {
        T_DataDictionaryDAL dal = new T_DataDictionaryDAL(ConnectionString.ConnectionStrings);
        
        /// <summary>
        /// ���� T_DataDictionary ������
        /// </summary>
        /// <param name=info>T_DataDictionary ʵ�����</param>
        public bool Insert(T_DataDictionary info)
        {
            return  dal.Insert(info);
        }
        
        /// <summary>
        /// ���������޸� T_DataDictionary ������
        /// </summary>
        /// <param name=info>T_DataDictionary ʵ�����</param>
        public bool Update(T_DataDictionary info)
        {
            return dal.Update(info);
        }
        
                
        /// <summary>
        /// ��������ɾ�� T_DataDictionary ������
        /// </summary>
        /// <param name=id>����</param>
        public bool Delete(System.Int64 id)
        { 
            T_DataDictionary info = new T_DataDictionary();
            info.DId = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// ����������ѯһ��ʵ��
        /// </summary>
        /// <param name=id>����</param>
        public T_DataDictionary SelectForID(System.Int64 id)
        {
            T_DataDictionary info = new T_DataDictionary();
            info.DId = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// ��ȡ T_DataDictionary ����������
        /// </summary>
        public List<T_DataDictionary> SelectAll()
        {
            return dal.SelectAll<T_DataDictionary>();
        }
        /// <summary>
        /// ��ѯ���ݷ�ҳ
        /// </summary>
        /// <param name=page></param>
        /// <returns></returns>
        public List<T_DataDictionary> SelectForPage(PageClass page)
        {
            page.ConnectionString = dal.sqlHelp.ConnectionString;
            return page.GetPageData<T_DataDictionary>();
       }
   }
}
