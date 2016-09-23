using DataHelp;
using GeneBlood.BLL;
using GeneBlood.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GeneBlood.Web.Admin
{
    public partial class log : System.Web.UI.Page
    {
        //操作日志逻辑处理类
        private T_LogsBLL logBLL = new T_LogsBLL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
                BindLogData(1);
        }

        //显示最新用户操作记录
        private void BindLogData(int pageIndex)
        {
            T_User user = new T_User();//Static.GetUserForCookie();
            if (user != null && user.UserType == 0)
            {
                try
                {
                    PageClass pageClass = new PageClass();
                    pageClass.TableName = "t_logs";
                    pageClass.ShowField = "*";
                    pageClass.WhereText = "";
                    pageClass.OrderText = "LogTime desc";
                    pageClass.PageSize = 10;
                    pageClass.PageIndex = pageIndex;

                    var data = logBLL.SelectForPage(pageClass);
                    AspNetPager1.PageSize = pageClass.PageSize;
                    AspNetPager1.RecordCount = pageClass.DataCount;
                    repLogList.DataSource = data;
                    repLogList.DataBind();
                }
                catch (Exception ex)
                {

                }
            }
            else
            {
                loglist.Visible = false;
            }
        }

        //当前选中页
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindLogData(AspNetPager1.CurrentPageIndex);
        }
    }
}