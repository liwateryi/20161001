using GeneBlood.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GeneBlood.Web
{
    public class AdminPageBase : System.Web.UI.Page
    {
        public AdminPageBase()
        {
            Load += new EventHandler(PageBase_Load);
        }

        void PageBase_Load(object sender, EventArgs e)
        {
            if (System.Configuration.ConfigurationManager.AppSettings["IsRights"].ToString().ToLower() == "true")
            {
                T_User user = Static.GetUserForCookie();
                string executionFilePath = Request.CurrentExecutionFilePath;
                string fileName = System.IO.Path.GetFileName(executionFilePath);
                string ex = System.IO.Path.GetExtension(Request.PhysicalPath);
                if (user == null)
                {
                    Response.Redirect("/Admin/login.html");
                }
                else
                {
                    if (ex.ToLower() == ".aspx" && fileName != "index.aspx" )//
                    {
                        //判断是否具有访问权限
                        List<T_Rights> rightList = Static.GetUserRightForCache(user);
                        if (rightList != null && rightList.Exists(p => p.Url.Contains(executionFilePath)))
                        {
                            //有访问权限，直接显示页面
                        }
                        else
                        {
                            //否则提示无访问权限
                            Response.Redirect("/Admin/norights.html");
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 提示，跳转
        /// </summary>
        /// <param name="Msg">提示信息</param>
        /// <param name="Url">跳转url</param>
        protected void JsAlert(string Msg, string Url)
        {
            Msg = Msg.Replace("\r\n", "");
            HttpContext.Current.Response.Write("<script>alert('" + Msg + "');window.location.href='" + Url + "';</script>");
        }

        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="Msg">提示信息</param>
        protected void JsAlert(string Msg)
        {
            Msg = Msg.Replace("\r\n", "");
            HttpContext.Current.Response.Write("<script>alert('" + Msg + "');</script>");
        }

        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="Msg">提示信息</param>
        protected void JsAlert(Exception ex)
        {
            string Msg = ex.Message;
            Msg = Msg.Replace("\r\n", "");
            HttpContext.Current.Response.Write("<script>alert('" + Msg + "');</script>");
        }

        /// <summary>
        /// 提示
        /// </summary>
        /// <param name="Msg">提示信息</param>
        protected void JsAlertClose(string Msg)
        {
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "alert('" + Msg + "');layerclose();</script>",true);
        }
    }
}