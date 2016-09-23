using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common.Service;

namespace GeneBlood.Web
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btn_SendEmail_Click(object sender, EventArgs e)
        {
            string sid = Session.SessionID;
            new EmailService().Send("1024095780@qq.com", "启点基因", "启点基因");
        }

        protected void btn_SendSMS_Click(object sender, EventArgs e)
        {
            string errMsg = string.Empty;
            new SMSService().Send("1580200947211", "0769", 10, out errMsg);
        }
    }
}