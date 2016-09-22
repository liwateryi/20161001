using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Common.Service
{
   public class HttpService
    {
        #region 自定义Http 请求

        /// <summary>
        /// 发生post请求
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public static string HttpPost(string Url, string postDataStr)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);

                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// 发生get请求
        /// </summary>
        /// <param name="Url"></param>
        /// <param name="postDataStr"></param>
        /// <returns></returns>
        public static string HttpGet(string Url, string getDataStr)
        {
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (getDataStr == "" ? "" : "?") + getDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=utf-8";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

        #endregion
    }
}
