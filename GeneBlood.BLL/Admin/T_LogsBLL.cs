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
    /// T_Logs 逻辑处理类
    /// </summary>
    public class T_LogsBLL
    {
        T_LogsDAL dal = new T_LogsDAL(ConnectionString.ConnectionStrings);

        /// <summary>
        /// 新增 T_Logs 表数据
        /// </summary>
        /// <param name=info>T_Logs 实体对象</param>
        public bool Insert(T_Logs info)
        {
            return dal.Insert(info);
        }

        /// <summary>
        /// 根据主键修改 T_Logs 表数据
        /// </summary>
        /// <param name=info>T_Logs 实体对象</param>
        public bool Update(T_Logs info)
        {
            return dal.Update(info);
        }


        /// <summary>
        /// 根据主键删除 T_Logs 表数据
        /// </summary>
        /// <param name=id>主键</param>
        public bool Delete(System.Int64 id)
        {
            T_Logs info = new T_Logs();
            info.Logid = id;
            return dal.Delete(info);
        }

        /// <summary>
        /// 根据主键查询一个实体
        /// </summary>
        /// <param name=id>主键</param>
        public T_Logs SelectForID(System.Int64 id)
        {
            T_Logs info = new T_Logs();
            info.Logid = id;
            dal.SelectForID(info);
            return info;
        }

        /// <summary>
        /// 获取 T_Logs 表所有数据
        /// </summary>
        public List<T_Logs> SelectAll()
        {
            return dal.SelectAll<T_Logs>();
        }

        /// <summary>
        /// 查询日志分页
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
