using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataHelp;

namespace GeneBlood.Model
{
    /// <summary>
    /// T_DataDictionary ʵ����
    /// </summary>
    [Serializable]
    [Key("DId")]
    public class T_DataDictionary:ModelBase
    {


        System.Int64 _DId;
        /// <summary>
        /// ����������
        /// </summary>
        public System.Int64 DId
        {
            get { return _DId; }
            set { _DId = value;SetValue("DId"); }
        }


        System.String _DName;
        /// <summary>
        /// DName
        /// </summary>
        public System.String DName
        {
            get { return _DName; }
            set { _DName = value;SetValue("DName"); }
        }


        System.Int64 _DParentId;
        /// <summary>
        /// ���ڵ�Id
        /// </summary>
        public System.Int64 DParentId
        {
            get { return _DParentId; }
            set { _DParentId = value;SetValue("DParentId"); }
        }


        System.String _DParentName;
        /// <summary>
        /// ���ڵ�����
        /// </summary>
        public System.String DParentName
        {
            get { return _DParentName; }
            set { _DParentName = value;SetValue("DParentName"); }
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

