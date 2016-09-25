using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.IO;
using Newtonsoft.Json;

namespace GeneBlood.Web
{
    public class JsonHelper
    {
        /// <summary>
        /// object 序列化成json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string JsonSerializer<T>(T t) where T : class
        {
            try
            {
                return JsonConvert.SerializeObject(t);
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// json 字符串反序列化成object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="jonsText"></param>
        /// <returns></returns>
        public static T JsonDeserialize<T>(string jonsText) where T : class
        {
            try
            {

                return (T)Newtonsoft.Json.JsonConvert.DeserializeObject(jonsText, typeof(T));
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        ///// <summary>
        ///// JSON序列化
        ///// </summary>
        //public static string JsonSerializer<T>(T t)
        //{
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        //    MemoryStream ms = new MemoryStream();
        //    ser.WriteObject(ms, t);
        //    string jsonString = Encoding.UTF8.GetString(ms.ToArray());
        //    ms.Close();
        //    return jsonString;
        //}

        ///// <summary>
        ///// JSON反序列化
        ///// </summary>
        //public static T JsonDeserialize<T>(string jsonString)
        //{
        //    DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
        //    MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
        //    T obj = (T)ser.ReadObject(ms);
        //    return obj;
        //}
    }
}
