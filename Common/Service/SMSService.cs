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
        public void Send(string m, string code, int time)
        {
            string url = "http://gw.api.taobao.com/router/rest?";
            string appkey = "23463322";
            string secret = "ec5c4a7707b826793484f7c1e2aaccbf";
            ITopClient client = new DefaultTopClient(url, appkey, secret);
            AlibabaAliqinFcSmsNumSendRequest req = new AlibabaAliqinFcSmsNumSendRequest();
            req.Extend = m;
            req.SmsType = "normal";
            req.SmsFreeSignName = "启点基因";
            req.SmsParam = "{\"code\":\"" + code + "\",\"product\":\"\",\"time\":\" " + time + "\"}";
            req.RecNum = m;
            req.SmsTemplateCode = "SMS_15870205";
            AlibabaAliqinFcSmsNumSendResponse rsp = client.Execute(req);
            string result = rsp.Body;
            /*
             <?xml version="1.0" encoding="utf-8" ?><alibaba_aliqin_fc_sms_num_send_response><result><model>103096727457^0</model><success>true</success></result><request_id>rxvq1sgu4s5g</request_id></alibaba_aliqin_fc_sms_num_send_response><!--top011186097053.eu13-->
             <?xml version="1.0" encoding="utf-8" ?><alibaba_aliqin_fc_sms_num_send_response><result><err_code>0</err_code><model>103096807691^1103942123890</model><success>true</success></result><request_id>3jvmp9ck65dy</request_id></alibaba_aliqin_fc_sms_num_send_response><!--top011250207161.eu13-->
             */
        }
    }
}
