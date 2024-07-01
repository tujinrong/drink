using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.Model
{
    public class DeliveryRouteStorageModel
    {
        /// <summary>
        /// 店舗名
        /// </summary>
        public string shopName;

        /// <summary>
        /// 出庫日
        /// </summary>
        public string outdate;

        /// <summary>
        /// 担当者
        /// </summary>
        public string masterName;

        /// <summary>
        /// route
        /// </summary>
        public string route;

        /// <summary>
        /// date
        /// </summary>
        public string date;

        /// <summary>
        /// page
        /// </summary>
        public string page;

        /// <summary>
        /// 明细
        /// </summary>
        public List<DeliveryRouteStorageItem> details;

        /// <summary>
        /// 
        /// </summary>
        public DeliveryRouteStorageModel()
        {
            details = new List<DeliveryRouteStorageItem>();
        }
    }

    public class DeliveryRouteStorageItem
    {
        /// <summary>
        /// 商品コード
        /// </summary>
        public string productCode;

        /// <summary>
        /// 商品名
        /// </summary>
        public string productName;

        /// <summary>
        /// 実数
        /// </summary>
        public string realNum;

        /// <summary>
        /// ケース数
        /// </summary>
        public string caseNum;

        /// <summary>
        /// 端数
        /// </summary>
        public string fractionNum;
    }
}
