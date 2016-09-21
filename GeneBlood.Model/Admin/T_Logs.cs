using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataHelp;

namespace GeneBlood.Model
{
    /// <summary>
    /// T_Logs  µÃÂ¿‡
    /// </summary>
    [Serializable]
    [Key("Logid")]
    public class T_Logs:ModelBase
    {


        System.Int64 _Logid;
        /// <summary>
        /// Logid
        /// </summary>
        public System.Int64 Logid
        {
            get { return _Logid; }
            set { _Logid = value;SetValue("Logid"); }
        }


        System.String _LogName;
        /// <summary>
        /// LogName
        /// </summary>
        public System.String LogName
        {
            get { return _LogName; }
            set { _LogName = value;SetValue("LogName"); }
        }


        System.String _LogContext;
        /// <summary>
        /// LogContext
        /// </summary>
        public System.String LogContext
        {
            get { return _LogContext; }
            set { _LogContext = value;SetValue("LogContext"); }
        }


        System.String _UserName;
        /// <summary>
        /// UserName
        /// </summary>
        public System.String UserName
        {
            get { return _UserName; }
            set { _UserName = value;SetValue("UserName"); }
        }


        System.String _Ip;
        /// <summary>
        /// Ip
        /// </summary>
        public System.String Ip
        {
            get { return _Ip; }
            set { _Ip = value;SetValue("Ip"); }
        }


        System.String _LogType;
        /// <summary>
        /// LogType
        /// </summary>
        public System.String LogType
        {
            get { return _LogType; }
            set { _LogType = value;SetValue("LogType"); }
        }


        System.DateTime _LogTime;
        /// <summary>
        /// LogTime
        /// </summary>
        public System.DateTime LogTime
        {
            get { return _LogTime; }
            set { _LogTime = value;SetValue("LogTime"); }
        }


        System.String _Address;
        /// <summary>
        /// Address
        /// </summary>
        public System.String Address
        {
            get { return _Address; }
            set { _Address = value;SetValue("Address"); }
        }
    }
}

