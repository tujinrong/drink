using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Linq;

using SafeNeeds.DySmat.DB.Util;
using SafeNeeds.DySmat.Util;
//using SafeNeeds.SMAT.Core.Define;
using SafeNeeds.DySmat.DB;

using SafeNeeds.DySmat.Model;

namespace SafeNeeds.DySmat
{
    /// <summary>
    /// 単一のテーブルの処理を管理するクラス
    /// </summary>
    public partial class EntityAdapter
    {



        /// <summary>
        /// データセットの取得
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <param name="select">取得する項目の配列</param>
        /// <param name="filter">フィルター</param>
        /// <param name="orderBy">ソート文</param>
        //public virtual void FillDataSet(DataSet ds, string select, string filter, string orderBy)
        //{
        //    this.FillDataSet(ds, select, filter, this.GetSQLOrderBy(orderBy), 0, 0, false);
        //}


        /// <summary>
        /// データセットの取得
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <param name="select">取得する項目の配列</param>
        /// <param name="filter">フィルター</param>
        /// <param name="orderBy">ソート文</param>
        /// <param name="rowFrom">開始行番号</param>
        /// <param name="rowTo">終了行番号</param>
        //public virtual void FillDataSet(DataSet ds, string select, string filter, string orderBy, int rowFrom, int rowTo)
        //{
        //    this.FillDataSet(ds, select, filter, orderBy, rowFrom, rowTo, false);
        //}

      


        /// <summary>
        /// キーの値を条件に、データセットを取得する
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <param name="selectItems">抽出する項目</param>
        /// <param name="strOrderBy">ソート文</param>
        /// <param name="objKeys">キー</param>
        //public virtual void FillDataByKey(DataSet ds, string selectItems, string strOrderBy, params object[] objKeys)
        //{
        //    FillDataSet(ds, selectItems, this.GetSQLFromKey(objKeys), this.GetSQLOrderBy(strOrderBy));
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="selectitems"></param>
        /// <param name="keyValues"></param>
        //public virtual void FillDataByKeyArray(System.Data.DataSet ds, string selectitems, List<object> keyValues)
        //{
        //    if (keyValues.Count < 3)
        //    {
        //        FillDataSet(ds, selectitems, this.GetSQLFromKeyArray(keyValues));
        //    }
        //    else
        //    {
        //        List<object> al2 = new List<object>();
        //        for (int i = 0; i <= keyValues.Count - 1; i++)
        //        {
        //            al2.Add(keyValues[i]);
        //            if ((i + 1) % MAX_OR == 0 || i == keyValues.Count - 1)
        //            {
        //                Type t = ds.GetType();
        //                DataSet tmpDs = ((DataSet)(Activator.CreateInstance(t)));
        //                FillDataSet(tmpDs, selectitems, this.GetSQLFromKeyArray(al2));
        //                ds.Merge(tmpDs);
        //                al2 = new List<object>();
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 複数行のキーを指定して、データセットを取得する
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <param name="selectitems">抽出する項目</param>
        /// <param name="keyValues">キー値の配列</param>
        //public virtual void FillDataByKeysArray(System.Data.DataSet ds, string selectitems, List<object[]> keyValues)
        //{
        //    if (keyValues.Count < 3)
        //    {
        //        FillDataSet(ds, selectitems, this.GetSQLFromKeysArray(keyValues));
        //    }
        //    else
        //    {
        //        List<object[]> al2 = new List<object[]>();
        //        for (int i = 0; i <= keyValues.Count - 1; i++)
        //        {
        //            al2.Add(keyValues[i]);
        //            if ((i + 1) % MAX_OR == 0 || i == keyValues.Count - 1)
        //            {
        //                Type t = ds.GetType();
        //                DataSet tmpDs = ((DataSet)(Activator.CreateInstance(t)));
        //                FillDataSet(tmpDs, selectitems, this.GetSQLFromKeysArray(al2));
        //                ds.Merge(tmpDs);
        //                al2 = new List<object[]>();
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// 項目指定により、データセットを取得する
        /// </summary>
        /// <param name="ds">データセット</param>
        /// <param name="selectItems">抽出項目</param>
        /// <param name="item"></param>
        /// <param name="value"></param>
        //public virtual void FillDataByItem(System.Data.DataSet ds, string selectItems, string item, object value)
        //{
        //    FillDataSet(ds, selectItems, this.GetSQLFromItem(new string[] { item }, new object[] { value }));
        //}





        /// <summary>
        /// 
        /// </summary>
        /// <param name="keyValues"></param>
        /// <returns></returns>
        //private string GetSQLFromKeysArray(List<object[]> keyValues)
        //{
        //    List<Y_EntityField> flds = null;    // _TableDefine.Fields;
        //    return this.GetSQLFromItemsArray(GetKeys(), keyValues, "");
        //}

        //private string GetSQLFromKeyArray(List<object> keyValues)
        //{
        //    //FieldDefs flds = _TableDefine.Fields;
        //    return this.GetSQLFromItemArray(GetKeys(), keyValues, "");
        //}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        //private string GetSQLFromItem(string[] item, object[] value)
        //{
        //    FieldDefs flds = _TableDefine.Fields;
        //    return Common.GetWhere(flds, item, value, this.Config.GetSQLConfig());
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="value"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        //private string GetSQLFromItemsArray(string[] item, List<object[]> value, string filter)
        //{
        //    FieldDefs flds = _TableDefine.Fields;
        //    List<string> alOr = new List<string>();
        //    foreach (object o in value)
        //    {
        //        if (o is object[])
        //        {
        //            alOr.Add(Common.GetWhere(flds, item, ((object[])(o)), this.Config.GetSQLConfig()));
        //        }
        //        else
        //        {
        //            alOr.Add(Common.GetWhere(flds, item, new object[] { o }, this.Config.GetSQLConfig()));
        //        }
        //    }
        //    string sql = string.Join(DBOption.SqlCrLf + " OR ", (string[])alOr.ToArray());
        //    if (DataUtil.CStr(filter) != "")
        //    {
        //        sql = "(" + sql + ") AND (" + filter + ")";
        //    }
        //    return sql;
        //}

        //private string GetSQLFromItemArray(string[] item, List<object> value, string filter)
        //{
        //    FieldDefs flds = _TableDefine.Fields;
        //    List<string> alOr = new List<string>();
        //    foreach (object o in value)
        //    {
        //        alOr.Add(Common.GetWhere(flds, item, new object[] { o }, this.Config.GetSQLConfig()));
        //    }
        //    string sql = string.Join(DBOption.SqlCrLf + " OR ", (string[])alOr.ToArray());
        //    if (filter!= "")
        //    {
        //        sql = "(" + sql + ") AND (" + filter + ")";
        //    }
        //    return sql;
        //}

        //private string GetSQLOrderBy(string i_orderBy)
        //{
        //    if (i_orderBy != "") return i_orderBy;

        //    FieldDefs f = this._TableDefine.Fields.GetKeyFields();
        //    List<string> list = new List<string>();
        //    foreach (FieldDef def in f)
        //    {
        //        list.Add(this._TableDefine.sTableName + "." + def.FieldName);
        //    }

        //    if (list.Count > 0)
        //    {
        //        return string.Join(",", list.ToArray());
        //    }

        //    return i_orderBy;
        //}

      


    }
}
