using GeneBlood.BLL;
using GeneBlood.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace GeneBlood.Web.Admin
{
    public partial class index :AdminPageBase
    {
        //权限逻辑处理类
        private T_RightsBLL rightBLL = new T_RightsBLL();

        //菜单
        //public string MenuText = string.Empty;
        public StringBuilder MenuText = new StringBuilder();

        public string UserName = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            BindMenu();
        }

        /// <summary>
        /// 绑定菜单
        /// </summary>
        //private void BindMenu()
        //{
        //    T_User user = new T_User { Id = 1, LoginName = "admin" }; //Static.GetUserForCookie();
        //    if (user != null)
        //    {
        //        //显示登录用户
        //        UserName = user.LoginName;

        //        //显示权限菜单
        //        List<T_Rights> rightList = Static.GetUserRightForCache(user);
        //        IEnumerable<T_Rights> parents = rightList.Where(p => p.ParentId == 0);
        //        foreach (T_Rights item in parents)
        //        {
        //            CreateMenu(item, rightList);
        //        }
        //        //MenuText.AppendFormat("<h3>{0}</h3><ul><li onclick=\"NavMenuUrl('{1}','{0}','{2}')\">{0}</li></ul>", "密码管理", "pwd.aspx", "修改密码");
        //    }
        //}

        private void BindMenu()
        {
            T_User user =  Static.GetUserForCookie();//new T_User { Id = 1, LoginName = "admin" };
            if (user != null)
            {
                //显示登录用户
                UserName = user.LoginName;

                //显示权限菜单
                List<T_Rights> rightList = Static.GetUserRightForCache(user);
                IEnumerable<T_Rights> parents = rightList.Where(p => p.ParentId == 0);
                foreach (T_Rights item in parents)
                {
                    MenuText.AppendFormat("<dl id='menu-article'><dt><i class='Hui-iconfont'></i>{0}<i class='Hui-iconfont menu_dropdown-arrow'>&#xe6d5;</i></dt><dd><ul>", item.RName);
                    IEnumerable<T_Rights> childs = rightList.Where(p => p.ParentId == item.Rid && p.IsDisplay == 0);
                    foreach (var child in childs)
                    {
                        MenuText.AppendFormat("<li><a onclick=\"NavMenuUrl('{0}','{1}','{2}')\" href='javascript:void(0)'>{2}</a></li>", child.Url,item.RName, child.RName);
                    }
                    MenuText.Append("</ul></dd></dl>");
                }
                //MenuText.AppendFormat("<h3>{0}</h3><ul><li onclick=\"NavMenuUrl('{1}','{0}','{2}')\">{0}</li></ul>", "密码管理", "pwd.aspx", "修改密码");
            }
        }

        private string CreateMenu(T_Rights currRight, IEnumerable<T_Rights> rightList)
        {
            
            MenuText.AppendFormat("<dl id='menu-article'><dt><i class='Hui-iconfont'></i>{0}<i class='Hui-iconfont menu_dropdown-arrow'>&#xe6d5;</i></dt><dd><ul>", currRight.RName);
            IEnumerable<T_Rights> childs = rightList.Where(p => p.ParentId == currRight.Rid && p.IsDisplay == 0);
            foreach (var child in childs)
            {
                MenuText.AppendFormat("<li><a _href='{0}' href='javascript:void(0)'>{1}</a></li>", child.Url, child.RName);
                IEnumerable<T_Rights> childs1 = rightList.Where(p => p.ParentId == child.Rid && p.IsDisplay == 0);
                if (childs != null && childs.Count() > 0)
                {
                    CreateMenu(child, childs1);
                }
            }
            MenuText.Append("</ul></dd></dl>");
            return MenuText.ToString();
        }
    }
}