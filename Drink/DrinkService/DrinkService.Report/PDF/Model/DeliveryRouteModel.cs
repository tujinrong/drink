using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.Model
{
    public class DeliveryRouteModel
    {
        /// <summary>
        /// 会社名
        /// </summary>
        public string kaishaName;

        /// <summary>
        /// 回数
        /// </summary>
        public string times;

        /// <summary>
        /// 補充日
        /// </summary>
        public string addDate;

        /// <summary>
        /// 次回訪問日
        /// </summary>
        public string accessDate;

        /// <summary>
        /// 担当者
        /// </summary>
        public string master;

        /// <summary>
        /// 売上計
        /// </summary>
        public string sale;

        /// <summary>
        /// 集金計
        /// </summary>
        public string collect;

        /// <summary>
        /// 過不足
        /// </summary>
        public string deficiency;

        /// <summary>
        /// Memo
        /// </summary>
        public string memo;

        /// <summary>
        /// page
        /// </summary>
        public string page;

        /// <summary>
        /// date
        /// </summary>
        public string date;

        /// <summary>
        /// signData
        /// </summary>
        public string signData;

        /// <summary>
        /// 明細
        /// </summary>
        public List<DeliveryRouteDetailItem> details;

        public DeliveryRouteModel() {
            details = new List<DeliveryRouteDetailItem>();
        }
    }

    public class DeliveryRouteDetailItem
    {
        /// <summary>
        /// 棚
        /// </summary>
        public string shelf;

        /// <summary>
        /// 順
        /// </summary>
        public string order;

        /// <summary>
        /// 商品名
        /// </summary>
        public string productName;

        /// <summary>
        /// 売価
        /// </summary>
        public string price;

        /// <summary>
        /// 前回補充数
        /// </summary>
        public string preNum;

        /// <summary>
        /// 前回補充後
        /// </summary>
        public string afterPreNum;

        /// <summary>
        /// 現在庫
        /// </summary>
        public string curStorage;

        /// <summary>
        /// 使用
        /// </summary>
        public string used;

        /// <summary>
        /// 補充
        /// </summary>
        public string fill;

        /// <summary>
        /// 補充後
        /// </summary>
        public string afterFill;

        /// <summary>
        /// 賞味期限
        /// </summary>
        public string expirationDate;
    }
}
