//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  DBContext
//
// [作成履歴]　2015/06/25  屠錦栄　初版 
//
// [レビュー]　2015/07/17  屠錦栄　 
//*****************************************************************************


using System;
using System.Data;
using SafeNeeds.DySmat.Model;
using System.Collections.Generic;
using System.Transactions;

using System.Linq;

using SafeNeeds.DySmat;
using SafeNeeds.DySmat.DB;

namespace SafeNeeds.DySmat
{

    /// <summary>
    /// 
    /// </summary>
    public partial class EntityAdapter : AdapterBase
    {

        //private int _projID;
       // private string _entityName;
        public Y_Proj _Proj;
        public Y_Entity _Entity;


        public EntityAdapter()
        {
        }


        public EntityAdapter(EntityRequest entityRequest, string entityName)
        {
            //_entityName = entityName;
            _entityRequest = entityRequest;

            Global.Init(entityRequest.ProjID);
            _Proj = Global.ProjDic[entityRequest.ProjID];
            Config = new DySmat.DB.DBConfig(_Proj.DatabaseType, _Proj.ProviderType, _Proj.GetConnectionString(), entityRequest.ProjID);


            _Entity = _Proj.EntityList.Find(e => e.EntityName == entityName);

            if (_Entity == null)
            {
                throw new System.ApplicationException("Entity Name Error");
            }


            m_DBMachine = new DMNewConnection(Config);
        }

        public EntityAdapter(EntityRequest entityRequest, string entityName, List<Y_EntityFilterControl> FilterControlList, List<Y_EntityFilter> FilterList)
        {

            _entityRequest = entityRequest;

            Global.Init(entityRequest.ProjID);
            _Proj = Global.ProjDic[entityRequest.ProjID];
            Config = new DySmat.DB.DBConfig(_Proj.DatabaseType, _Proj.ProviderType, _Proj.GetConnectionString(), entityRequest.ProjID);


            _Entity = new Y_Entity();
            _Entity.ProjID = entityRequest.ProjID;
            _Entity.EntityName = entityName;
            _Entity.FilterControlList = FilterControlList;
            _Entity.FilterList = FilterList;

            if (_Entity.FilterControlList == null) {
                _Entity.FilterControlList = new List<Y_EntityFilterControl>();
            }

            if (_Entity.FilterList == null)
            {
                _Entity.FilterList = new List<Y_EntityFilter>();
            }

            if (_Entity == null)
            {
                throw new System.ApplicationException("Entity Name Error");
            }


            m_DBMachine = new DMNewConnection(Config);
        }

        /// <summary>
        /// 検索一覧画面
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PageViewResult GetList(PageViewRequest req, string viewName)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, _Entity.EntityName, viewName);

            return view.GetPageView(req);
        }

        /// <summary>
        /// 検索一覧画面
        /// </summary>
        /// <param name="req"></param>
        /// <returns></returns>
        public PageViewResult GetList(PageViewRequest req, Y_EntityView entityView)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, _Entity, entityView);

            return view.GetPageView(req);
        }

        /// <summary>
        /// データ取得
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public Result GetData(TableReadRequest req, ref DataSet data)
        {
            FillDataSetByKey(data, req.Keys);
            Decrypt(data.Tables[0]);    //解密
            if (req.ReadSubTables)
            {
                DataTable dt = data.Tables[0];
                if (dt != null && dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    List<Y_EntityRela1N> subTableEntity = _Entity.Rela1NList.Where(e => e.IsSubTable).ToList();
                    foreach (Y_EntityRela1N r in subTableEntity)
                    {
                        string relaEntityName = r.RelaEntityName;
                        string[] relaFieldNames = r.RelaFieldNames.Split(',');
                        string[] fieldNames = r.FieldNames.Split(',');
                        object[] keys = new object[fieldNames.Length];
                        for (int i = 0; i < keys.Length; i++)
                        {
                            keys[i] = row[fieldNames[i]];
                        }

                        EntityAdapter ea = new EntityAdapter(_entityRequest, relaEntityName);
                        DataSet ds = ea.GetDataSet("*", ea.GetSQLFromItem(relaFieldNames, keys));
                        ea.Decrypt(ds.Tables[relaEntityName]);    //解密
                        data.Tables.Add(ds.Tables[relaEntityName].Copy());
                    }
                }
            }         

            return new Result();
        }


        /// <summary>
        /// 
        /// 保存処理
        /// </summary>
        /// <param name="data"></param>
        public Result SaveData(TableSaveRequest req, DataSet ds)
        {
            //加密数据
            Encrypt(ds.Tables[0]);
            string[] names = GetRela1NName();
            foreach (string name in names)
            {
                EntityAdapter ea = new EntityAdapter(_entityRequest, name);
                ea.Encrypt(ds.Tables[name]);
            }
            
            switch (req.SaveMode)
            {
                case TableSaveRequest.EnumSaveMode.SaveBinding:
                    return SaveData_Binding(req, ds);
                case TableSaveRequest.EnumSaveMode.SaveChange:
                    return SaveData_Change(req, ds, req.UpdateSystemItem);
                default:
                    throw new ApplicationException("");
            }
        }

        /// <summary>
        /// 
        /// 保存処理
        /// </summary>
        /// <param name="data"></param>
        public Result SaveData(TableSaveRequest req, string filter, DataTable ds)
        {

            switch (req.SaveMode)
            {
                case TableSaveRequest.EnumSaveMode.SaveBinding:

                case TableSaveRequest.EnumSaveMode.SaveChange:
                    return SaveData_Change(filter, ds, req.UpdateSystemItem);
                default:
                    throw new ApplicationException("");
            }
        }


        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="id"></param>
        public Result Delete(TableDeleteRequest req)
        {
            //関連性をチェック、関連データが存在すれば、処理中止
            List<Y_EntityRela1N> checkDelEntity = _Entity.Rela1NList.Where(e => e.CheckDelete).ToList <Y_EntityRela1N>();
            foreach (Y_EntityRela1N r in checkDelEntity)
            {
                EntityAdapter ea = new EntityAdapter(_entityRequest, r.RelaEntityName);
                string[] relaFieldNames = r.RelaFieldNames.Split(',');
                string[] fieldNames = r.FieldNames.Split(',');
                object[] keys = req.GetRelaFields(fieldNames);
                if (ea.HasData(relaFieldNames, keys))
                {
                    string entityName = r.RelaEntityName;
                    object data = new object();
                    Y_Entity en = _Proj.EntityList.Find(e => e.EntityName == entityName);
                    if (en != null)
                    {
                        entityName = en.EntityDesc;
                        data = en;
                    }
                    return new Result(EnumResult.CheckDelete, "【" + entityName + "】に関連データが存在する為、削除できません。", data);
                }
            }

            //using (TransactionScope ts = new TransactionScope())
            //{
                //try
                //{
                    //子テーブルを削除
                    List<Y_EntityRela1N> subTableEntity = _Entity.Rela1NList.Where(e => e.IsSubTable).ToList<Y_EntityRela1N>();
                    foreach (Y_EntityRela1N r in subTableEntity)
                    {
                        EntityAdapter ea = new EntityAdapter(_entityRequest, r.RelaEntityName);
                        string[] relaFieldNames = r.RelaFieldNames.Split(',');
                        string[] fieldNames = r.FieldNames.Split(',');
                        object[] keys = req.GetRelaFields(fieldNames);
                        ea.DeleteByRelaKey(relaFieldNames, keys);
                    }

                    //データを削除
                    DeleteByKey(req.Keys);

                    //ts.Complete();
                //}
                //catch (Exception ex)
                //{
                //    return new Result(ex);
                //}

            //}


            return new Result();
        }
    }
}
