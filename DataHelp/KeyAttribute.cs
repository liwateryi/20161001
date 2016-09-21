using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelp
{
    public class KeyAttribute:Attribute
    {
        public KeyAttribute(string keyName)
        {
            this.keyName = keyName;
        }

        string keyName;
        public string KeyName
        {
            get { return keyName; }
            set { keyName = value; }
        }
    }
}
