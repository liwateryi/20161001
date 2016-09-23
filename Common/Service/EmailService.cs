using MyCls;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace Common.Service
{
    public class EmailService
    {
        public enum _EM_TYPE_
        {
            Pub
        }

        public string GetEmailBody(string User, _EM_TYPE_ type, params string[] msg)
        {
            string emailbody = "";
            switch (type)
            {
                case _EM_TYPE_.Pub:
                    emailbody = "testtesttesttesttesttesttesttesttesttest";
                    break;
            }
            return emailbody;
        }

        private string _SYS_EMAIL_SRV = "smtp.qq.com";
        private string _SYS_EMAIL_PORT = "25";
        private string _SYS_EMAIL_ADDR = "3102310972@qq.com";
        private string _SYS_EMAIL_PWD = "ufkkzbqcgrtsdcia";
        private string _SYS_EMAIL_DISPNAME = "启点基因";
        //http://www.cnblogs.com/sunyl/p/5490169.html
        public void Send(string to, string subject, string body, bool isHtml = true)
        {
            SmtpClient smtpClient = new SmtpClient();
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;//指定电子邮件发送方式        
            smtpClient.Host = _SYS_EMAIL_SRV; //指定SMTP服务器        
            smtpClient.Credentials = new System.Net.NetworkCredential(_SYS_EMAIL_ADDR.Split('@')[0], _SYS_EMAIL_PWD);//用户名和授权码
            // 发送邮件设置        
            MailMessage mailMessage = new MailMessage(_SYS_EMAIL_ADDR, to); // 发送人和收件人      
            mailMessage.Subject = _SYS_EMAIL_DISPNAME+" "+subject;//主题        
            mailMessage.Body = body;
            mailMessage.BodyEncoding = Encoding.UTF8;//正文编码        
            mailMessage.IsBodyHtml = isHtml;//设置为HTML格式        
            mailMessage.Priority = MailPriority.Low;//优先级
            smtpClient.Send(mailMessage);
            //注意：一定要先设置 EnableSsl和UseDefaultCredentials，再实例化Credentials
        }
    }
}
