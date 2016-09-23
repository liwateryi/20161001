using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace GeneBlood.Web
{
    /// <summary>
    /// Cookie 操作类
    /// </summary>
    public class CookieHelper
    {
        /// <summary>
        /// 设置Cookie
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="strKey">键</param>
        /// <param name="strValue">值</param>
        /// <param name="num">数字(作用于saveType)</param>
        /// <param name="saveType">保存类型</param>
        public static void SetCookie(string cookieName, string strKey, string strValue, int num, CookieEnum saveType)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            DateTime dt = DateTime.Now;
            switch (saveType)
            {
                case CookieEnum.NoSave:
                    break;
                case CookieEnum.Second:
                    dt = dt.AddSeconds(num);
                    cookie.Expires = dt;
                    break;
                case CookieEnum.Minute:
                    dt = dt.AddMinutes(num);
                    cookie.Expires = dt;
                    break;
                case CookieEnum.Hour:
                    dt = dt.AddHours(num);
                    cookie.Expires = dt;
                    break;
                case CookieEnum.Day:
                    dt = dt.AddDays(num);
                    cookie.Expires = dt;
                    break;
                case CookieEnum.Month:
                    dt = dt.AddMinutes(num);
                    cookie.Expires = dt;
                    break;
                case CookieEnum.Year:
                    dt = dt.AddYears(num);
                    cookie.Expires = dt;
                    break;
                default:
                    break;
            }
            cookie.Path = "/";
            cookie.Values.Add(strKey, strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 设置Cookie 指定域名
        /// </summary>
        /// <param name="cookieName">cookie名称</param>
        /// <param name="strKey">键</param>
        /// <param name="strValue">值</param>
        /// <param name="num">数字(作用于saveType)</param>
        /// <param name="doMain">域名</param>
        /// <param name="saveType">保存类型</param>
        public static void SetCookie(string cookieName, string strKey, string strValue, int num, string doMain, CookieEnum saveType)
        {
            HttpCookie cookie = new HttpCookie(cookieName);
            if (!string.IsNullOrEmpty(doMain))
                cookie.Domain = doMain;
            DateTime dt = DateTime.Now;
            switch (saveType)
            {
                case CookieEnum.NoSave:
                    break;
                case CookieEnum.Second:
                    dt = dt.AddSeconds(num);
                    cookie.Expires = dt;
                    break;
                case CookieEnum.Minute:
                    dt = dt.AddMinutes(num);
                    cookie.Expires = dt;
                    break;
                case CookieEnum.Hour:
                    dt = dt.AddHours(num);
                    cookie.Expires = dt;
                    break;
                case CookieEnum.Day:
                    dt = dt.AddDays(num);
                    cookie.Expires = dt;
                    break;
                case CookieEnum.Month:
                    dt = dt.AddMinutes(num);
                    cookie.Expires = dt;
                    break;
                case CookieEnum.Year:
                    dt = dt.AddYears(num);
                    cookie.Expires = dt;
                    break;
                default:
                    break;
            }
            cookie.Path = "/";
            cookie.Values.Add(strKey, strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 取Cookie
        /// </summary>
        /// <param name="CookieName">cookie名称</param>
        /// <param name="strKey">cookie键</param>
        /// <returns></returns>
        public static string GetCookie(string cookieName, string strKey)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];

            if (cookie == null)
                return "";
            else
                return cookie.Values[strKey];
        }

        /// <summary>
        /// 删除Cookie
        /// </summary>
        /// <param name="CookieName">Cookie名称</param>
        /// <returns></returns>
        public static void DelCookie(string cookieName)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[cookieName];
            if (cookie != null)
            {
                HttpContext.Current.Request.Cookies[cookieName].Expires = DateTime.Now.AddDays(-1d);
                cookie.Expires = DateTime.Now.AddDays(-1);

                System.Web.HttpContext.Current.Response.AppendCookie(cookie);
            }
        }
    }

    /// <summary>
    /// 保存 Cookies 类型枚举
    /// </summary>
    public enum CookieEnum
    {
        /// <summary>
        /// 会话模式
        /// </summary>
        NoSave,
        /// <summary>
        /// 保存秒
        /// </summary>
        Second,
        /// <summary>
        /// 保存分钟
        /// </summary>
        Minute,
        /// <summary>
        /// 保存小时
        /// </summary>
        Hour,
        /// <summary>
        /// 保存天
        /// </summary>
        Day,
        /// <summary>
        /// 保存月
        /// </summary>
        Month,
        /// <summary>
        /// 保存年
        /// </summary>
        Year
    }
}
