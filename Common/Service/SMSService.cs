using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Top.Api;
using Top.Api.Request;
using Top.Api.Response;

namespace Common.Service
{
    public class SMSService
    {
        private static readonly string APIURL = "http://gw.api.taobao.com/router/rest?";
        private static readonly string APPKEY = "23463322";
        private static readonly string SECRET = "ec5c4a7707b826793484f7c1e2aaccbf";
        public bool Send(string m, string code, int time, out string errMsg)
        {
            ITopClient client = new DefaultTopClient(APIURL, APPKEY, SECRET);
            AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            req.Extend = m;
            req.SmsType = "normal";
            req.SmsFreeSignName = "启点基因";
            req.SmsParam = "{\"code\":\"" + code + "\",\"product\":\"\",\"time\":\" " + time + "\"}";
            req.RecNum = m;
            req.SmsTemplateCode = "SMS_15870205";
            AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
            string result = rsp.Body;
            errMsg = string.Empty;
            if (rsp.Result != null && rsp.Result.Success)
            {
                return true;
            }
            else
            {
                errMsg = rsp.SubErrMsg;
                return false;
            }
        }
    }
}
