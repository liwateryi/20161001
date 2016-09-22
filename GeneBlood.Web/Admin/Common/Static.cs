using GeneBlood.BLL;
using GeneBlood.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace GeneBlood.Web.Admin.Common
{
    /// <summary>
    /// 页面公用类
    /// </summary>
    public class Static
    {

        //新增用户默认密码
        public static string defaultPwd = "123456";

        #region 用户数据缓存处理

        //验证码
        private static string codeCookieName = "_checkcode";
        private static string codeCookieKey = "checkcode";

        //用户信息
        private static string userCookieName = "_userinfo";
        private static string userCookieKey = "userinfo";

        //权限信息
        private static string rightCookieName = "_userright";
        private static string rightCookieKey = "userright";

        //Cookie 写入指定域名下
        private static string doMain = System.Configuration.ConfigurationManager.AppSettings["DoMain"];

        /// <summary>
        /// 保存验证码到Cookie
        /// </summary>
        /// <param name="codeValue"></param>
        public static void SetCodeToCookie(string codeValue)
        {
            if (string.IsNullOrEmpty(doMain))
                CookieHelper.SetCookie(codeCookieName, codeCookieKey, codeValue, 0, CookieEnum.NoSave);
            else
                CookieHelper.SetCookie(codeCookieName, codeCookieKey, codeValue, 0, doMain, CookieEnum.NoSave);
        }

        /// <summary>
        /// 从Cookie获取验证码
        /// </summary>
        /// <returns></returns>
        public static string GetCodeForCookie()
        {
            return CookieHelper.GetCookie(codeCookieName, codeCookieKey);
        }

        /// <summary>
        /// 保存登录用户信息到Cookie
        /// </summary>
        /// <param name="user"></param>
        public static void SetUserToCookie(T_User user)
        {
            string userJsonText = JsonHelper.JsonSerializer<T_User>(user);

            if (string.IsNullOrEmpty(doMain))
                CookieHelper.SetCookie(userCookieName, userCookieKey, userJsonText, 0, CookieEnum.NoSave);
            else
                CookieHelper.SetCookie(userCookieName, userCookieKey, userJsonText, 0, doMain, CookieEnum.NoSave);
        }

        /// <summary>
        /// 从Cookie中获取登录用户信息
        /// </summary>
        /// <returns></returns>
        public static T_User GetUserForCookie()
        {
            try
            {
                string eText = CookieHelper.GetCookie(userCookieName, userCookieKey);
                return JsonHelper.JsonDeserialize<T_User>(eText) ?? new T_User();
            }
            catch (Exception ex)
            {
                return new T_User();
            }
        }

        /// <summary>
        /// 删除Cookie中的用户信息
        /// </summary>
        /// <returns></returns>
        public static bool DeleteUserToCookie()
        {
            CookieHelper.DelCookie(userCookieName);
            return true;
        }

        /// <summary>
        /// 保存当前用户权限列表到Cookie
        /// </summary>
        public static void SetUserRightToCache(T_User user)
        {
            string rightKey = user.LoginName + "_right";
            List<T_Rights> rightList = CacheHelp.GetCache<List<T_Rights>>(rightKey);
            if (rightList == null)
            {
                rightList = new T_UserBLL().GetUserRights(user);
                CacheHelp.AddAbsoluteExpireCache(rightKey, rightList, 24 * 6);
            }
        }

        /// <summary>
        /// 从Cookie中获取当前用户权限列表
        /// </summary>
        /// <returns></returns>
        public static List<T_Rights> GetUserRightForCache(T_User user)
        {
            try
            {
                string rightKey = user.LoginName + "_right";
                List<T_Rights> rightList = CacheHelp.GetCache<List<T_Rights>>(rightKey);
                if (rightList == null)
                {
                    rightList = new T_UserBLL().GetUserRights(user);
                    CacheHelp.AddAbsoluteExpireCache(rightKey, rightList, 24 * 6);
                }
                return rightList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 退出删除权限Cookie信息
        /// </summary>
        /// <returns></returns>
        public static bool DeleteUserRightForCache(T_User user)
        {
            string rightKey = user.LoginName + "_right";
            CacheHelp.ClearCache(rightKey);
            return true;
        }

        #endregion


        /// <summary>
        /// 获取IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetIPadress()
        {
            if (HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] == null)
            {
                return HttpContext.Current.Request.UserHostAddress;
            }
            else
            {
                string[] arr = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].Split(',');
                if (arr.Length > 1)
                {
                    return arr[0];
                }
                else
                {
                    return HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString().PadLeft(30, ' ').Substring(0, 30);
                }
            }
        }

        #region 注册用户加密
        /// <summary>
        ///md5加密处理
        ///</summary>
        public static string GetMD5Str(string strText)
        {
            byte[] result = Encoding.GetEncoding("UTF-8").GetBytes(strText);
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] output = md5.ComputeHash(result);
            return BitConverter.ToString(output).Replace("-", "").ToLower();
        }

        //可配置 key
        public static string GetEncryptPwd(string input_pwd, string randKey)
        {
            return GetMD5Str(GetMD5Str(input_pwd) + randKey);
        }

        //获取随机数
        public static string GetRandom(byte size)
        {
            string chs = "0123456789qwertyuiopasdfghjklzxcvbnm$#@%!&*";
            Random r = null;
            r = new Random();
            string rlt = "";
            for (byte i = 0; i < size; i++)
            {

                int index = r.Next(0, chs.Length);
                rlt += chs[index];
            }
            return rlt;
        }
        #endregion

        public static string strEncode(string content)
        {
            content = content.Replace("script", "s cript");
            content = content.Replace("&", "&amp;");
            content = content.Replace("'", "‘");
            content = content.Replace(";", "&#59;");
            content = content.Replace("%", "％");
            content = content.Replace("\t", "   ");
            content = content.Replace(" ", "&nbsp;");
            content = content.Replace("<", "&lt;");
            content = content.Replace(">", "&gt;");
            content = content.Replace("\n", "<BR>");
            return content;
        }


        public static string SqlSafe(string content)
        {
            content = content.Replace("'", "’");
            content = content.Replace("--", "");
            return content;
        }

        #region 获取季度
        /// <summary>
        /// 获取当前所属季度
        /// </summary>
        /// <returns></returns>
        public static int GetCurrQuarter()
        {
            return Quarter(DateTime.Now);
        }

        /// <summary>
        /// 根据日期获取所属季度
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static int GetQuarterForDate(DateTime dt)
        {
            return Quarter(dt);
        }

        /// <summary>
        /// 根据日期计算季度
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static int Quarter(DateTime dt)
        {
            int quarter = 1;
            int m = dt.Month;
            if (m >= 0 && m <= 3)
                quarter = 1;
            else if (m >= 4 && m <= 6)
                quarter = 2;
            else if (m >= 7 && m <= 9)
                quarter = 3;
            else if (m >= 10 && m <= 12)
                quarter = 4;
            return quarter;
        }

        /// <summary>
        /// DateTime 转时间戳
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GenerateTimeStamp(DateTime dt)
        {
            TimeSpan ts = dt.ToUniversalTime() - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        #endregion
    }

    public static class SqlSafe
    {
        /// <summary>
        /// 客户端参数sql注入安全过滤
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToSqlSafe(this string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str.ToLower().Replace("exec", "").Replace("-", "").Replace("'", "");
                return str;
            }
            else
                return "";
        }
    }
}
