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
            REG = 0,
            F_PWD,
            R_PWD,
            U_PWD,
            Pub
        }

        public enum _EMAIL_CODE_
        {
            ERR_ADDR = -1001,
            ERR_SEND = -1002,
            OK_ACK = 0
        }

        public string GetEmailBody(string User, _EM_TYPE_ type, params string[] msg)
        {
            
            string emailbody = "";
            switch (type)
            {
                case _EM_TYPE_.REG:
                    emailbody = "--启点欢迎您--\r\n\r\n";
                    emailbody += "亲爱的 " + User + "，您好！\r\n\r\n";
                    emailbody += "您已成功注册了启点账号，请牢记账号与密码。如果忘记了密码可在启点账号登录界面通过该邮箱找回！";
                    emailbody += "\r\n\r\n祝您使用愉快！";
                    emailbody += "\r\n\r\n--启点";
                    emailbody += "\r\n\r\n此邮件为系统自动发送，请勿回复。";
                    emailbody += "\r\n如果您在使用中遇到问题，请发邮件到Support@lecoo.com";
                    break;
                case _EM_TYPE_.F_PWD:
                    //emailbody = "启点账号密码重置入口：<br/>";
                    //string go_email_url = System.Configuration.ConfigurationManager.AppSettings["go_email_url"];
                    //emailbody += "<a href='" + go_email_url + "?lgName=" + User + "&gurl=" + HttpUtility.UrlEncode(msg[0]) + "' id='a_sendemail'>点击进入</a>";
                    break;
                case _EM_TYPE_.R_PWD:
                    emailbody = "亲爱的 " + User + "，您好！<br/><br/>";
                    emailbody += "您的密码已重置，新密码：<font color='#FF0000'>" + msg[0] + "</font>，请尽快使用新的密码登录启点账号并修改。";
                    emailbody += "<br/><br/>祝您使用愉快！";
                    emailbody += "<br/><br/>--启点";
                    emailbody += "<br/><br/>此邮件为系统自动发送，请勿回复。";
                    emailbody += "<br/>如果您在使用中遇到问题，请发邮件到Support@lecoo.com";
                    break;
                case _EM_TYPE_.U_PWD:
                    emailbody = "亲爱的 " + User + "，您好！\r\n\r\n";
                    emailbody += "您的账号密码已修改，如非本人操作请尽快进入启点登录界面点击[密码找回]。";
                    emailbody += "\r\n\r\n祝您使用愉快！";
                    emailbody += "\r\n\r\n--启点";
                    emailbody += "\r\n\r\n此邮件为系统自动发送，请勿回复。";
                    emailbody += "\r\n如果您在使用中遇到问题，请发邮件到Support@lecoo.com";
                    break;
                case _EM_TYPE_.Pub:
                    emailbody = "testtesttesttesttesttesttesttesttesttest";
                    break;
            }

            return emailbody;
        }

        //private string _SYS_EMAIL_SRV = "smtp.exmail.qq.com";
        //private string _SYS_EMAIL_PORT = "25";
        //private string _SYS_EMAIL_ADDR = "Service@lecoo.com";
        //private string _SYS_EMAIL_PWD = "kf1234567c";
        //private string _SYS_EMAIL_DISPNAME = "启点客服";

        //private string _SYS_EMAIL_SRV = "smtp.163.com";
        //private string _SYS_EMAIL_PORT = "25";
        //private string _SYS_EMAIL_ADDR = "qidian_service_kf@163.com";
        //private string _SYS_EMAIL_PWD = "kf_9876543210";
        //private string _SYS_EMAIL_DISPNAME = "测试";

        //private string _SYS_EMAIL_SRV = "smtp.163.com";
        //private string _SYS_EMAIL_PORT = "25";
        //private string _SYS_EMAIL_ADDR = "liwateryi@163.com";
        //private string _SYS_EMAIL_PWD = "luo_123456";
        //private string _SYS_EMAIL_DISPNAME = "测试";

        private string _SYS_EMAIL_SRV = "smtp.qq.com";
        private string _SYS_EMAIL_PORT = "25";
        private string _SYS_EMAIL_ADDR = "3102310972@qq.com.com";
        private string _SYS_EMAIL_PWD = "czfbmqzqlfbbddic";
        private string _SYS_EMAIL_DISPNAME = "测试";

        public delegate void AsyncSendEmail(string recv_email, string cc, string title, string body, bool ishtml, string files);
        public void CallAsyncSendEmail(string recv_email, string cc, string title, string body, bool ishtml, string files)
        {
            AsyncSendEmail cbSendEmail;
            IAsyncResult arcb_email;
            cbSendEmail = new AsyncSendEmail(sendEmail);
            arcb_email = cbSendEmail.BeginInvoke(recv_email, cc, title, body, ishtml, files, null, null);
        }

        public void sendEmail(string recv_email, string cc, string title, string body, bool ishtml, string files)
        {
            try
            {
                //EMAIL s_email = new MyCls.EMAIL();
                string tmp = "";
                //EMAIL._EMAIL_CODE_ code = s_email.SendEmail(files, title, _SYS_EMAIL_SRV, _SYS_EMAIL_PORT, _SYS_EMAIL_ADDR, _SYS_EMAIL_PWD, _SYS_EMAIL_DISPNAME, cc, recv_email, body, ishtml, ref tmp);
                //bool isTrue = code == EMAIL._EMAIL_CODE_.OK_ACK;

                _EMAIL_CODE_ code = SendEmailBase(files, title, _SYS_EMAIL_SRV, _SYS_EMAIL_PORT, _SYS_EMAIL_ADDR, _SYS_EMAIL_PWD, _SYS_EMAIL_DISPNAME, cc, recv_email, body, ishtml, ref tmp);
            }
            catch (Exception ex)
            {
            }
        }

        public void sendEmail(string recv_email, string cc, string title, string body, bool ishtml, string files, string sender, string s_pwd, string s_srv, string s_dispname)
        {
            try
            {
                EMAIL s_email = new MyCls.EMAIL();
                string tmp = "";
                if (s_email.SendEmail(files, title, _SYS_EMAIL_SRV, _SYS_EMAIL_PORT, sender, s_pwd, s_dispname, cc, recv_email, body, ishtml, ref tmp) == EMAIL._EMAIL_CODE_.OK_ACK)
                {
                    //"发送邮件成功");
                }
            }
            catch (Exception ex)
            {
            }
        }

        //http://www.cnblogs.com/sunyl/p/5490169.html

        private  _EMAIL_CODE_ SendEmailBase(string FilePath, string strSubject, string EmailSrv, string SrvPort, string Sender, string SenderPwd, string dispName, string cc, string Reciver, string FixBodyTest, bool isBodyHtml, ref string retmsg)
        {
            _EMAIL_CODE_ _email_code_ = _EMAIL_CODE_.OK_ACK;
            string host = EmailSrv;
            MailAddress from = new MailAddress(Sender, dispName);
            string[] strArray = Reciver.Split(new char[] { ';' });
            string[] strArray2 = cc.Split(new char[] { ';' });
            if (strArray.Length <= 0)
            {
                retmsg = "邮件地址：" + Reciver.Trim() + " 不符合！";
                return _EMAIL_CODE_.ERR_ADDR;
            }
            MailAddress to = new MailAddress(strArray[0]);
            MailMessage message = new MailMessage(from, to);
            ArrayList list = new ArrayList();
            try
            {
                message.Subject = strSubject;
                message.IsBodyHtml = isBodyHtml;
                message.Body = FixBodyTest;
                for (int j = 0; j < strArray2.Length; j++)
                {
                    if (strArray2[j] != "")
                    {
                        message.CC.Add(strArray2[j]);
                    }
                }
                for (int k = 1; k < strArray.Length; k++)
                {
                    if (strArray[k] != "")
                    {
                        message.To.Add(strArray[k].ToString().Trim());
                    }
                }
                if (FilePath != "")
                {
                    string[] strArray3 = FilePath.Split(new char[] { ';' });
                    for (int m = 0; m < strArray3.Length; m++)
                    {
                        if (strArray3[m].Trim() != "")
                        {
                            Attachment attachment = new Attachment(strArray3[m], "application/octet-stream");
                            list.Add(attachment);
                            ContentDisposition contentDisposition = attachment.ContentDisposition;
                            contentDisposition.CreationDate = File.GetCreationTime(strArray3[m]);
                            contentDisposition.ModificationDate = File.GetLastWriteTime(strArray3[m]);
                            contentDisposition.ReadDate = File.GetLastAccessTime(strArray3[m]);
                            message.Attachments.Add(attachment);
                        }
                    }
                }
                new SmtpClient(host, int.Parse(SrvPort)) { Credentials = new NetworkCredential(Sender, SenderPwd) }.Send(message);
            }
            catch (Exception exception)
            {
                retmsg = retmsg + "\r\n" + exception.Message;
                _email_code_ = _EMAIL_CODE_.ERR_SEND;
            }
            for (int i = 0; i < list.Count; i++)
            {
                try
                {
                    if (((Attachment)list[i]) != null)
                    {
                        ((Attachment)list[i]).Dispose();
                    }
                }
                catch (Exception exception2)
                {
                    throw exception2;
                }
            }
            if (message != null)
            {
                message.Dispose();
            }
            return _email_code_;

        }
    }
}
