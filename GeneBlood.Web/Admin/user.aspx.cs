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
    public partial class user : AdminPageBase
    {
        //用户逻辑处理类
        private T_UserBLL userBLL = new T_UserBLL();

        //角色逻辑处理类
        private T_RolesBLL rolesBLL = new T_RolesBLL();

        //用户角色关联处理类
        private T_UserRoleBLL userroleBLL = new T_UserRoleBLL();

        private ListItem defaultSelected = new ListItem { Value = "", Text = "--请选择--" };
        public T_User currUser = null;

        public static string failUrl = "user.aspx";
        public static string successUrl = "userlist.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    string action = Request["action"];
                    string userId = Request["userid"];
                    if (action == "update")
                    {
                        long uId = 0;
                        long.TryParse(userId, out uId);
                        if (uId > 0)
                        {
                            BindUserData(uId);
                        }
                        else
                        {
                            JsAlert("用户id为:" + uId + "不存在!", "#");
                        }
                    }
                    else
                    {
                        BindRoles(null);
                    }
                }
            }
            catch (Exception ex)
            {
                JsAlert(ex.Message, failUrl);
            }
        }

        //保存
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string UserId = string.IsNullOrEmpty(txtUserId.Value) ? "0" : txtUserId.Value;
            string loginName = txtLoginName.Text;
            string randKey = Static.GetRandom(6);
            string trueName = txtTrueName.Text;
            string qq = txtQQ.Text;
            string email = txtEmail.Text;
            string phone = txtPhone.Text;
            string intro = txtIntro.Text;

            T_Logs log = LogHelper.GetLog();
            try
            {
                T_User user = new T_User();
                user.UTime = DateTime.Now;
                user.QQ = qq;
                user.Email = email;
                user.Phone = phone;
                user.Intro=intro;
                List<long> listRolesIds = new List<long>();
                foreach (ListItem item in listRoles.Items)
                {
                    if (item.Selected)
                        listRolesIds.Add(Convert.ToInt64(item.Value));
                }

                string msg = string.Empty;

                if (UserId == "0")
                {
                    user.LoginName = loginName.ToLower();
                    user.RandKey = randKey;
                    user.Pwd = Static.GetEncryptPwd(Static.defaultPwd, randKey); ;
                    user.CTime = DateTime.Now;
                    user.TrueName = trueName;
                    user.Contact = trueName;
                    user.State = 0;
                    user.UserType = 0;
                    msg = "新增用户";
                }
                else
                {
                    user.Id = Convert.ToInt64(UserId);
                    user.TrueName = trueName;
                    msg = "修改用户";
                }
                if (user.Id == 0)
                {
                    if (userBLL.ExistsUserName(user) != null)
                    {
                        JsAlert("登录名:" + user.LoginName + " 已经存在！", failUrl);
                        return;
                    }
                }
                if (userBLL.Edit(user, listRolesIds))
                    log.LogContext = msg + ",成功！";
                else
                    log.LogContext = msg + ",失败！";

                //记录操作日志
                LogHelper.WriteOperationLog(log);

                JsAlert(log.LogContext, successUrl);
            }
            catch (Exception ex)
            {
                log.LogContext = "编辑用户，服务器异常！" + ex.Message + ex.StackTrace;
                LogHelper.WriteOperationLog(log);
                JsAlert(ex.Message, failUrl);
            }
        }

        //修改用户表单赋值
        private void BindUserData(long userId)
        {
            T_User user = userBLL.SelectForID(userId);
            currUser = user;
            if (user != null)
            {
                BindRoles(user);

                txtUserId.Value = user.Id.ToString();
                txtLoginName.Text = user.LoginName;
                txtTrueName.Text = user.TrueName;
                txtPhone.Text = user.Phone;
                txtQQ.Text = user.QQ;
                txtEmail.Text = user.Email;
                txtIntro.Text = user.Intro;
                txtLoginName.ReadOnly = true;
            }
        }

        //绑定角色
        private void BindRoles(T_User user)
        {
            listRoles.DataTextField = "RoleName";
            listRoles.DataValueField = "RoleId";
            listRoles.DataSource = rolesBLL.SelectAll();
            listRoles.DataBind();

            //选中已选择的角色
            if (user != null)
            {
                List<T_UserRole> rolesList = userroleBLL.SelectAll().Where(p => p.UserId == user.Id).ToList<T_UserRole>();
                if (rolesList != null && rolesList.Count() > 0)
                {
                    foreach (ListItem item in listRoles.Items)
                    {
                        if (rolesList.FirstOrDefault(p => p.RoleId.ToString() == item.Value) != null)
                            item.Selected = true;
                        else
                            continue;
                    }
                }
            }
        }
    }
}