using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using MySql.Data.MySqlClient;

namespace DataHelp
{
    public class MySqlDALBase
    {
        public MySqlDALBase() { }
        public MySqlHelp sqlHelp = new MySqlHelp();
        public MySqlDALBase(string connectionString)
        {
            sqlHelp.ConnectionString = connectionString;
        }
        public virtual bool Insert(ModelBase model)
        {
            if (model.Count() == 0)
            {
                throw new Exception("无任何数据进行添加，无法执行添加操作");
            }

            Type type = model.GetType();
            StringBuilder sql1 = new StringBuilder();
            StringBuilder sql2 = new StringBuilder();
            MySqlManager manager = new MySqlManager();
            for (int i = 0; i < model.Count(); i++)
            {
                //取出已赋值的属性的名称
                string name = model.GetValue(i);
                sql1.Append(name + ",");
                sql2.Append("@" + name + ",");
                //根据属性名获取属性对象
                PropertyInfo pinfo = type.GetProperty(name);
                object value = pinfo.GetValue(model, null);
                manager.Add("@" + name, value);
            }
            sql1.Remove(sql1.Length - 1, 1);
            sql2.Remove(sql2.Length - 1, 1);
            string sql = string.Format("insert into {0}({1}) values({2})", type.Name + model.Extend, sql1, sql2);
            manager.Sql = sql;
            int count = sqlHelp.ExecuteNonQuery(manager);
            return count == 1;
        }

        public virtual int InsertToID(ModelBase model)
        {
            if (model.Count() == 0)
            {
                throw new Exception("无任何数据进行添加，无法执行添加操作");
            }

            Type type = model.GetType();
            StringBuilder sql1 = new StringBuilder();
            StringBuilder sql2 = new StringBuilder();
            MySqlManager manager = new MySqlManager();
            for (int i = 0; i < model.Count(); i++)
            {
                //取出已赋值的属性的名称
                string name = model.GetValue(i);
                sql1.Append(name + ",");
                sql2.Append("@" + name + ",");
                //根据属性名获取属性对象
                PropertyInfo pinfo = type.GetProperty(name);
                object value = pinfo.GetValue(model, null);
                manager.Add("@" + name, value);
            }
            sql1.Remove(sql1.Length - 1, 1);
            sql2.Remove(sql2.Length - 1, 1);
            string sql = string.Format("insert into {0}({1}) values({2});select @@identity", type.Name + model.Extend, sql1, sql2);
            manager.Sql = sql;
            int id = int.Parse(sqlHelp.ExecuteScalar(manager).ToString());
            return id;
        }

        public virtual bool Delete(ModelBase model)
        {
            //验证是否有主键（实体类上是否表示了主键特征）
            Type type = model.GetType();
            string keyName = ValidateKey(model);

            string sql = string.Format("delete {0} where {1}=@{1}", type.Name + model.Extend, keyName);
            MySqlManager manager = new MySqlManager(sql);
            PropertyInfo pinfo = type.GetProperty(keyName);
            object value = pinfo.GetValue(model, null);
            manager.Add("@" + keyName, value);

            int count = sqlHelp.ExecuteNonQuery(manager);
            return count == 1;
        }

        private string ValidateKey(ModelBase model)
        {
            Type type = model.GetType();
            object[] objAtt = type.GetCustomAttributes(typeof(KeyAttribute), false);
            if (objAtt.Length == 0)
            {
                //如果一个实体类没有key特征，就表示这个实体类对应的表没有主键
                throw new Exception("没有主键，无法执行操作");
            }
            KeyAttribute keyAtt = objAtt[0] as KeyAttribute;
            string keyName = keyAtt.KeyName;
            if (!model.Contains(keyName))
            {
                throw new Exception("主键没有赋值，无法执行操作");
            }
            return keyName;
        }

        private string GetPrimaryKey(ModelBase model)
        {
            Type type = model.GetType();
            object[] objAtt = type.GetCustomAttributes(typeof(KeyAttribute), false);
            if (objAtt.Length == 0)
            {
                //如果一个实体类没有key特征，就表示这个实体类对应的表没有主键
                throw new Exception("没有主键，无法执行操作");
            }
            KeyAttribute keyAtt = objAtt[0] as KeyAttribute;
            return keyAtt.KeyName;
        }

        public virtual bool Update(ModelBase model)
        {
            //主键验证
            string keyName = ValidateKey(model);
            //除主键外至少还有一个属性赋值
            if (model.Count() < 2)
            {
                throw new Exception("没有给主键以外的其他列赋值，无法执行修改操作");
            }
            Type type = model.GetType();
            StringBuilder sql1 = new StringBuilder();
            MySqlManager manager = new MySqlManager();
            for (int i = 0; i < model.Count(); i++)
            {
                //取出已赋值的属性名称
                string name = model.GetValue(i);
                if (name != keyName)//除主键外其他的属性应该拼凑到Sql中
                {
                    sql1.Append(name + "=@" + name + ",");
                    //根据属性名获取属性对象，再取出属性值
                    PropertyInfo pinfo = type.GetProperty(name);
                    object value = pinfo.GetValue(model, null);
                    manager.Add("@" + name, value);
                }
            }
            sql1.Remove(sql1.Length - 1, 1);
            string sql = string.Format("update {0} set {1} where {2}=@{2}", type.Name + model.Extend, sql1, keyName);
            PropertyInfo keyPInfo = type.GetProperty(keyName);
            object keyValue = keyPInfo.GetValue(model, null);
            manager.Add("@" + keyName, keyValue);
            manager.Sql = sql;

            int count = sqlHelp.ExecuteNonQuery(manager);
            return count == 1;
        }

        public virtual void SelectForID(ModelBase model)
        {
            string keyName = ValidateKey(model);
            Type type = model.GetType();
            string sql = string.Format("select * from {0} where {1}=@{1}", type.Name + model.Extend, keyName);
            MySqlManager manager = new MySqlManager(sql);
            PropertyInfo pinfo = type.GetProperty(keyName);
            object keyValue = pinfo.GetValue(model, null);
            manager.Add("@" + keyName, keyValue);
            using (MySqlDataReader read = sqlHelp.ExecuteReader(manager))
            {
                if (read.Read())
                {
                    //取出实体类中所有的属性名
                    PropertyInfo[] pinfos = type.GetProperties();
                    //根据属性名取出查询结果中的值
                    foreach (PropertyInfo item in pinfos)
                    {
                        string name = item.Name;
                        //验证该属性名在查询结果中
                        //read.fieldCount表示查询结果中的列数
                        for (int i = 0; i < read.FieldCount; i++)
                        {
                            //根据列的索引取出列的名称
                            string colName = read.GetName(i);
                            if (name.ToLower() == colName.ToLower())//属性名与查询结果的列名匹配
                            {
                                object value = read[name];
                                if (value != DBNull.Value)
                                {
                                    item.SetValue(model, value, null);
                                }
                                break;
                            }
                        }
                    }
                }
                else
                { throw new Exception("无法根据ID查询数据"); }
            }
        }
        
        /// <summary>
        /// 查询数据返回实体集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public virtual List<T> SelectAll<T>() where T : ModelBase
        {
            Type type = typeof(T);
            string sql = string.Format("select * from {0}", type.Name);
            MySqlManager manager = new MySqlManager(sql);
            return Select<T>(manager);
        }

        /// <summary>
        /// 查询数据返回实体集合
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="tableExtend">数据表扩展名</param>
        /// <returns></returns>
        public virtual List<T> SelectAll<T>(string tableExtend) where T : ModelBase
        {
            Type type = typeof(T);
            string sql = string.Format("select * from {0}", type.Name + tableExtend);
            MySqlManager manager = new MySqlManager(sql);
            return Select<T>(manager);
        }

        protected internal virtual List<T> Select<T>(MySqlManager manager) where T : ModelBase
        {
            Type type = typeof(T);
            using (MySqlDataReader read = sqlHelp.ExecuteReader(manager))
            {
                List<T> list = new List<T>();
                while (read.Read())
                {
                    //根据类型来创建类型的对象
                    T t = Activator.CreateInstance(type) as T;
                    PropertyInfo[] pinfos = type.GetProperties();
                    foreach (PropertyInfo item in pinfos)
                    {
                        string name = item.Name;
                        //验证该属性名在查询结果中
                        //read.fieldCount表示查询结果中的列数
                        for (int i = 0; i < read.FieldCount; i++)
                        {
                            //根据列的索引取出列的名称
                            string colName = read.GetName(i);
                            if (name.ToLower() == colName.ToLower())//属性名与查询结果的列名匹配
                            {
                                object value = read[name];
                                if (value != DBNull.Value)
                                {
                                    item.SetValue(t, value, null);
                                }
                                break;
                            }
                        }
                    }

                    list.Add(t);
                }
                return list;
            }
        }

        /// <summary>
        /// 获取表总记录数
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public long GetCount(ModelBase model)
        {
            Type type = model.GetType();
            string sql = string.Format("select count(1) as num from {0}", type.Name + model.Extend);
            return Convert.ToInt64(sqlHelp.ExecuteScalar(sql));
        }

        /// <summary>
        /// 获取当前所属季度
        /// </summary>
        /// <returns></returns>
        public int GetCurrQuarter()
        {
            return Quarter(DateTime.Now);
        }

        /// <summary>
        /// 根据日期获取所属季度
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public int GetQuarterForDate(DateTime dt)
        {
            return Quarter(dt);
        }


        /// <summary>
        /// 根据日期年份
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string GetYear(DateTime dt)
        {
            return dt.ToString("yyyy");
        }

        /// <summary>
        /// 根据日期计算季度
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private int Quarter(DateTime dt)
        {
            int quarter = 1;
            int m = dt.Month;
            if (m >= 0 && m <= 3)
                quarter = 1;
            else if (m >= 4 && m <= 6)
                quarter = 2;
            else if (m >= 7 && m <= 9)
                quarter = 3;
            else if (m >= 10 && m <= 12)
                quarter = 4;
            return quarter;
        }
        
        /// <summary>
        /// 获取最大标识
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public string GetMaxId(ModelBase model)
        {
            Type type = model.GetType();
            string keyName = GetPrimaryKey(model);
            string sql = string.Format("select max({0}) from {1}", keyName, type.Name + model.Extend);
            return sqlHelp.ExecuteScalar(sql).ToString();
        }
    }
}
