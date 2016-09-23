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
    public partial class roleslist : AdminPageBase
    {
        //角色逻辑处理类
        private T_RolesBLL rolesBLL = new T_RolesBLL();

        public List<T_Roles> rolesList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindRolesData();
                string action = Request["action"] ?? "";
                if (action == "delete")
                {
                    string rolesId = Request["rolesid"];
                    long rid = 0;
                    long.TryParse(rolesId, out rid);
                    DeleteRoleForId(rid);
                }
            }
        }

        //绑定角色列表
        private void BindRolesData()
        {
            rolesList = rolesBLL.SelectAll();
        }

        //删除角色
        private void DeleteRoleForId(long rolesId)
        {
            T_Logs log = LogHelper.GetLog();
            try
            {
                //删除角色todo...
                if (rolesBLL.Delete(rolesId))
                {
                    log.LogContext = "删除角色，id为:" + rolesId;
                    LogHelper.WriteOperationLog(log);
                }
                JsAlert("操作成功！", "#");
            }
            catch (Exception ex)
            {
                log.LogContext = ex.Message + ex.StackTrace;
                LogHelper.WriteOperationLog(log);
                JsAlert("服务器异常！", "#");
            }
        }
    }
}