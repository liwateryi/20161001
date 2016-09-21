using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataHelp;

namespace GeneBlood.Model
{
    /// <summary>
    /// T_Rights ʵ����
    /// </summary>
    [Serializable]
    [Key("Rid")]
    public class T_Rights:ModelBase
    {


        System.Int64 _Rid;
        /// <summary>
        /// ��������
        /// </summary>
        public System.Int64 Rid
        {
            get { return _Rid; }
            set { _Rid = value;SetValue("Rid"); }
        }


        System.String _RName;
        /// <summary>
        /// Ȩ������
        /// </summary>
        public System.String RName
        {
            get { return _RName; }
            set { _RName = value;SetValue("RName"); }
        }


        System.String _Url;
        /// <summary>
        /// Ȩ�޵�ַ
        /// </summary>
        public System.String Url
        {
            get { return _Url; }
            set { _Url = value;SetValue("Url"); }
        }


        System.Int32 _IsDisplay;
        /// <summary>
        /// �Ƿ���Ϊ�˵���ʾ
        /// </summary>
        public System.Int32 IsDisplay
        {
            get { return _IsDisplay; }
            set { _IsDisplay = value;SetValue("IsDisplay"); }
        }


        System.Int64 _ParentId;
        /// <summary>
        /// ��Ȩ��Id
        /// </summary>
        public System.Int64 ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value;SetValue("ParentId"); }
        }


        System.Int32 _NLevel;
        /// <summary>
        /// ��������Ŀ¼
        /// </summary>
        public System.Int32 NLevel
        {
            get { return _NLevel; }
            set { _NLevel = value;SetValue("NLevel"); }
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


        System.Int32 _OrderByNum;
        /// <summary>
        /// ��ʾ����
        /// </summary>
        public System.Int32 OrderByNum
        {
            get { return _OrderByNum; }
            set { _OrderByNum = value;SetValue("OrderByNum"); }
        }
    }
}

