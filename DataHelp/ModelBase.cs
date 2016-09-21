using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelp
{
	[Serializable]
    public class ModelBase
    {
        
        private string _Extend = "";
        /// <summary>
        /// 表后缀(主要用于分表后处理)
        /// </summary>
        public string Extend
        {
            get { return _Extend; }
            set { _Extend = value; }
        }
        

        //记录已赋值的属性的名称
        List<string> list = new List<string>();

        public void SetValue(string name)
        {
            //如果集合中不存在该属性的名称，就将该属性名添加到集合中
            if (!list.Contains(name))
            {
                list.Add(name);
            }
        }

        public string GetValue(int index)
        {
            return list[index];
        }

        public int Count()
        {
            return list.Count;
        }

        public bool Contains(string name)
        {
            return list.Contains(name);
        }
    }
}
