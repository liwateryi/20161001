using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataHelp;

namespace GeneBlood.Model
{
    /// <summary>
    /// T_Roles 实体类
    /// </summary>
    [Serializable]
    [Key("RoleId")]
    public class T_Roles:ModelBase
    {


        System.Int64 _RoleId;
        /// <summary>
        /// 自增主键
        /// </summary>
        public System.Int64 RoleId
        {
            get { return _RoleId; }
            set { _RoleId = value;SetValue("RoleId"); }
        }


        System.String _RoleName;
        /// <summary>
        /// 角色名称
        /// </summary>
        public System.String RoleName
        {
            get { return _RoleName; }
            set { _RoleName = value;SetValue("RoleName"); }
        }


        System.String _RightsId;
        /// <summary>
        /// 权限Id
        /// </summary>
        public System.String RightsId
        {
            get { return _RightsId; }
            set { _RightsId = value;SetValue("RightsId"); }
        }


        System.DateTime _CreateDate;
        /// <summary>
        /// 创建时间
        /// </summary>
        public System.DateTime CreateDate
        {
            get { return _CreateDate; }
            set { _CreateDate = value;SetValue("CreateDate"); }
        }
    }
}

