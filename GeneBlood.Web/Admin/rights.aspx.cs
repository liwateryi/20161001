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
    public partial class rights : AdminPageBase
    {
        // 角色业务处理类
        private T_RolesBLL rolesBll = new T_RolesBLL();

        //权限业务处理类
        private T_RightsBLL rightBll = new T_RightsBLL();

        //权限角色关联逻辑处理类
        private static T_RolesRightBLL rolesRightBLL = new T_RolesRightBLL();

        public static string failUrl = "rights.aspx";
        public static string successUrl = "rightslist.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                try
                {
                    BindRight();
                    BindRoles();

                    string action = Request["action"] ?? "";
                    string rightId = Request["rightid"] ?? "0";

                    long rid = 0;
                    long.TryParse(rightId, out rid);
                    BindCurrRight(rid);

                }
                catch (Exception ex)
                {
                    JsAlert(ex.Message, failUrl);
                }
            }
        }


        //修改权限
        private void BindCurrRight(long rightId)
        {
            if (rightId > 0)
            {
                //修改操作 todo...
                T_Rights right = rightBll.SelectForID(rightId);

                if (right != null)
                {
                    //选择菜单
                    txtRightName.Text = right.RName;
                    txtUrl.Text = right.Url;
                    txtRightId.Value = right.Rid.ToString();
                    chkDisplay.Checked = right.IsDisplay == 0 ? true : false;
                    txtOrderNum.Text = right.OrderByNum.ToString();

                    //选中对应的菜单
                    foreach (ListItem item in listRight.Items)
                    {
                        if (item.Value.Split(',')[0] == right.Rid.ToString())
                            item.Selected = true;
                        else
                            continue;
                    }

                    //选中对应的角色
                    List<T_RolesRight> rolesRight = rolesRightBLL.SelectAll();
                    if (rolesRight != null && rolesRight.Count > 0)
                    {
                        List<T_RolesRight> currRoleList = rolesRight.Where(p => p.RightId == right.Rid).ToList<T_RolesRight>();
                        if (currRoleList != null && currRoleList.Count > 0)
                        {
                            foreach (ListItem item in listRoles.Items)
                            {
                                if (currRoleList.Exists(p => p.RolesId.ToString() == item.Value))
                                    item.Selected = true;
                            }
                        }
                    }
                }
            }
        }


        //绑定权限
        private void BindRight()
        {

            var rightData = rightBll.SelectAll();

            var parents = rightData.Where(p => p.ParentId == 0);
            foreach (var item in parents)
            {
                listRight.Items.Add(new ListItem { Value = item.Rid.ToString() + "," + item.NLevel, Text = "--" + item.RName });
                var childs = rightData.Where(p => p.ParentId == item.Rid);
                foreach (var child in childs)
                {
                    listRight.Items.Add(new ListItem { Value = child.Rid.ToString() + "," + child.NLevel, Text = "----" + child.RName });
                }
            }
            listRight.Items.Insert(0, new ListItem { Value = "0,0", Text = "一级菜单" });
        }

        //绑定角色
        private void BindRoles()
        {
            listRoles.DataTextField = "RoleName";
            listRoles.DataValueField = "RoleId";
            listRoles.DataSource = rolesBll.SelectAll();
            listRoles.DataBind();
        }

        //添加权限(菜单)
        protected void btn_AddRight_Click(object sender, EventArgs e)
        {
            string rightName = txtRightName.Text;
            string url = txtUrl.Text;
            int isDisplayMenu = chkDisplay.Checked ? 0 : 1;
            long parentId = Convert.ToInt64(listRight.SelectedItem.Value.Split(',')[0]);
            int level = Convert.ToInt32(listRight.SelectedItem.Value.Split(',')[1]);
            long rid = Convert.ToInt64(txtRightId.Value);
            int orderNum = Convert.ToInt32(txtOrderNum.Text ?? "0");

            T_Logs log = LogHelper.GetLog();
            log.LogType = "系统权限编辑";

            try
            {
                //新增菜单
                T_Rights right = new T_Rights
                {
                    RName = rightName,
                    Url = url,
                    IsDisplay = isDisplayMenu,
                    CreateDate = DateTime.Now,
                    OrderByNum = orderNum
                };
                if (rid > 0)
                {
                    right.NLevel = level;
                    right.Rid = rid;
                }
                else
                {
                    right.ParentId = parentId;
                    right.NLevel = level + 1;
                }
                //分配角色
                List<long> rolesIds = null;
                if (listRoles != null && listRoles.Items.Count > 0)
                {
                    rolesIds = new List<long>();
                    foreach (ListItem item in listRoles.Items)
                    {
                        if (item.Selected && !string.IsNullOrEmpty(item.Value))
                        {
                            rolesIds.Add(Convert.ToInt64(item.Value));
                        }
                    }
                }

                T_Rights resultRight = rightBll.AddRightToRoles(right, rolesIds);
                if (resultRight.Rid > 0)
                {
                    log.LogContext = rightName + "权限编辑成功！";
                    JsAlert(string.Format("权限名：{0} 保存成功！", rightName), successUrl);
                }
                else
                {
                    log.LogContext = rightName + "权限保存失败！";
                    JsAlert(string.Format("权限名：{0} 添加失败！", rightName), failUrl);
                }
                //记录日志
                LogHelper.WriteOperationLog(log);
            }
            catch (Exception ex)
            {
                log.LogContext = rightName + "权限编辑异常！," + ex.Message + ex.StackTrace;
                JsAlert("服务器异常！", "#");
                //JsAlert(ex.Message, failUrl);
            }
            LogHelper.WriteOperationLog(log);
        }
    }
}