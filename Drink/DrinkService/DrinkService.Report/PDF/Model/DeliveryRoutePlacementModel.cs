using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.Model
{
    public class DeliveryRoutePlacementModel
    {
        /// <summary>
        /// 店舗名
        /// </summary>
        public string shopName;

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
        public List<DeliveryRoutePlacementDetailItem> details;

        public DeliveryRoutePlacementModel()
        {
            details = new List<DeliveryRoutePlacementDetailItem>();
        }
        
    }

    public class DeliveryRoutePlacementDetailItem
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
        /// 配置数
        /// </summary>
        public string placeNum;
    }

    
}
