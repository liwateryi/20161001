using DataHelp;
using GeneBlood.BLL;
using GeneBlood.Model;
using GeneBlood.Web.Admin.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GeneBlood.Web.Admin
{
    public partial class userlist : AdminPageBase
    {
        //用户逻辑处理类
        private T_UserBLL userBLL = new T_UserBLL();

        public string failUrl = "user.aspx";
        public string successUrl = "userlist.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //绑定用户数据
                BindUserList(1);
                Delete();
            }
        }

        //禁用，启用，删除用户
        private void Delete()
        {
            try
            {
                string action = Request["action"] ?? "";
                if (!string.IsNullOrEmpty(action))
                {
                    T_User user = new T_User();
                    user.Id = Convert.ToInt64(Request["userid"] ?? "0");
                    user = userBLL.SelectForID(user.Id);
                    if (user == null)
                    {
                        throw new Exception("该用户不存在！userid" + user.Id);
                    }
                    bool isTrue = false;
                    if (action == "delete")//禁用，启用
                    {
                        user.State = Convert.ToInt32(Request["isdelete"] ?? "0");
                        isTrue = userBLL.Update(user);
                    }
                    if (action == "dodelete")//直接删除
                    {
                        isTrue = userBLL.Delete(user.Id);
                    }
                    if (action == "resetpwd")//重置密码
                    {
                        user.Pwd = Static.GetEncryptPwd(Static.defaultPwd, user.RandKey);
                        isTrue = userBLL.Update(user);
                    }
                    if (isTrue)
                    {
                        //登录用户
                        T_User curruser = Static.GetUserForCookie();
                        if (user != null)
                        {
                            T_Logs log = new T_Logs();
                            log.UserName = curruser.LoginName;
                            log.Ip = Static.GetIPadress();
                            log.LogTime = DateTime.Now;
                            //被操作用户
                            T_User opsuser = Static.GetUserForCookie();
                            if (action == "dodelete")
                            {
                                log.LogContext = " 删除账号 " + user.LoginName;
                            }
                            else if (action == "delete")
                            {
                                if (user.State == 0)
                                {
                                    log.LogContext = "启用账号 " + user.LoginName;
                                }
                                else
                                {
                                    log.LogContext = "禁用账号 " + user.LoginName;
                                }
                            }
                            else if (action == "resetpwd")
                            {
                                log.LogContext = user.LoginName + "账号密码重置";
                            }
                            else
                            {
                                log.LogContext = "恶意操作(禁用，启用，删除用户)";
                            }
                            LogHelper.WriteOperationLog(log);
                            JsAlert("操作成功！", successUrl);
                        }
                        else
                        {
                            JsAlert("操作失败！", successUrl);
                        }

                    }

                }
            }
            catch (Exception ex)
            {
                JsAlert("服务器异常！", successUrl);
            }
        }


        //绑定用户列表
        private void BindUserList(int pageIndex)
        {
            string name = txtSeach.Text;
            string whereText = " 1=1 ";
            if (!string.IsNullOrEmpty(name))
                whereText += " and (LoginName like '%" + name + "%' or TrueName like '%" + name + "%' or Email like '%" + name + "%')";
            PageClass pageClass = new PageClass();
            pageClass.TableName = "T_User";
            pageClass.ShowField = "*";
            pageClass.WhereText = whereText;
            pageClass.OrderText = "CTime desc";
            pageClass.PageSize = 1;
            pageClass.PageIndex = pageIndex;
            try
            {
                var data = userBLL.SelectForPage(pageClass);
                AspNetPager1.PageSize = pageClass.PageSize;
                AspNetPager1.RecordCount = pageClass.DataCount;
                repUserList.DataSource = data;
                repUserList.DataBind();
            }
            catch (Exception ex)
            {
                JsAlert(ex);
            }
        }

        //当前选中页
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindUserList(AspNetPager1.CurrentPageIndex);
        }

        //查询
        protected void btnSeachUser_Click(object sender, EventArgs e)
        {
            BindUserList(1);
        }


        protected string IsDelete(object obj)
        {
            if (obj == null || obj.ToString() == "0")
                return "是";
            else
                return "否";
        }

        protected string IsDelete(object obj, object userId)
        {
            //IsDelete(msg, userid, isdelete) 
            string opText = "<a style='text-decoration:none' onclick=\"IsDelete('{0}',{1},{2})\" href='javascript:;' title='{0}' class='btn btn-primary radius'><i ></i>{0}</a> ";
            if (obj == null || obj.ToString() == "0")
                return string.Format(opText, "禁用", userId, 1);
            else
                return string.Format(opText, "启用", userId, 0);
        }
    }
}