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
    public class DynamicView
    {
        //private int _projID;
        //private string _entityName;
        //private string _viewName;
        int PageRows = 50;
        DynamicContext db;

        Y_Proj _Proj;
        Y_Entity _Entity;
        Y_EntityView _View;
        //cache
        //Dictionary<string, Y_EntityView> ViewDic = new Dictionary<string, Y_EntityView>();


        public DynamicView(DynamicContext context, int proj, string entityName, string viewName)
        {
            //_projID = proj;
            //_entityName = entityName;
            //_viewName = viewName;
            db = context;
            Global.Init( proj);
            _Proj=Global.ProjDic[proj];
            _Entity=_Proj.EntityList.Find(e=>e.EntityName==entityName);
            _View=_Entity.ViewList.Find(e=>e.ViewName==viewName);
            PageRows = _Proj.PageRows;

            if (_View==null)
            {
                throw new ApplicationException(string.Format("Viewが存在しません:{0}", viewName));
            }
        }

        public DynamicView(DynamicContext context, int proj, Y_Entity entity, Y_EntityView view)
        {
            //_projID = proj;
            //_entityName = entityName;
            //_viewName = viewName;
            db = context;
            Global.Init(proj);
            _Proj = Global.ProjDic[proj];
            _Entity = entity;
            _View = view;
            PageRows = _Proj.PageRows;
        }


        string GetSql(Dictionary<string, string[]> filters, bool disableOrderBy=false, int startRow =0, int rows=0)
        {
            //Pathの取得
            List<string> paths = new List<string>();
            foreach (string  fn in filters.Keys)
            {
                Y_EntityFilter f = _Entity.FilterList.Find(e=>e.FilterName==fn);
                if (f==null)
                {
                    throw new ApplicationException("Filter未定義");

                }
                if (string.IsNullOrEmpty(f.Path) == false)
                {
                    paths.Add(f.Path);
                }

            }

            foreach(Y_EntityViewItem item in _View.ItemList)
            {
                if (string.IsNullOrEmpty(item.Path) == false) 
                {
                    paths.Add(item.Path);
                }

            }

            //Ralationの作成
            List<Relation> relas = new List<Relation>();
            foreach (string p in paths)
            {
                string[] rela = p.Split('\\');
                if (rela.Length <= 1) {
                    continue;
                }
                foreach(string r in rela)
                {
                    Relation relation = new Relation(_Proj, r);
                    if (relas.Where(c=> c.RelaName==relation.RelaName).Count()==0)
                    {
                        relas.Add(relation);
                    }
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
            List<string> selectList=new List<string>();
            foreach (Y_EntityViewItem item in _View.ItemList)
            {
                selectList.Add(item.ItemSql + " AS " + item.ItemName);
            }

            string select = string.Join(",", selectList.ToArray());


            //Whereの作成
            List<string> whereList= new List<string>();
            foreach (string fn in filters.Keys)
            {
                string[] values = filters[fn];
                
                if (values != null)
                {
                    Y_EntityFilterControl ec = _Entity.FilterControlList.Find(e => e.FilterControlName == fn);
                    if (ec==null)
                    {
                        throw new ApplicationException(string.Format("FilterControl not defined {0} in {1}", fn, _Entity.EntityName));
                    }
                    string[] fs = ec.FilterNames.Split(',');
                    for (int i = 0; i < fs.Length; i++)
                    {
                        string f = fs[i];
                        string v = values[i];
                        if (v != string.Empty)
                        {
                            Y_EntityFilter ef = _Entity.FilterList.Find(e => e.FilterName == f);

                            whereList.Add(string.Format(ef.FilterSql, v));
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

            if (disableOrderBy) return sql;

            List<string> orderbyList = new List<string>();
            foreach (Y_EntityViewItem item in _View.ItemList)
            {
                if (item.OrderBy ==1)
                {
                    orderbyList.Add(item.ItemSql);
                }
                else if (item.OrderBy==-1)
                {
                    orderbyList.Add(item.ItemSql + " DESC ");
                }
            }
            string orderby = string.Empty;
            if (orderbyList.Count > 0)
                orderby = " ORDER BY " + string.Join(",", orderbyList.ToArray());

            if (rows==0)
            {
                sql = "SELECT " + select + "\nFROM " + sjoin + swhere + orderby;
            }
            else
            {
                if (orderby ==string.Empty)
                {
                    orderbyList.Add(_View.ItemList[0].ItemSql);
                    orderby = " ORDER BY " + string.Join(",", orderbyList.ToArray());
                    //throw new ApplicationException("order by not defined");
                }
                sql = "SELECT * FROM (SELECT ROW_NUMBER() OVER (" + orderby + " ) as _rowNo, " + select + "\nFROM " + sjoin + swhere;
                sql += " ) _t WHERE _t._rowNo>" + startRow + " and _t._rowNo<=" + (startRow + rows).ToString();

            }
            return sql;
        }

        public PageViewResult GetPageView(PageViewRequest req)
        {
            PageViewResult result = new PageViewResult();
            result.DataTable=GetData(req.FilterDic, ref result.PageCount, req.GetPageCount, req.PageNo, PageRows);

            return result;
        }
        public ViewResult GetView(ViewRequest req)
        {
            ViewResult result = new ViewResult();
            int dummy=0;
            result.DataTable = GetData(req.FilterDic, ref dummy ,false, 0, 0);
            return result;
        }

        public DataTable GetData(Dictionary<string, string[]> filter, ref int count, bool getCount =false, int page=0, int pageRows=0)
        {
            SqlConnection connection = new SqlConnection(db.Database.Connection.ConnectionString);
            SqlDataAdapter adapter ;

            if (getCount)
            {
                string sql1 = GetSql(filter, true);
                sql1 = "SELECT COUNT(*) from (" + sql1 + ") as _counttable";
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(sql1, connection);
                DataTable dt1 = new DataTable();
                adapter.Fill(dt1);
                count =(int) dt1.Rows[0][0];
            }
            if (page == 0) page = 1;
            string sql=GetSql(filter,false, (page-1)*pageRows, pageRows);
            adapter = new SqlDataAdapter();
            adapter.SelectCommand = new SqlCommand(sql, connection);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            if (dt.Columns[0].ColumnName == "_rowNo")
                dt.Columns[0].ColumnName = "No";

            for (int i=0;i< _View.ItemList.Count; i++)
            {
                Y_EntityViewItem item = _View.ItemList[i];
                if (!string.IsNullOrEmpty(item.Format))
                {
                    string format = item.Format;
                    if (format.StartsWith("=Name("))
                    {
                        int p1=format.IndexOf("(");
                        int p2=format.IndexOf(")",p1);
                        string kind = format.Substring(p1 + 1, p2 - p1 - 1);
                        foreach (DataRow row in dt.Rows)
                        {
                            if (row[i] != System.Convert.DBNull)
                            {
                               string s=row[i].ToString();
                               Dictionary<string, string> dic = _Proj.OptionSet[kind];
                               if (dic.ContainsKey(s))
                               {
                                   row[i] = dic[s];
                               }
                             }
                        }
                    }
                    else
                    {
                        if (format.IndexOf('{') < 0)
                        {
                            throw new ApplicationException("format error");
                        }
                        
                            int src = i, obj = i;
                            if (dt.Columns[i].DataType.ToString().EndsWith("String") == false)
                            {
                                dt.Columns.Add("newCol");
                                obj = dt.Columns.Count - 1;
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
                                for (int j = 0; j < dt.Columns.Count-1; j++)
                                {
                                    DataColumn dc;

                                    if (j == src)
                                    {
                                        dc = dt.Columns[dt.Columns.Count-1];
                                    }
                                    else
                                    {
                                        dc=dt.Columns[j];
                                    }
                                    dt2.Columns.Add(dt.Columns[j].ColumnName, dc.DataType);
                                    
                                }
                                foreach(DataRow dr in dt.Rows)
                                {
                                    DataRow dr2 = dt2.NewRow();
                                    for (int j = 0; j < dt.Columns.Count-1; j++)
                                    {
                                        if (j == src)
                                        {
                                            dr2[i] = dr[dt.Columns.Count-1];
                                        }
                                        else
                                        {
                                            dr2[j]=dr[j];
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

                }
            }

            return dt;
        }
    }

    

}