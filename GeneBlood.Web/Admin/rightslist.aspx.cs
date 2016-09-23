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
    public partial class rightslist : AdminPageBase
    {
        //权限逻辑处理类
        private T_RightsBLL rightBLL = new BLL.T_RightsBLL();

        public List<T_Rights> rightList = null;

        public static string successUrl = "rightslist.aspx";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindRightList();
                string action = Request["action"] ?? "";
                string rightId = Request["rightid"] ?? "0";
                long rid = 0;
                long.TryParse(rightId, out rid);
                if (action == "delete")
                {
                    DeleteRightForId(rid);
                }
            }
        }

        //获取权限列表
        private void BindRightList()
        {
            rightList = rightBLL.SelectAll().OrderByDescending(p => p.OrderByNum).ToList<T_Rights>();
        }

        //删除权限
        private void DeleteRightForId(long rightId)
        {
            if (rightId > 0)
            {
                T_Logs log = LogHelper.GetLog();
                try
                {
                    if (rightBLL.Delete(rightId))
                    {
                        JsAlert("操作成功!", successUrl);
                        log.LogContext = "删除权限操作成功！ rightid" + rightId;
                    }
                    else
                    {
                        JsAlert("操作失败!", successUrl);
                        log.LogContext = "删除权限操作失败！ rightid" + rightId;
                    }
                    LogHelper.WriteOperationLog(log);

                }
                catch (Exception ex)
                {
                    log.LogContext = "删除权限服务器异常！";
                    LogHelper.WriteOperationLog(log);
                }
            }
        }
    }
}