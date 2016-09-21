using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataHelp;

namespace GeneBlood.Model
{
    /// <summary>
    /// T_Roles ʵ����
    /// </summary>
    [Serializable]
    [Key("RoleId")]
    public class T_Roles:ModelBase
    {


        System.Int64 _RoleId;
        /// <summary>
        /// ��������
        /// </summary>
        public System.Int64 RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value;SetValue("RoleId"); }
        }


        System.String _RoleName;
        /// <summary>
        /// ��ɫ����
        /// </summary>
        public System.String RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value;SetValue("RoleName"); }
        }


        System.String _RightsId;
        /// <summary>
        /// Ȩ��Id
        /// </summary>
        public System.String RightsId
        {
            get { return _RightsId; }
            set { _RightsId = value;SetValue("RightsId"); }
        }


        System.DateTime _CreateDate;
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public System.DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value;SetValue("CreateDate"); }
        }
    }
}

