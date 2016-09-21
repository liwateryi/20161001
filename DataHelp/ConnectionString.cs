using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHelp
{
    public class ConnectionString
    {
        /// <summary>
        ///sqlserver 链接示例:server=.;uid=sa;pwd=123;database=DemoDB;/>
        // mysql 链接示例[必须加上oldsyntax=true，以便支付mysql老语法，参数化命令时会用到@]
        //:server=localhost;uid=root;pwd=root;database=test;CharSet=utf8;oldsyntax=true; 
        /// /// </summary>
        public static string ConnectionStrings
        {
            get
            {
                return System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }
        }

    }
}
