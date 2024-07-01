using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.Model
{
    public class DeliveryRouteFillupModel
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


        /// <summary>
        /// 明细
        /// </summary>
        public List<DeliveryRouteFillupDetailItem> details;

        public DeliveryRouteFillupModel() {
            details = new List<DeliveryRouteFillupDetailItem>();
        }
    }

    public class DeliveryRouteFillupDetailItem
    {
        /// <summary>
        /// 顧客
        /// </summary>
        public string customer;

        /// <summary>
        /// 集金額
        /// </summary>
        public string amount;

        /// <summary>
        /// 
        /// </summary>
        public string changePage;
        
        /// <summary>
        /// 顧客明细
        /// </summary>
        public List<DeliveryRouteFillupCustomerDetailItem> customerDetail;

        public DeliveryRouteFillupDetailItem()
        {
            customerDetail=new List<DeliveryRouteFillupCustomerDetailItem>();
        }
    }

    public class DeliveryRouteFillupCustomerDetailItem
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
        /// 単価
        /// </summary>
        public string price;

        /// <summary>
        /// 使用数
        /// </summary>
        public string usedNum;

        /// <summary>
        /// 補充後数
        /// </summary>
        public string fillNum;

        /// <summary>
        /// 金額
        /// </summary>
        public string money;

        /// <summary>
        /// 軽減税率区分
        /// </summary>
        public string taxTypeCD;
    }


    public class DeliveryRouteFillupNotYetModel
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
        public List<DeliveryRouteFillupNotYetDetailItem> details;
    }

    public class DeliveryRouteFillupNotYetDetailItem
    {
        /// <summary>
        /// 担当者
        /// </summary>
        public string masterName;
        /// <summary>
        /// 顧客明细
        /// </summary>
        public List<DeliveryRouteFillupNotYetCustomerDetailItem> customerDetail;
        
    }

    public class DeliveryRouteFillupNotYetCustomerDetailItem
    {
        /// <summary>
        /// 顧客
        /// </summary>
        public string customer;

        /// <summary>
        /// 後日今ｽﾄフラグ
        /// </summary>
        public string afterStopFlag;

        /// <summary>
        /// 後日
        /// </summary>
        public string afterDate;
    }
}
