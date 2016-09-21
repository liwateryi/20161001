using GeneBlood.BLL;
using GeneBlood.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeneBlood.Web.Admin.Common
{
    public class LogHelper
    {
        public static T_LogsBLL logBLL = new T_LogsBLL();

        /// <summary>
        /// 记录用户操作日志
        /// </summary>
        /// <param name="log"></param>
        public static void WriteOperationLog(T_Logs log)
        {
            try
            {
                logBLL.Insert(log);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// 记录用户操作日志
        /// </summary>
        /// <param name="log"></param>
        public static void WriteOperationLog(string title, string logContext, string userName, string ip)
        {
            try
            {
                T_Logs log = new T_Logs
                {
                    LogName = title,
                    LogContext = logContext,
                    UserName = userName,
                    Ip = ip,
                    LogTime = DateTime.Now,
                    LogType = "用户操作日志"
                };
                logBLL.Insert(log);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 获取日志对象
        /// </summary>
        /// <returns></returns>
        public static T_Logs GetLog()
        {
            T_Logs log = new T_Logs();
            log.Ip = Static.GetIPadress();
            log.UserName = Static.GetUserForCookie() == null ? "" : Static.GetUserForCookie().LoginName;
            log.LogTime = DateTime.Now;
            return log;
        }
    }
}
