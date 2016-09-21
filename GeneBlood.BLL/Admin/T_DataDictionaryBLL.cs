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
    /// T_DataDictionary 逻辑处理类
    /// </summary>
    public class T_DataDictionaryBLL
    {
        T_DataDictionaryDAL dal = new T_DataDictionaryDAL(ConnectionString.ConnectionStrings);
        
        /// <summary>
        /// 新增 T_DataDictionary 表数据
        /// </summary>
        /// <param name=info>T_DataDictionary 实体对象</param>
        public bool Insert(T_DataDictionary info)
        {
            return  dal.Insert(info);
        }
        
        /// <summary>
        /// 根据主键修改 T_DataDictionary 表数据
        /// </summary>
        /// <param name=info>T_DataDictionary 实体对象</param>
        public bool Update(T_DataDictionary info)
        {
            return dal.Update(info);
        }
        
                
        /// <summary>
        /// 根据主键删除 T_DataDictionary 表数据
        /// </summary>
        /// <param name=id>主键</param>
        public bool Delete(System.Int64 id)
        { 
            T_DataDictionary info = new T_DataDictionary();
            info.DId = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// 根据主键查询一个实体
        /// </summary>
        /// <param name=id>主键</param>
        public T_DataDictionary SelectForID(System.Int64 id)
        {
            T_DataDictionary info = new T_DataDictionary();
            info.DId = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// 获取 T_DataDictionary 表所有数据
        /// </summary>
        public List<T_DataDictionary> SelectAll()
        {
            return dal.SelectAll<T_DataDictionary>();
        }
        /// <summary>
        /// 查询数据分页
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
