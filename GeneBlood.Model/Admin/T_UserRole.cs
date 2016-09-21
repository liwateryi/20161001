using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataHelp;

namespace GeneBlood.Model
{
    /// <summary>
    /// T_UserRole ʵ����
    /// </summary>
    [Serializable]
    [Key("URId")]
    public class T_UserRole:ModelBase
    {


        System.Int64 _URId;
        /// <summary>
        /// URId
        /// </summary>
        public System.Int64 URId
        {
            get { return _URId; }
            set { _URId = value;SetValue("URId"); }
        }


        System.Int64 _UserId;
        /// <summary>
        /// �û�Id
        /// </summary>
        public System.Int64 UserId
        {
            get { return _UserId; }
            set { _UserId = value;SetValue("UserId"); }
        }


        System.Int64 _RoleId;
        /// <summary>
        /// ��ɫId
        /// </summary>
        public System.Int64 RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value;SetValue("RoleId"); }
        }
    }
}

