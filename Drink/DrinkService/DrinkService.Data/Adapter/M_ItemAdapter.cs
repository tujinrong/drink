//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  。
//
// [作成履歴]　2015/06/25  屠錦栄　初版 
//
// [レビュー]　2015/07/17  屠錦栄　 
//*****************************************************************************

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Migrations;
using SafeNeeds.DySmat;
using DrinkService.Utils;
using System.Data.SqlClient;
using SafeNeeds.DySmat.Util;

namespace DrinkService.Models
{
    /// <summary>
    /// 商品管理
    /// </summary>
    public class M_ItemAdapter : HoEntityAdapterBase
    {

        public M_ItemAdapter(EntityRequest request)
            : base(request, typeof(M_Item).Name)
        {

        }

        /// <summary>
        /// 削除処理
        /// </summary>
        /// <param name="id"></param>
        public Result Delete(M_Item item)
        {
            TableDeleteRequest dreq = new TableDeleteRequest(item.ItemCD);
            return base.Delete(dreq);
        }

        public Result Save(M_Item _item, bool newMode)
        {
            Result result = new Result();

            string[] keys = { _item.ItemCD };

            if (newMode)
            {
                string[] keyFields = { "ItemCD" };

                if (this.HasData(keyFields, keys))
                {
                    result.Message = "このデータはすでに存在しています。";
                    result.ReturnValue = EnumResult.Error;
                    result.ErrorKey = "key";
                    return result;
                }
            }

            string[] itemNameField = { "ItemName" };
            string[] itemNameValue = { _item.ItemName };
            if (this.UniqueCheck(keys, itemNameField, itemNameValue))
            {
                result.Message = "このデータはすでに存在しています。";
                result.ReturnValue = EnumResult.Error;
                result.ErrorKey = "ItemName";
                return result;
            }

            //string[] shortNameField = { "ShortName" };
            //string[] shortNameValue = { _item.ShortName };
            //if (this.UniqueCheck(keys, shortNameField, shortNameValue))
            //{
            //    result.Message = "このデータはすでに存在しています。";
            //    result.ReturnValue = EnumResult.Error;
            //    result.ErrorKey = "ShortName";
            //    return result;
            //}

            _item.UpdateUser = this._entityRequest.User;
            _item.UpdateTime = CommonUtils.GetDateTimeNow();
            dbContext.Items.AddOrUpdate(_item);
            dbContext.SaveChanges();
            result.Message = "商品保存完了しました。";
            result.ReturnValue = EnumResult.OK;
            return result;
        }

        internal PageViewResult GetItemRefer(PageViewRequest req)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, typeof(M_Item).Name, Y_EntityViewData.商品参照);

            return view.GetPageView(req);
        }

        internal PageViewResult GetItemList(PageViewRequest req)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, typeof(M_Item).Name, Y_EntityViewData.商品一覧);

            return view.GetPageView(req);
        }

        internal PageViewResult GetItemKitList(PageViewRequest req)
        {
            ViewAdapter view = new ViewAdapter(_entityRequest, typeof(M_ItemKit).Name, Y_EntityViewData.初期キット検索);

            return view.GetPageView(req);
        }

        public PageViewResult StockList(string shopCD, string itemKey, int page, bool getAll, bool getCount)
        {
            PageViewResult result = new PageViewResult();

            SqlConnection connection = new SqlConnection(Config.ConnectionString);
            SqlDataAdapter adapter = new SqlDataAdapter();
            DataTable dt = new DataTable();
            string sql = @"
                select M_Item.ItemCD
                ,M_Item.ItemName
                ,'{0}' AS ShopCD
                ,M_ItemStock.ItemCD as StockItemCD
                ,sum(T_HoClientItem.AfterNum) as AfterNum
                ,M_ItemStock.StockNum
                ,M_ItemStock.StockNum + sum(T_HoClientItem.AfterNum) as SumNum

                from M_Item

                left join M_ItemStock
                on M_Item.ItemCD = M_ItemStock.ItemCD
                and M_ItemStock.ShopCD = '{0}'

                left join T_HoClientItem 
                on M_Item.ItemCD = T_HoClientItem.ItemCD
                and T_HoClientItem.ShopCD = '{0}'

                left join M_Maker 
                on M_Item.MakerCD = M_Maker.MakerCD

                left join M_Client 
                on M_Client.ShopCD = T_HoClientItem.ShopCD
                AND M_Client.ClientCD = T_HoClientItem.ClientCD

                where 
                 (T_HoClientItem.Seq is null or T_HoClientItem.Seq = M_Client.LastSeq)
                 {1}
 
                 group by 
                 M_Item.ItemCD
                ,M_Item.ItemName
                ,M_ItemStock.ItemCD
                ,M_ItemStock.StockNum
                ,  M_Maker.MakerNameKana
                , M_Maker.MakerName
                , M_Item.SaleStartDay
        ";
            if (!getAll) {
                sql += " having sum(T_HoClientItem.AfterNum) >0 or M_ItemStock.StockNum > 0";
            }

            //sql += " order by  M_Maker.MakerNameKana ,M_Maker.MakerName ,M_Item.SaleStartDay DESC";
             

            string where = "";
            if (!DataUtil.IsNullOrEmpty(itemKey))
            {
                where += " and (M_Item.ItemCD  like  N'" + itemKey + "%' or M_Item.ItemName like  N'%" + itemKey + "%')";
            }

            sql = String.Format(sql, shopCD, where);

            if (getCount)
            {
                //count 
                string sqlCount = "SELECT COUNT(*) from (" + sql + ") as _counttable";
                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(sqlCount, connection);
                adapter.SelectCommand.CommandTimeout = 300;
                dt = new DataTable();
                adapter.Fill(dt);
                int count = (int)dt.Rows[0][0];

                if (count > 0)
                {
                    int pageRows = _Proj.PageRows;
                    int startRow = (page - 1) * pageRows;
                    sql = "SELECT * FROM (" + sql.Replace("select M_Item.ItemCD", "select ROW_NUMBER() OVER (ORDER BY M_Maker.MakerNameKana, M_Maker.MakerName, M_Item.SaleStartDay DESC ) as _rowNo,M_Item.ItemCD");
                    sql += " ) _t WHERE _t._rowNo>" + startRow + " and _t._rowNo<=" + (startRow + pageRows).ToString();

                    adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(sql, connection);
                    adapter.SelectCommand.CommandTimeout = 300;
                    dt = new DataTable();
                    adapter.Fill(dt);

                    result.DataTable = dt;
                }
                else {
                    result.DataTable = new DataTable();
                }
                result.PageCount = count;
            }
            else 
            {
                sql = "SELECT * FROM (" + sql.Replace("select M_Item.ItemCD", "select ROW_NUMBER() OVER (ORDER BY M_Maker.MakerNameKana, M_Maker.MakerName, M_Item.SaleStartDay DESC ) as _rowNo,M_Item.ItemCD");
                sql += " ) _t";

                adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(sql, connection);
                adapter.SelectCommand.CommandTimeout = 300;
                dt = new DataTable();
                adapter.Fill(dt);

                result.DataTable = dt;
            }
            connection.Close();
            return result;
        }


        public Result SaveItemStock(List<M_ItemStock> saveDatas, List<M_ItemStock> delDatas)
        {
            Result result = new Result();

            //delete
            if (delDatas != null) {
                foreach (M_ItemStock item in delDatas)
                {
                    dbContext.ItemStocks.RemoveRange(dbContext.ItemStocks.Where(e => e.ShopCD == item.ShopCD && e.ItemCD == item.ItemCD));
                }
            }

            //save
            if (saveDatas != null)
            {
                foreach (M_ItemStock item in saveDatas)
                {
                    dbContext.ItemStocks.AddOrUpdate(item);
                }
            }
            
            dbContext.SaveChanges();
            result.Message = "商品在庫保存完了しました。";
            result.ReturnValue = EnumResult.OK;
            return result;
        }
    }
}