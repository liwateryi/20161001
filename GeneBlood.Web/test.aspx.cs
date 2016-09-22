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
            //EmailService em = new EmailService();
            //string bodyEmail = em.GetEmailBody("water", EmailService._EM_TYPE_.Pub, "");
            //em.sendEmail("1024095780@qq.com", "", "测试", bodyEmail, true, "");

            new EmailService().SendEmail1();
        }

        protected void btn_SendSMS_Click(object sender, EventArgs e)
        {

            new SMSService().Send("15802009472", "1234", 10);

        }
    }
}