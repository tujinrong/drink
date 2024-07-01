//*****************************************************************************
// [システム]  
// 
// [機能概要]  
//  
// [作成履歴]　2014/06/25  屠錦栄　初版 
//
// [レビュー]　2014/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Migrations;
using System.Linq;

using System.Data;
using System.Data.SqlClient;



using SafeNeeds.DySmat.Model;
using SafeNeeds.DySmat.Util;
using SafeNeeds.DySmat.DB.Util;
using SafeNeeds.DySmat.DB;


namespace SafeNeeds.DySmat
{
    /// <summary>
    /// 
    /// 関連：　
    /// 一覧表：N1　→１Nは不可
    /// 
    /// 集計：Sum項目は主テーブル
    /// 　　　１N表は不可。
    /// 
    /// </summary>
    public class ViewAdapter : AdapterBase
    {
        //private int _projID;
        //private string _entityName;
        //private string _viewName;
        int PageRows = 50;
        //DynamicContext db;

        Y_Proj _Proj;
        Y_Entity _Entity;
        Y_EntityView _View;

        SqlDataTime _SqlDataTime = new SqlDataTime(DySmat.DB.EnumDatabaseType.SQLSERVER);
        //cache
        //Dictionary<string, Y_EntityView> ViewDic = new Dictionary<string, Y_EntityView>();

        string SQL = string.Empty;

        public ViewAdapter( EntityRequest entityRequest, string entityName, string viewName)
        {
            _entityRequest = entityRequest;

            //_projID = proj;
            //_entityName = entityName;
            //_viewName = viewName;
            //db = context;
            Global.Init(entityRequest.ProjID);
            _Proj = Global.ProjDic[entityRequest.ProjID];
            _Entity=_Proj.EntityList.Find(e=>e.EntityName==entityName);
            _View=_Entity.ViewList.Find(e=>e.ViewName==viewName);
            PageRows = _Proj.PageRows;

            Config = new DySmat.DB.DBConfig(_Proj.DatabaseType, _Proj.ProviderType, _Proj.GetConnectionString(), entityRequest.ProjID);

            if (_View==null)
            {
                throw new ApplicationException(string.Format("Viewが存在しません:{0}", viewName));
            }
            //夏 2016.09.01
            m_DBMachine = new DMNewConnection(Config);

        }

        public ViewAdapter( EntityRequest entityRequest, Y_Entity entity, Y_EntityView view)
        {
            _entityRequest = entityRequest;

            //_projID = proj;
            //_entityName = entityName;
            //_viewName = viewName;
            //db = context;
            Global.Init(entityRequest.ProjID);
            _Proj = Global.ProjDic[entityRequest.ProjID];
            _Entity = entity;
            _View = view;
            PageRows = _Proj.PageRows;

            Config = new DySmat.DB.DBConfig(_Proj.DatabaseType, _Proj.ProviderType, _Proj.GetConnectionString(), entityRequest.ProjID);
            //夏 2016.09.01
            m_DBMachine = new DMNewConnection(Config);
        }

        string GetSql(Dictionary<string, string[]> filters, bool disableOrderBy=false, int startRow =0, int rows=0, bool disableFormat=false)
        {
            //Pathの取得
            List<string> paths = new List<string>();
            foreach (string  fn in filters.Keys)
            {
                Y_EntityFilter f = _Entity.FilterList.Find(e=>e.FilterName==fn);
                if (f==null)
                {
                    continue;
                    //throw new ApplicationException("Filter未定義");
                }
                if (string.IsNullOrEmpty(f.Path) == false)
                {
                    if (string.IsNullOrEmpty(f.Path) == false)
                    {
                        string[] pathTemp = f.Path.Split('|');
                        foreach (string p in pathTemp)
                        {
                            if (paths.Contains(p) == false)
                            {
                                paths.Add(p);
                            }
                        }
                    }
                }

            }

            foreach(Y_EntityViewItem item in _View.ItemList)
            {
                if (string.IsNullOrEmpty(item.Path) == false)
                {
                    string[] pathTemp = item.Path.Split('|');
                    foreach (string p in pathTemp) { 
                        if(paths.Contains(p) == false){
                            paths.Add(p);
                        }
                    }
                }
            }

            //Ralationの作成
            List<Relation> relas = new List<Relation>();

            //paths: main entity first
            List<string> pathsTemp = new List<string>();
            foreach (string p in paths) {
                if (p.Contains(_Entity.EntityName)) {
                    pathsTemp.Add(p);
                }
            }
            foreach (string p in paths)
            {
                if (p.Contains(_Entity.EntityName) == false)
                {
                    pathsTemp.Add(p);
                }
            }
            paths = pathsTemp;

            foreach (string p in paths)
            {
                string[] rela = p.Split('\\');
                foreach(string r in rela)
                {
                    Relation relation = new Relation(_Proj, r);
                    //if (relas.Where(c=> c.RelaName==relation.RelaName).Count()==0)
                    //{
                        relas.Add(relation);
                    //}
                }
            }

            //relationからJoinの作成
            string sjoin = _Entity.EntityName;

            for(int i=0;i<relas.Count;i++)
            {
                sjoin =  sjoin + relas[i].ToSql() ;
                if (i > 0)
                    sjoin = "(" + sjoin + ")";
            }

            //Selectの作成

            string groupby = string.Join(",", _View.ItemList.Where(e => e.Group == "GroupBy").Select(e => GetFormatedSql(e)).ToArray());

            string select;
            if (string.IsNullOrEmpty(groupby))
            {
                List<string> selectList = new List<string>();
                foreach (Y_EntityViewItem item in _View.ItemList)
                {
                    selectList.Add(GetFormatedSql(item) + " AS " + item.ItemName);
                }
                select = string.Join(",", selectList.ToArray());
            }
            else
            {
                List<string> selectList = new List<string>();
                foreach (Y_EntityViewItem item in _View.ItemList)
                {
                    if (string.IsNullOrEmpty(item.Group))
                    {
                        throw new ApplicationException("Group Error in viewItem");
                    }
                    else if (item.GetGroup() == Y_EntityViewItem.EnumGroup.GroupBy)
                    {
                        selectList.Add(GetFormatedSql(item) + " AS " + item.ItemName);
                    }
                    else if (item.GetGroup() == Y_EntityViewItem.EnumGroup.Avg)
                    {
                        selectList.Add(item.Group + "(IsNull(" + GetFormatedSql(item).Replace("DISTINCT","") + ",0)) AS " + item.ItemName);
                    }
                    else
                    {
                        selectList.Add(item.Group + "(" + GetFormatedSql(item) + ") AS " + item.ItemName);
                    }
                }
                select = string.Join(",", selectList.ToArray());


            }

            //Whereの作成
            List<string> whereList= new List<string>();
            List<string> havingList = new List<string>();
            foreach (string fn in filters.Keys)
            {
                string[] values = filters[fn];
                
                if (values != null)
                {
                    Y_EntityFilterControl ec = _Entity.FilterControlList.Find(e => e.FilterControlName == fn);
                    if (ec==null)
                    {
                        continue;
                        //throw new ApplicationException(string.Format("FilterControl not defined {0} in {1}", fn, _Entity.EntityName));
                    }
                    string[] fs = ec.FilterNames.Split(',');
                    for (int i = 0; i < fs.Length; i++)
                    {
                        string f = fs[i];
                        string v = values[i];
                        if (string.IsNullOrEmpty(v) == false)
                        {
                            Y_EntityFilter ef = _Entity.FilterList.Find(e => e.FilterName == f);
                            //has in 
                            var filterSql = ef.FilterSql.Replace(" ", "");
                            if (filterSql.IndexOf("in({0})") > 0)
                            {
                                if (ef.IsHaving)
                                {
                                    havingList.Add(string.Format(ef.FilterSql, DataUtil.CStr(v)));
                                }
                                else 
                                {
                                    whereList.Add(string.Format(ef.FilterSql, DataUtil.CStr(v)));
                                }
                            }
                            else if (filterSql.IndexOf("{1}") > 0 && v.IndexOf(",") > 0)
                            {
                                string[] vs = v.Split(',');
                                for (int vi = 0; vi < vs.Length; vi++)
                                {
                                    vs[vi] = DataUtil.CStr(vs[vi]);
                                }


                                if (ef.IsHaving)
                                {
                                    havingList.Add(string.Format(ef.FilterSql, vs));
                                }
                                else
                                {
                                    whereList.Add(string.Format(ef.FilterSql, vs));
                                }
                            }
                            else
                            {
                                string filterValue = v;
                                if ("{0}" != filterSql)
                                {
                                    filterValue = DataUtil.SQLConvert(v);
                                }
                                if (ef.IsHaving)
                                {
                                    havingList.Add(string.Format(ef.FilterSql, filterValue));
                                }
                                else
                                {
                                    whereList.Add(string.Format(ef.FilterSql, filterValue));
                                }
                            }

                        }
                    }
                }
            }
		
            string swhere = string.Empty;
            if (whereList.Count>0)
            {
                swhere = " WHERE " + string.Join("\n AND ", whereList.ToArray());
            }

            //SQLの作成
            string sql = "SELECT " + select + "\nFROM " + sjoin + swhere;

            List<string> orderbyList = new List<string>();
            if (disableOrderBy == false)
            {
                var orderByQuery = from items in _View.ItemList orderby items.Seq select items;

                foreach (Y_EntityViewItem item in orderByQuery)
                {
                    string orderByStr = GetFormatedSql(item);

                    if (string.IsNullOrEmpty(item.Group) == false && item.GetGroup() != Y_EntityViewItem.EnumGroup.GroupBy)
                    {
                        orderByStr = item.Group + "(" + orderByStr + ")";
                    }

                    if (item.OrderBy > 0)
                    {
                        orderbyList.Add(orderByStr);
                    }
                    else if (item.OrderBy < 0)
                    {
                        orderbyList.Add(orderByStr + " DESC ");
                    }
                }
            }

            string orderby = string.Empty;
            if (orderbyList.Count > 0)
                orderby = " ORDER BY " + string.Join(",", orderbyList.ToArray());

            string shaving = string.Empty;
            

            if (groupby != string.Empty)
            {
                //集計
                groupby = "\nGROUP BY " + groupby;

                //having
                if (havingList.Count > 0)
                {
                    shaving = " HAVING " + string.Join("\n AND ", havingList.ToArray());
                }
            }

            if (rows == 0)
            {

                sql = "SELECT " + select + "\nFROM " + sjoin + swhere + groupby + shaving + orderby;
            }
            else
            {
                if (orderby == string.Empty)
                {

                    string orderByStr = GetFormatedSql(_View.ItemList[0]);

                    if (string.IsNullOrEmpty(_View.ItemList[0].Group) == false && _View.ItemList[0].GetGroup() != Y_EntityViewItem.EnumGroup.GroupBy)
                    {
                        orderByStr = _View.ItemList[0].Group + "(" + orderByStr + ")";
                    }

                    orderbyList.Add(orderByStr);
                    orderby = " ORDER BY " + string.Join(",", orderbyList.ToArray());
                    //throw new ApplicationException("order by not defined");
                }
                sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (" + orderby + " ) as _rowNo, " + select + "\nFROM " + sjoin + swhere + groupby + shaving;
                sql += " ) _t WHERE _t._rowNo>" + startRow + " and _t._rowNo<=" + (startRow + rows).ToString();

            }
            return sql;
        }

        public PageViewResult GetPageView(PageViewRequest req)
        {
            if (req.PageRows == null)
            {
                req.PageRows = PageRows;
            }
            PageViewResult result = new PageViewResult();

            //getViewFilter
            if (this._View.ViewFilterList != null) {
                foreach (Y_EntityViewFilter filter in this._View.ViewFilterList)
                {

                    if (req.FilterDic.ContainsKey(filter.FilterControlName))
                    {
                        continue;
                    }

                    Y_EntityFilter f = _Entity.FilterList.Find(e => e.FilterName == filter.FilterControlName);
                    if (f == null)
                    {
                        continue;
                    }

                    req.FilterDic.Add(filter.FilterControlName, new string[] { "1" });
                }
            }

            try
            {
                result.DataTable = GetData(req.FilterDic, ref result.PageCount, req.GetPageCount, req.PageNo, req.PageRows.Value, req.DisableFormat);
            }
            catch (Exception e)
            {

                throw new ApplicationException(string.Format("Sql Exception  \r\n Message : {0} \r\n view : {1} \r\n SQL:  \r\n     {2}", e.Message, this._View.ViewName, SQL));
            }

            result.SQL = SQL;

            return result;
        }
        public ViewResult GetView(ViewRequest req)
        {
            ViewResult result = new ViewResult();
            int dummy=0;
            result.DataTable = GetData(req.FilterDic, ref dummy ,false, 0, 0);
            return result;
        }

        private string GetFormatedSql(Y_EntityViewItem item) {

            if (item.ItemSql == "Group") {

                item.ItemSql = "\"" + item.ItemSql + "\"";

            }
            else if (item.ItemSql.EndsWith(".Group"))
            {

                item.ItemSql = item.ItemSql.Replace(".Group", ".\"Group\"");

            };

            if (!string.IsNullOrEmpty(item.Format)) {
                if (item.Format == "=Date(YMD)")
                {
                    return _SqlDataTime.ToYMD(item.ItemSql);
                }
                else if (item.Format == "=Date(YM)")
                {
                    return _SqlDataTime.ToYM(item.ItemSql);
                }
                else if (item.Format == "=Date(Year)")
                {
                    return _SqlDataTime.ToYear(item.ItemSql);
                }
            }

            return item.ItemSql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="count"></param>
        /// <param name="getCount"></param>
        /// <param name="page"></param>
        /// <param name="pageRows"></param>
        /// <param name="disableFormat"></param>//廖add --2016/09/20
        /// <returns></returns>
        public DataTable GetData(Dictionary<string, string[]> filter, ref int count, bool getCount = false, int page = 0, int pageRows = 0, bool disableFormat = false)
        {
            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter ;
            DataTable dt = new DataTable();

            if (getCount)
            {
                string sql1 = GetSql(filter, true);
                sql1 = "SELECT COUNT(*) from (" + sql1 + ") as _counttable";
                adapter = new SqlDataAdapter();
                SQL = sql1;
                adapter.SelectCommand = new SqlCommand(sql1, connection);
                DataTable dt1 = new DataTable();
                adapter.Fill(dt1);
                count = (int)dt1.Rows[0][0];

                if (page == 0) page = 1;
                string sql = GetSql(filter, false, (page - 1) * pageRows, pageRows, disableFormat);
                adapter = new SqlDataAdapter();
                SQL = sql;
                adapter.SelectCommand = new SqlCommand(sql, connection);
                adapter.Fill(dt);
            }
            else
            {
                string sql = GetSql(filter, false, 0, 0, disableFormat);
                adapter = new SqlDataAdapter();
                SQL = sql;
                adapter.SelectCommand = new SqlCommand(sql, connection);
                adapter.Fill(dt);
            }

            int index = 0;

            if (dt.Columns[0].ColumnName == "_rowNo")
            {
                index = 1;
                dt.Columns[0].ColumnName = "No";
            }

           string[] entityNames = _View.ItemList.Select(o => o.ItemEntityName).Distinct().ToArray();  //  获取查询关联的表格名称
           Dictionary<string, Y_EntityField> dicEnField = new Dictionary<string, Y_EntityField>(); 
           foreach (string entityName in entityNames)
           {
               EntityAdapter ea = new EntityAdapter(_entityRequest, entityName);
               List<Y_EntityField> listEF = ea._Entity.FieldList.Where(f => f.IsEncryption == true && f.EntityName == entityName).ToList();
                if(listEF==null||listEF.Count==0)
                {
                    continue;
                }
                foreach (Y_EntityField ef in listEF)
                {
                    dicEnField.Add(entityName+"."+ef.FieldName, ef);
                }
                
           }

            for (int i=0;i< _View.ItemList.Count; i++)
            {
                Y_EntityViewItem item = _View.ItemList[i];
                Y_EntityField field = null;
                if (dicEnField.ContainsKey(item.ItemEntityName+"."+item.ItemName))
                {
                    field = dicEnField[item.ItemEntityName + "." + item.ItemName];
                }

                if (field != null )
                {
                    //解密
                    foreach (DataRow dr in dt.Rows)
                    {
                        dr[field.FieldName] = DataUtil.Decrypt(DataUtil.CStr(dr[field.FieldName]), field.EncryptionType);
                    }
                }

                if (disableFormat == false)//廖add --2016/09/20  Format無効
                {
                    if (!string.IsNullOrEmpty(item.Format))
                    {
                        int colIndex = index + i;
                        string format = item.Format;
                        if (format.StartsWith("=Name("))
                        {
                            int p1 = format.IndexOf("(");
                            int p2 = format.IndexOf(")", p1);
                            string kind = format.Substring(p1 + 1, p2 - p1 - 1);
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[colIndex] != System.Convert.DBNull)
                                {
                                    string s = row[colIndex].ToString();
                                    Dictionary<string, string> dic = _Proj.OptionSet[kind];
                                    if (dic.ContainsKey(s))
                                    {
                                        row[colIndex] = dic[s];
                                    }
                                }
                            }
                        }
                        else if (format.StartsWith("=Date("))
                        {

                        }
                        else if ((format.IndexOf('{') >= 0))
                        {
                            //if (format.IndexOf('{') < 0)
                            //{
                            //    throw new ApplicationException("format error");
                            //}

                            int src = colIndex, obj = colIndex;
                            if (dt.Columns[src].DataType.ToString().EndsWith("String") == false)
                            {
                                dt.Columns.Add("newCol");
                                obj = dt.Columns.Count - 1;

                                //set old DataType to Caption.  for data export format  
                                dt.Columns[obj].Caption = "beforeType:" + dt.Columns[src].DataType.ToString();
                            }
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[src] != System.Convert.DBNull)
                                {
                                    string s = string.Format(format, row[src]);
                                    row[obj] = s;
                                }
                            }
                            if (obj != src)
                            {
                                DataTable dt2 = new DataTable();
                                for (int j = 0; j < dt.Columns.Count - 1; j++)
                                {
                                    DataColumn dc;

                                    if (j == src)
                                    {
                                        dc = dt.Columns[dt.Columns.Count - 1];
                                    }
                                    else
                                    {
                                        dc = dt.Columns[j];
                                    }
                                    dt2.Columns.Add(dt.Columns[j].ColumnName, dc.DataType);
                                    dt2.Columns[dt2.Columns.Count - 1].Caption = dc.Caption;

                                }
                                foreach (DataRow dr in dt.Rows)
                                {
                                    DataRow dr2 = dt2.NewRow();
                                    for (int j = 0; j < dt.Columns.Count - 1; j++)
                                    {
                                        if (j == src)
                                        {
                                            dr2[j] = dr[dt.Columns.Count - 1];
                                        }
                                        else
                                        {
                                            dr2[j] = dr[j];
                                        }
                                    }
                                    dt2.Rows.Add(dr2);

                                }

                                dt = dt2;
                                //string name = dt.Columns[src].ColumnName;
                                //dt.Columns.RemoveAt(src);
                                //obj--;
                                //dt.Columns[obj].ColumnName = name;

                            }

                        }
                        else
                        {
                            foreach (DataRow row in dt.Rows)
                            {
                                if (row[colIndex] != System.Convert.DBNull)
                                {
                                    row[colIndex] = string.Format(format, row[colIndex]);
                                }
                            }
                        }
                    }
                }else{
                    if (!string.IsNullOrEmpty(item.Format)) {
                        int src = index + i;
                        dt.Columns[src].Caption = "Format:" + item.Format;
                    }
                }
                
                
            }

            return dt;
        }

    }

       

    

}