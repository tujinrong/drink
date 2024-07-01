//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  DBContext
//
// [作成履歴]　2015/06/25  屠錦栄　初版 
//
// [レビュー]　2015/07/17  屠錦栄　 
//*****************************************************************************




using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using SafeNeeds.DySmat.Model;
using SafeNeeds.DySmat.Util;
using System;

namespace SafeNeeds.DySmat
{

    public class EntityRequest
    {
        public int ProjID;
        public string User;
        public string Program;


        public EntityRequest(int projID, string user, string program)
        {
            User = user;
            ProjID = projID;
            Program = program;
        }
    }

    public class TableReadRequest : RequestBase
    {
        public object[] Keys;
        public bool ReadSubTables = true;

        public TableReadRequest(params object[] keys)
        {
            Keys = keys;
        }
    }

    public class TableDeleteRequest : RequestBase
    {
        public object[] Keys;
        public bool DeleteSubTables = true;
        public bool CheckRelationTable = true;
        public List<Dictionary<string, object>> SaveData { get; set; }

        public TableDeleteRequest(params object[] keys)
        {
            if (keys == null)
            {
                throw new System.ApplicationException("全件削除防止策");
            }
            Keys = keys;
        }

        public object[] GetRelaFields(string[] keyFields)
        {
            if (keyFields == null)
            {
                return null;
            }

            object[] RelaFields = new object[keyFields.Length];
            Dictionary<string, object> data = SaveData[0];
            for (int index = 0; index < keyFields.Length; index++)
            {
                string fieldName = keyFields[index];
                if (data.ContainsKey(fieldName))
                {
                    RelaFields[index] = data[fieldName];
                }
            }

            return RelaFields;
        }
    }



    public class TableSaveRequest : RequestBase
    {
        public enum EnumSaveMode
        {
            /// <summary>
            /// 差分を保存
            /// </summary>
            SaveChange,

            /// <summary>
            /// 直接保存
            /// </summary>
            SaveBinding
        }
        public enum EnumIsolationMode
        {
            None,

            /// <summary>
            /// 日付を比較し、変わった時には放棄
            /// </summary>
            GiveUpWhenChanged,

            //未対応
            UseGlobal,
            //未対応
            NotAllowEdit,
        }

        //public bool SaveChange = true;
        public bool SaveSubTables = true;

        public EnumSaveMode SaveMode = EnumSaveMode.SaveChange;

        public EnumIsolationMode IsolationMode = EnumIsolationMode.GiveUpWhenChanged;

        public bool UpdateSystemItem = true;
    }

    public class RequestBase
    {

    }

    public class ViewRequest : RequestBase
    {

        public Dictionary<string, string[]> FilterDic = new Dictionary<string, string[]>();
    }

    public class PageViewRequest : ViewRequest
    {
        public int PageNo;
        public int? PageRows;
        public bool GetPageCount;
        public bool DisableFormat;          //廖add --2016/09/20  Format無効
        public bool DisableCrossDataFormat; //廖add --2016/09/20  CrossDataFormat無効
    }

    public class DynamicsViewRequest
    {
        public DynamicsViewRequest()
        {

            this.GetPageCount = true;
        }

        public Dictionary<string, string> FilterDic { get; set; }

        public int ProjID { get; set; }

        public string EntityName { get; set; }

        public bool GetPageCount { get; set; }

        public int GetPageSize { get; set; }

        public string ViewName { get; set; }

        public int pageNumber { get; set; }

        public bool GetSQL { get; set; }

        /// <summary>
        /// 請求元Entity
        /// </summary>
        public string FromEntityName { get; set; }

        /// <summary>
        /// 請求元Form
        /// </summary>
        public string FromFormName { get; set; }

        public List<Y_EntityFilterControl> FilterControlList { get; set; }

        public List<Y_EntityFilter> FilterList { get; set; }

        public Y_EntityView View { get; set; }

        //20160408 liao add  --begin
        /// <summary>
        /// CrossData
        /// </summary>
        public bool GetCrossData { get; set; }

        public bool MultipleVFieldShowHorizontal { get; set; }

        /// <summary>
        /// SeriesData
        /// </summary>
        public bool GetSeriesData { get; set; }

        /// <summary>
        /// CollectOtherSeriesData
        /// </summary>
        public bool CollectOtherSeriesData { get; set; }


        public int GetXDataSize { get; set; }

        /// <summary>
        /// CollectOtherXData
        /// </summary>
        public bool CollectOtherXData { get; set; }
        

        /// <summary>
        /// CollectOtherSeriesData
        /// </summary>
        public int MaxSeriesSize { get; set; }

        //20160408 liao add  --end

        
        //20160907 liao add  --begin
        public string LinkGroupTitle { get; set; }

        public string LinkGroupName { get; set; }
        //20160907 liao add  --end


        public bool DisableCrossDataFormat { get; set; } //廖add --2016/09/20  CrossDataFormat無効

        public Dictionary<string, string[]> GetFilterDic()
        {
            Dictionary<string, string[]> newDic = new Dictionary<string, string[]>();
            if (this.FilterDic != null)
            {
                foreach (string key in this.FilterDic.Keys)
                {
                    //if (this.FilterDic[key] != null) newDic.Add(key, this.FilterDic[key].Split(','));
                    if (this.FilterDic[key] != null) newDic.Add(key, new string[] { this.FilterDic[key] });
                }
            }

            return newDic;
        }
    }

    public class DynamicsSaveRequest
    {
        public DynamicsSaveRequest()
        {
            SaveData = new List<Dictionary<string, object>>();
            DataState = DataState.Modified;
        }

        public int ProjID { get; set; }
        public string UpdateUser { get; set; }
        public string Program { get; set; }
        
        public string EntityName { get; set; }

        /// <summary>
        /// 請求元Entity
        /// </summary>
        public string FromEntityName { get; set; }

        /// <summary>
        /// 請求元Form
        /// </summary>
        public string FromFormName { get; set; }

        public DataState DataState { get; set; }

        public List<Dictionary<string, object>> SaveData { get; set; }

        public DataSet GetDs()
        {

            Global.Init(this.ProjID);
            Y_Proj proj = Global.ProjDic[this.ProjID];

            DataSet ds = new DataSet();
            foreach (Dictionary<string, object> data in SaveData)
            {

                //2016/02/02
                if (data.ContainsKey("DyTableName") == false)
                {
                    continue;
                }
                //2016/02/02

                string tableName = DataUtil.CStr(data["DyTableName"]);

                DataTable dt = null;

                Y_Entity entity = proj.EntityList.Find(e => e.EntityName == tableName);
                List<Y_EntityField> fields = entity.FieldList;

                if (ds.Tables.Contains(tableName) == false)
                {
                    dt = new DataTable();
                    ds.Tables.Add(dt);
                    dt.TableName = tableName;

                    foreach (Y_EntityField p in fields)
                    {
                        System.Collections.Generic.List<Dictionary<string, object>> fieldDatas = SaveData.Where(d => d.ContainsKey("DyTableName") && DataUtil.CStr(d["DyTableName"]) == tableName && d.ContainsKey(p.FieldName)).ToList();

                        if (fieldDatas.Count == 0)
                        {
                            continue;
                        }

                        Type tp;
                        if (p.DataType.ToLower() == "nvarchar") tp = typeof(string);
                        else if (p.DataType.ToLower() == "varchar") tp = typeof(string);
                        else if (p.DataType.ToLower() == "smallint") tp = typeof(int);
                        else if (p.DataType.ToLower() == "int") tp = typeof(int);
                        else if (p.DataType.ToLower() == "number") tp = typeof(decimal);
                        else if (p.DataType.ToLower() == "numeric") tp = typeof(decimal);
                        else if (p.DataType.ToLower() == "decimal") tp = typeof(decimal);
                        else if (p.DataType.ToLower() == "datetime") tp = typeof(DateTime);
                        else if (p.DataType.ToLower() == "date") tp = typeof(DateTime);
                        else if (p.DataType.ToLower() == "bit") tp = typeof(bool);
                        else tp = typeof(string);

                        DataColumn dc = new DataColumn(p.FieldName, tp);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    dt = ds.Tables[tableName];
                }


                DataRow dr = dt.NewRow();
                foreach (KeyValuePair<string, object> p in data)
                {
                    object v = p.Value;
                    if (dt.Columns.Contains(p.Key) == false)
                    {
                        continue;
                    }
                    bool isNullable = fields.Find(f => f.FieldName == p.Key).IsNullable;

                    if (dt.Columns[p.Key].DataType == typeof(string))
                    {

                        dr[p.Key] = DataUtil.CStr(v);
                    }
                    else if (dt.Columns[p.Key].DataType == typeof(int))
                    {
                        if (isNullable && DataUtil.CStr(v) == "")
                        {
                            dr[p.Key] = DBNull.Value;
                        }
                        else
                        {
                            dr[p.Key] = DataUtil.CInt(v);
                        }
                    }
                    else if (dt.Columns[p.Key].DataType == typeof(decimal))
                    {
                        if (isNullable && DataUtil.CStr(v) == "")
                        {
                            dr[p.Key] = DBNull.Value;
                        }
                        else
                        {
                            dr[p.Key] = DataUtil.CDec(v);
                        }
                    }
                    else if (dt.Columns[p.Key].DataType == typeof(DateTime))
                    {
                        if (isNullable && DataUtil.CStr(v) == "")
                        {
                            dr[p.Key] = DBNull.Value;
                        }
                        else
                        {
                            dr[p.Key] = DataUtil.CDate(v);
                        }
                    }
                    else if (dt.Columns[p.Key].DataType == typeof(bool))
                    {
                        if (isNullable && DataUtil.CStr(v) == "")
                        {
                            dr[p.Key] = DBNull.Value;
                        }
                        else
                        {
                            dr[p.Key] = DataUtil.CBool(v);
                        }
                    }
                }

                dt.Rows.Add(dr);
            }

            return ds;
        }

        //2016/02/02
        public Dictionary<string, List<Dictionary<string, object>>> GetDelData()
        {

            Global.Init(this.ProjID);
            Y_Proj proj = Global.ProjDic[this.ProjID];

            Dictionary<string, List<Dictionary<string, object>>> delData = new Dictionary<string, List<Dictionary<string, object>>>();
            List<Dictionary<string, object>> datas = new List<Dictionary<string, object>>();
            foreach (Dictionary<string, object> data in SaveData)
            {

                if (data.ContainsKey("DyDelTableName") == false)
                {
                    continue;
                }

                string tableName = DataUtil.CStr(data["DyDelTableName"]);


                Dictionary<string, object> item = new Dictionary<string, object>();

                Y_Entity entity = proj.EntityList.Find(e => e.EntityName == tableName);
                List<Y_EntityField> fields = entity.FieldList;

                if (delData.ContainsKey(tableName) == false)
                {
                    datas = new List<Dictionary<string, object>>();
                    delData.Add(tableName, datas);
                }
                else
                {
                    datas = delData[tableName];
                }

                foreach (KeyValuePair<string, object> p in data)
                {
                    object v = p.Value;
                    if (p.Key == "DyDelTableName")
                    {
                        continue;
                    }
                    item.Add(p.Key, p.Value);
                }

                datas.Add(item);
            }

            return delData;
        }
        //2016/02/02

        public List<Y_EntityField> GetkeyFields()
        {
            Y_Proj proj = Global.ProjDic[this.ProjID];
            Y_Entity entity = proj.EntityList.Find(e => e.EntityName == EntityName);
            List<Y_EntityField> keyFields = entity.FieldList.Where(e => e.IsKey).OrderBy(e => e.Seq).ToList();

            return keyFields;
        }
    }
    public class DynamicsDsRequest
    {
        public DynamicsDsRequest()
        {
            DsRequests = new List<DsRequestModel>();
        }

        public int ProjID { get; set; }

        public string UpdateUser { get; set; }

        public List<DsRequestModel> DsRequests { get; set; }


    }

    public class DsRequestModel
    {
        public DsRequestModel()
        {
            Select = "*";
            Filter = "";
            OrderBy = "";
        }

        public string TableName { get; set; }

        public string Select { get; set; }

        public string Filter { get; set; }

        public string OrderBy { get; set; }

        public int RowFrom { get; set; }

        public int RowTo { get; set; }

        public bool Distinct { get; set; }
    }

    public class DynamicsDeleteRequest
    {

        public int ProjID { get; set; }

        public string EntityName { get; set; }

        /// <summary>
        /// 請求元Entity
        /// </summary>
        public string FromEntityName { get; set; }

        /// <summary>
        /// 請求元Form
        /// </summary>
        public string FromFormName { get; set; }

        public List<Dictionary<string, object>> SaveData { get; set; }

        public TableDeleteRequest GetDelReq()
        {

            Y_Proj proj = Global.ProjDic[this.ProjID];

            TableDeleteRequest req = new TableDeleteRequest();
            req.SaveData = SaveData;

            Y_Entity entity = proj.EntityList.Find(e => e.EntityName == EntityName);
            List<string> keyFields = entity.FieldList.Where(e => e.IsKey).OrderBy(e => e.Seq).Select(e => e.FieldName).ToList();


            if (SaveData == null || SaveData.Count == 0)
            {
                return null;
            }

            object[] Keys = new object[keyFields.Count];
            int index = 0;
            Dictionary<string, object> data = SaveData[0];
            foreach (string fieldName in keyFields)
            {
                if (data.ContainsKey(fieldName))
                {
                    Keys[index] = data[fieldName];
                    index++;
                }
            }

            req.Keys = Keys;
            return req;
        }

        public List<Y_EntityField> GetkeyFields()
        {
            Y_Proj proj = Global.ProjDic[this.ProjID];
            Y_Entity entity = proj.EntityList.Find(e => e.EntityName == EntityName);
            List<Y_EntityField> keyFields = entity.FieldList.Where(e => e.IsKey).OrderBy(e => e.Seq).ToList();

            return keyFields;
        }
    }

    public enum DataState
    {
        Added = 0,
        Modified = 1
    }
}