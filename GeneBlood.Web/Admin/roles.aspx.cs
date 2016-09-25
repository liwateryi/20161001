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
    public partial class roles : AdminPageBase
    {
        //角色逻辑处理类
        private T_RolesBLL rolesBLL = new T_RolesBLL();
        //权限逻辑处理类
        private T_RightsBLL rightBLL = new T_RightsBLL();
        //权限角色逻辑处理类
        private T_RolesRightBLL rolesRightBLL = new T_RolesRightBLL();
        public static string failUrl = "roles.aspx";
        public static string successUrl = "roleslist.aspx";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRights();

                string action = Request["action"] ?? "";
                if (action == "update")
                {
                    string rolesId = Request["rolesid"];
                    long rid = 0;
                    long.TryParse(rolesId, out rid);
                    SelectRight(rid);
                }
            }
        }

        //修改选中已选择的角色
        private void SelectRight(long rolesId)
        {
            T_Roles roles = rolesBLL.SelectForID(rolesId);
            if (roles != null)
            {
                //显示待修改的角色
                txtRolesId.Value = roles.RoleId.ToString();
                txtRoles.Text = roles.RoleName;
                txtRoles.ReadOnly = true;

                //选中当前角色所属的权限
                List<T_RolesRight> rolesRightList = rolesRightBLL.SelectAll().Where(p => p.RolesId == rolesId).ToList<T_RolesRight>();
                if (rolesRightList != null && rolesRightList.Count() > 0)
                {
                    foreach (ListItem item in listRights.Items)
                    {
                        if (rolesRightList.Exists(p => p.RightId.ToString() == item.Value))
                            item.Selected = true;
                    }
                }
            }
        }

        //绑定权限列表
        private void BindRights()
        {
            var rightData = rightBLL.SelectAll();
            var parentMenu = rightData.Where(p => p.ParentId == 0);
            foreach (var item in parentMenu)
            {
                listRights.Items.Add(new ListItem { Value = item.Rid.ToString(), Text = "--" + item.RName });
                var childs = rightData.Where(p => p.ParentId == item.Rid);
                foreach (var child in childs)
                {
                    listRights.Items.Add(new ListItem { Value = child.Rid.ToString(), Text = "----" + child.RName });
                }
            }
        }

        //添加角色,分配权限
        protected void btnAddRoles_Click(object sender, EventArgs e)
        {
            T_Logs log = LogHelper.GetLog();

            string rolesName = txtRoles.Text;
            if (!string.IsNullOrEmpty(rolesName))
            {
                try
                {
                    long rid = 0;
                    long.TryParse(txtRolesId.Value, out rid);

                    //新增判断角色名是否存在
                    if (rid == 0)
                    {
                        var roles = rolesBLL.SelectAll().FirstOrDefault(p => p.RoleName == rolesName);
                        if (roles != null)
                            JsAlertClose("角色名已经存在，请勿重复添加！");
                    }

                    //当前角色
                    var newRoles = new T_Roles
                    {
                        CreateDate = DateTime.Now,
                        RoleName = rolesName
                    };

                    //修改需要指定主键id
                    if (rid > 0)
                        newRoles.RoleId = rid;

                    List<long> rightIds = null;
                    //选中的权限id
                    if (listRights != null && listRights.Items.Count > 0)
                    {
                        rightIds = new List<long>();
                        foreach (ListItem item in listRights.Items)
                        {
                            if (item.Selected && !string.IsNullOrEmpty(item.Value))
                                rightIds.Add(Convert.ToInt64(item.Value));
                        }
                    }
                    T_Roles resultRoles = rolesBLL.AddRolesForRights(newRoles, rightIds);
                    if (resultRoles.RoleId > 0)
                    {
                        JsAlert(string.Format("编辑【{0}】,保存成功！", rolesName),successUrl);
                        log.LogContext = string.Format("编辑角色名【{0}】保存成功！", rolesName);
                    }
                    else
                    {
                        JsAlert(string.Format("编辑【{0}】保存失败！", rolesName),failUrl);
                        log.LogContext = string.Format("编辑角色名 【{0}】保存失败！", rolesName);
                    }
                    LogHelper.WriteOperationLog(log);

                }
                catch (Exception ex)
                {
                    log.LogContext = string.Format("编辑角色名【{0}】，服务器异常", rolesName);
                    LogHelper.WriteOperationLog(log);
                    JsAlert(ex.Message,failUrl);
                }
               
            }
            else
            {
                JsAlert("请填写角色名", failUrl);
            }
        }
    }
}