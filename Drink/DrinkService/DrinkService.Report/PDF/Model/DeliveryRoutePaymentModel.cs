using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.Model
{
    public class DeliveryRoutePaymentModel
    {
        /// <summary>
        /// 店舗名
        /// </summary>
        public string shopName;

        /// <summary>
        /// 担当者名
        /// </summary>
        public string masterName;

        /// <summary>
        /// 出力日、時間
        /// </summary>
        public string outputTime;

        /// <summary>
        /// page
        /// </summary>
        public string page;

        /// <summary>
        /// 補充日
        /// </summary>
        public string fillDate;

        /// <summary>
        /// 入金額合計
        /// </summary>
        public string totalAmount;

        /// <summary>
        /// 現金
        /// </summary>
        public string cash;
    }
}
