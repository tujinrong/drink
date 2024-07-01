using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.Model
{
    public class DeliveryRouteFillupListModel
    {
        /// <summary>
        /// 店舗名
        /// </summary>
        public string shopName;

        /// <summary>
        /// 補充日
        /// </summary>
        public string fillDate;

        /// <summary>
        /// 担当者
        /// </summary>
        public string masterName;

        /// <summary>
        /// date
        /// </summary>
        public string date;

        /// <summary>
        /// page
        /// </summary>
        public string page;

        public List<DeliveryRouteFillupListDetailItem> details;

        public DeliveryRouteFillupListModel()
        {
            details = new List<DeliveryRouteFillupListDetailItem>();
        }
    }

    public class DeliveryRouteFillupListDetailItem
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
        /// 補充数
        /// </summary>
        public string fillupNum;
    }
}
