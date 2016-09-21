using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataHelp;

namespace GeneBlood.Model
{
    /// <summary>
    /// T_RolesRight 实体类
    /// </summary>
    [Serializable]
    [Key("RRId")]
    public class T_RolesRight:ModelBase
    {


        System.Int64 _RRId;
        /// <summary>
        /// 自增主键
        /// </summary>
        public System.Int64 RRId
        {
            get { return _RRId; }
            set { _RRId = value;SetValue("RRId"); }
        }


        System.Int64 _RolesId;
        /// <summary>
        /// RolesId
        /// </summary>
        public System.Int64 RolesId
        {
            get { return _RolesId; }
            set { _RolesId = value;SetValue("RolesId"); }
        }


        System.Int64 _RightId;
        /// <summary>
        /// RightId
        /// </summary>
        public System.Int64 RightId
        {
            get { return _RightId; }
            set { _RightId = value;SetValue("RightId"); }
        }
    }
}

