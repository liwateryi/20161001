using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataHelp;

namespace GeneBlood.Model
{
    /// <summary>
    /// T_User 实体类
    /// </summary>
    [Serializable]
    [Key("Id")]
    public class T_User:ModelBase
    {


        System.Int64 _Id;
        /// <summary>
        /// 自增，主键
        /// </summary>
        public System.Int64 Id
        {
            get { return _Id; }
            set { _Id = value;SetValue("Id"); }
        }


        System.String _LoginName;
        /// <summary>
        /// 登录名
        /// </summary>
        public System.String LoginName
        {
            get { return _LoginName; }
            set { _LoginName = value;SetValue("LoginName"); }
        }


        System.String _TrueName;
        /// <summary>
        /// TrueName
        /// </summary>
        public System.String TrueName
        {
            get { return _TrueName; }
            set { _TrueName = value;SetValue("TrueName"); }
        }


        System.String _Pwd;
        /// <summary>
        /// 登录密码
        /// </summary>
        public System.String Pwd
        {
            get { return _Pwd; }
            set { _Pwd = value;SetValue("Pwd"); }
        }


        System.String _RandKey;
        /// <summary>
        /// 密码加密key
        /// </summary>
        public System.String RandKey
        {
            get { return _RandKey; }
            set { _RandKey = value;SetValue("RandKey"); }
        }


        System.String _Contact;
        /// <summary>
        /// 联系人
        /// </summary>
        public System.String Contact
        {
            get { return _Contact; }
            set { _Contact = value;SetValue("Contact"); }
        }


        System.String _QQ;
        /// <summary>
        /// QQ号
        /// </summary>
        public System.String QQ
        {
            get { return _QQ; }
            set { _QQ = value;SetValue("QQ"); }
        }


        System.String _Phone;
        /// <summary>
        /// 手机号
        /// </summary>
        public System.String Phone
        {
            get { return _Phone; }
            set { _Phone = value;SetValue("Phone"); }
        }


        System.String _Email;
        /// <summary>
        /// 邮箱账号
        /// </summary>
        public System.String Email
        {
            get { return _Email; }
            set { _Email = value;SetValue("Email"); }
        }


        System.Int32 _State;
        /// <summary>
        /// 用户状态标识(0:正常,1:禁用)
        /// </summary>
        public System.Int32 State
        {
            get { return _State; }
            set { _State = value;SetValue("State"); }
        }


        System.Int32 _UserType;
        /// <summary>
        /// 用户类型:(0:内部用户,1:外网用户)
        /// </summary>
        public System.Int32 UserType
        {
            get { return _UserType; }
            set { _UserType = value;SetValue("UserType"); }
        }


        System.DateTime _CTime;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CTime
        {
            get { return _CTime; }
            set { _CTime = value;SetValue("CTime"); }
        }


        System.DateTime _UTime;
        /// <summary>
        /// 最后修改时间
        /// </summary>
        public System.DateTime UTime
        {
            get { return _UTime; }
            set { _UTime = value;SetValue("UTime"); }
        }
    }
}

