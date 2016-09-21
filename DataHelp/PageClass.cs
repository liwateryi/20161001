using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace DataHelp
{
    public class PageClass
    {
        /// <summary>
        /// 数据库连接字符串
        /// </summary>
        public string ConnectionString { get; set; }
        string _tableName;

        public string TableName
        {
            get { return _tableName; }
            set { _tableName = value; }
        }
        string _showField = "*";

        public string ShowField
        {
            get { return _showField; }
            set { _showField = value; }
        }
        string _whereText = "";

        public string WhereText
        {
            get { return _whereText; }
            set { _whereText = value; }
        }
        string _orderText = "";

        public string OrderText
        {
            get { return _orderText; }
            set { _orderText = value; }
        }
        int _pageSize = 10;

        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        int _pageIndex = 1;

        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }
        int _dataCount;

        public int DataCount
        {
            get { return _dataCount; }
            set { _dataCount = value; }
        }
        int _pageCount;

        public int PageCount
        {
            get { return _pageCount; }
            set { _pageCount = value; }
        }

        int _TimeOut = 0;

        public int TimeOut
        {
            get { return _TimeOut; }
            set { _TimeOut = value; }
        }

        /// <summary>
        /// 通用分页(适用于Sqlserver数据库)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetPageData<T>() where T : ModelBase
        {
            string sql = "ProcPage";
            SqlManager manager = new SqlManager(sql, System.Data.CommandType.StoredProcedure);
            manager.CommandTimeout = this.TimeOut;
            manager.Add("@tableName", this.TableName);
            manager.Add("@showField", this.ShowField);
            manager.Add("@whereText", this.WhereText.Replace("-", ""));
            manager.Add("@orderText", this.OrderText);
            manager.Add("@pageSize", this.PageSize);
            manager.Add("@pageIndex", this.PageIndex);
            manager.AddOutput("@dataCount", System.Data.SqlDbType.Int);
            DALBase dal = new DALBase();
            dal.sqlHelp.ConnectionString = this.ConnectionString;
            List<T> list = dal.Select<T>(manager);
            this.DataCount = int.Parse(manager[manager.Count - 1].SqlValue.ToString());
            this.PageCount = (DataCount - 1) / PageSize + 1;
            return list;

            #region Sqlserver分页存储过程
            /*
create proc [dbo].[procPage]
@tableName varchar(20),--表名
@showField varchar(100),--要显示的列名
@whereText varchar(500),--where条件(只需要写where后面的语句)
@orderText varchar(500),--排序条件（只需要写order by后面的语句）
@pageSize int,--每一页显示的记录数
@pageIndex int,--当前页
@dataCount int output--总记录数
as
if(len(@whereText)>0)
	set @whereText = ' where '+@whereText
if(len(@orderText)>0)
	set @orderText = ' order by '+@orderText
declare @sql nvarchar(4000)
set @sql = 
'
select * from
(
select row_number() over(order by tempCoulmn) num,* from
(
	select top '+convert(nvarchar(10),@pageIndex*@pageSize)+' '+@showField+',tempCoulmn=0 from '+@tableName+@whereText+@orderText+'
)p1
)p2 where num>'+convert(nvarchar(10),(@pageIndex-1)*@pageSize)
--执行字符串的SQL命令
exec(@sql)
--获取总记录数
set @sql = 'select @dataCount=count(*) from '+@tableName+@whereText
exec sp_executesql @sql,N'@dataCount int output',@dataCount output
            */
            #endregion
        }

        /// <summary>
        /// 通用分页(适用于MySql数据库)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public List<T> GetPageDataForMySql<T>() where T : ModelBase
        {
            string sql = "ProcPage";
            MySqlManager manager = new MySqlManager(sql, System.Data.CommandType.StoredProcedure);
            manager.CommandTimeout = this.TimeOut;
            manager.Add("tableName", this.TableName);
            manager.Add("showField", this.ShowField);
            manager.Add("whereText", this.WhereText.Replace("-", ""));
            manager.Add("orderText", this.OrderText);
            manager.Add("pageSize", this.PageSize);
            manager.Add("pageIndex", this.PageIndex);
            manager.AddOutput("dataCount", MySqlDbType.Int32);
            MySqlDALBase dal = new MySqlDALBase();
            dal.sqlHelp.ConnectionString = this.ConnectionString;
            List<T> list = dal.Select<T>(manager);
            this.DataCount = int.Parse(manager[manager.Count - 1].Value.ToString());
            this.PageCount = (DataCount - 1) / PageSize + 1;
            return list;

            #region MySql分页存储过程
            /*
CREATE PROCEDURE ProcPage(
in tableName varchar(20),#表名
in showField varchar(100),#要显示的列名
in whereText varchar(500),#where条件(只需要写where后面的语句)
in orderText varchar(500),#排序条件（只需要写order by后面的语句）
in pageSize int,#每一页显示的记录数
in pageIndex int,#当前页
out dataCount int#总记录数
)
BEGIN

if (pageSize<1)then
  set pageSize=20;
end if;

if (pageIndex < 1)then
  set pageIndex = 1;
end if;

if(LENGTH(whereText)>0)then
	set whereText=CONCAT(' where 1=1 ',whereText);
end if;

if(LENGTH(orderText)>0)then
	set orderText = CONCAT(' ORDER BY ',orderText);
end if;

set @strsql = CONCAT('select ',showField,' from ',tableName,' ',whereText,' ',orderText,' limit ',pageIndex*pageSize-pageSize,',',pageSize);

prepare stmtsql from @strsql;
execute stmtsql;
deallocate prepare stmtsql;

set @strsqlcount=concat('select count(1) as count into @datacount from ',tableName,'',whereText);
prepare stmtsqlcount from @strsqlcount;
execute stmtsqlcount;
deallocate prepare stmtsqlcount;
set datacount=@datacount;
END;*/
            #endregion
        }
    }
}
