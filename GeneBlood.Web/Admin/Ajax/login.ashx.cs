using GeneBlood.BLL;
using GeneBlood.Model;
using GeneBlood.Web.Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneBlood.Web.Admin.Ajax
{
    /// <summary>
    /// login 的摘要说明
    /// </summary>
    public class login : IHttpHandler
    {

        //用户逻辑处理类
        private T_UserBLL userBLL = new T_UserBLL();

        public void ProcessRequest(HttpContext context)
        {
            string u = context.Request["u"] ?? "";
            string p = context.Request["p"] ?? "";
            string c = context.Request["c"] ?? "";
            string action = context.Request["action"] ?? "";

            switch (action)
            {
                case "dologin":
                    context.Response.Write(DoLogin(context));
                    context.Response.End();
                    break;
                case "checklogin":
                    context.Response.Write(CheckLogin(context));
                    context.Response.End();
                    break;
                case "logout":
                    context.Response.Write(LogOut(context));
                    context.Response.End();
                    break;
                case "pwd":
                    context.Response.Write(Pwd(context));
                    context.Response.End();
                    break;
                default:
                    context.Response.Write("{" + "\"ok\":-6,\"msg\":\"非法请求！\"" + "}");
                    break;
            }
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string Pwd(HttpContext context)
        {
            string resultText = "\"ok\":{0},\"msg\":\"{1}\"";
            string resultMsg = string.Empty;

            string p1 = context.Request["p1"];
            string p2 = context.Request["p2"];
            string p3 = context.Request["p3"];
            if (string.IsNullOrEmpty(p1) || string.IsNullOrEmpty(p2) || string.IsNullOrEmpty(p3))
            {
                resultMsg = string.Format(resultText, -1, "参数不对！");
            }
            else
            {
                //获取当前用户
                T_User user = Static.GetUserForCookie();
                if (user != null)
                {
                    if (user.Pwd == Static.GetEncryptPwd(p1.ToLower(), user.RandKey))
                    {
                        T_User newUser = new T_User()
                        {
                            Id = user.Id,
                            Pwd = Static.GetEncryptPwd(p2.ToLower(), user.RandKey)
                        };
                        if (userBLL.Update(newUser))
                        {
                            Static.DeleteUserToCookie();
                            resultMsg = string.Format(resultText, 0, "操作成功！");

                            T_Logs log = LogHelper.GetLog();
                            log.LogContext = user.LoginName + " 成功修改密码!";
                            LogHelper.WriteOperationLog(log);
                        }
                        else
                        {
                            resultMsg = string.Format(resultText, -2, "操作失败！");
                        }
                    }
                    else
                        resultMsg = string.Format(resultText, -3, "原密码不正确！");
                }
                else
                {
                    resultMsg = string.Format(resultText, -4, "请登录后再进行操作！");
                }
            }
            return "{" + resultMsg + "}";
        }

        /// <summary>
        ///登录实现
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string DoLogin(HttpContext context)
        {
            string resultText = "\"ok\":{0},\"msg\":\"{1}\"";
            string resultMsg = string.Empty;
            string u = context.Request["u"] ?? "";
            string p = context.Request["p"] ?? "";
            string c = context.Request["c"] ?? "";
            try
            {
                string code = Static.GetCodeForCookie();
                if (u.Length == 0 || p.Length == 0 && c.Length == 0)
                {
                    resultMsg = string.Format(resultText, "-1", "参数异常！");
                }
                else if (code != c)
                {
                    resultMsg = string.Format(resultText, "-2", "验证码错误！");
                }
                else
                {
                    T_User user = userBLL.GetUserForUserName(u);
                    if (user != null)
                    {
                        string pwd = Static.GetEncryptPwd(p.ToLower(), user.RandKey);
                        if (pwd == user.Pwd)
                        {
                            //保存用户信息到Cookie
                            Static.SetUserToCookie(user);
                            resultMsg = string.Format(resultText, "0", "登录成功！");

                            //获取当前用户权限加载到缓存
                            Static.SetUserRightToCache(user);

                            //记录登录日志 todo...
                            LogHelper.WriteOperationLog("登录", user.LoginName + " 成功登录系统", user.LoginName, Static.GetIPadress());
                        }
                        else
                        {//到此处其实是密码错误，此处提示用户名或密码错误
                            resultMsg = string.Format(resultText, "-3", "用户名或密码错误！");
                        }
                    }
                    else
                    {
                        resultMsg = string.Format(resultText, "-4", "用户名不存在！");
                    }
                }
            }
            catch (Exception ex)
            {
                resultMsg = string.Format(resultText, "-5", "服务器异常！");
                LogHelper.WriteOperationLog("登录", u + " 服务器异常异常" + ex.Message + ex.StackTrace, u, Static.GetIPadress());
            }
            return "{" + resultMsg + "}";
        }

        /// <summary>
        /// 检测登录
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string CheckLogin(HttpContext context)
        {
            string resultText = "\"ok\":{0},\"msg\":\"{1}\"";
            string resultMsg = string.Empty;
            try
            {
                T_User user = Static.GetUserForCookie();
                if (user != null && user.Id > 0)
                {
                    resultMsg = string.Format(resultText, "0", "登录成功！");
                }
                else
                {
                    resultMsg = string.Format(resultText, "1", "未登录！");
                }
            }
            catch (Exception ex)
            {
                resultMsg = resultMsg = string.Format(resultText, "-5", "服务器异常！");
            }
            return "{" + resultMsg + "}";
        }

        /// <summary>
        /// 用户退出
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private string LogOut(HttpContext context)
        {
            string resultText = "\"ok\":{0},\"msg\":\"{1}\"";
            string resultMsg = string.Empty;
            try
            {
                T_User user = Static.GetUserForCookie();

                //记录日志
                if (user != null)
                    LogHelper.WriteOperationLog("退出系统", user.LoginName + " 成功退出系统", user.LoginName, Static.GetIPadress());

                //清除当前用户权限Cookie数据
                Static.DeleteUserRightForCache(user);

                //清除当前会话用户数据
                Static.DeleteUserToCookie();

                resultMsg = string.Format(resultText, "0", "成功退出！");
            }
            catch (Exception ex)
            {
                resultMsg = string.Format(resultText, "0", "服务器异常！");
            }
            return "{" + resultMsg + "}";
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}