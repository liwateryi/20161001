using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeneBlood.Model;
using DataHelp;
using System.Data.SqlClient;
using System.Data;

namespace GeneBlood.DAL
{
    /// <summary>
    /// T_RolesRight ���ݷ�����
    /// </summary>
    public class T_RolesRightDAL:DALBase
    {
        /// <summary>
        /// ���ݿ������ַ���
        /// </summary>
        public T_RolesRightDAL(string connectionString)
        {
            base.sqlHelp.ConnectionString = connectionString;
        }

        /// <summary>
        /// ���ݽ�ɫidɾ����ɫȨ�޹���
        /// </summary>
        /// <param name="rolesId"></param>
        /// <returns></returns>
        public bool DeleteForRolesId(long rolesId)
        {
            string sql = "delete from dbo.t_rolesright where RolesId =@RolesId";
            SqlManager manager = new SqlManager(sql);
            manager.Add("@RolesId", rolesId);
            return sqlHelp.ExecuteNonQuery(manager) > 0;
        }

        /// <summary>
        /// ����Ȩ��idɾ����ɫȨ�޹���
        /// </summary>
        /// <param name="rolesId"></param>
        /// <returns></returns>
        public bool DeleteForRightId(long rightId)
        {
            string sql = "delete from dbo.t_rolesright where RightId =@RightId";
            SqlManager manager = new SqlManager(sql);
            manager.Add("@RightId", rightId);
            return sqlHelp.ExecuteNonQuery(manager) > 0;
        }
    }
}
