using DrinkService.Report.FD.Logic;
using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.FD.Model
{
    public class FDDbShopModel
    {
        public string ShopCD;
        public DateTime HoDate;

        public List<FDDbClientModel> ClientList = new List<FDDbClientModel>();

        public static FDDbShopModel FromDataTable(DataTable dt)
        {
            FDDbShopModel model = new FDDbShopModel();

            foreach (DataRow dr in dt.Rows)
            {
                //店舗
                model.ShopCD = DataUtil.CStr(dr["ShopCD"]);
                model.HoDate = DataUtil.CDate(dr["HoDate"]);

                var ClientCD = DataUtil.CStr(dr["ClientCD"]);
                var TaxTypeCD = DataUtil.CStr(dr["TaxTypeCD"]);

                //顧客  (廖さん、以下は標準作成方法)
                FDDbClientModel client = model.ClientList.Find(e => e.ClientCD == ClientCD);
                if (client == null)
                {
                    client = new FDDbClientModel();
                    model.ClientList.Add(client);

                    client.ShopCD = model.ShopCD;
                    client.HoDate = model.HoDate;
                    client.ClientCD = DataUtil.CStr(dr["ClientCD"]);
                    client.Seq = DataUtil.CInt(dr["Seq"]);

                    client.SoldMoney = DataUtil.CInt(dr["SoldMoney"]);
                    client.GetMoney = DataUtil.CInt(dr["GetMoney"]);
                    client.DiffMoney = DataUtil.CInt(dr["DiffMoney"]);

                    client.SlipNO = DataUtil.CInt(dr["SlipNO"]);
                    client.TransactionType = DataUtil.CStr(dr["TransactionType"]);
                    client.KanriClientCD = DataUtil.CStr(dr["KanriClientCD"]);
                }

                //税区分
                FDDbGroupModel group = client.GroupList.Find(e => e.TaxTypeCD == TaxTypeCD);
                if (group==null)
                {
                    group = new FDDbGroupModel();
                    client.GroupList.Add(group);

                    group.ShopCD = model.ShopCD;
                    group.HoDate = model.HoDate;
                    group.ClientCD = client.ClientCD;
                    group.TaxTypeCD = TaxTypeCD;

                    group.TaxRate = TaxService.GetTax(model.HoDate, group.TaxTypeCD);

                }

                //商品
                FDDbItemModel item = new FDDbItemModel();

                item.ShopCD = group.ShopCD;
                item.ClientCD = group.ClientCD;
                item.TaxTypeCD = group.TaxTypeCD;
                item.TaxRate = group.TaxRate;

                item.ItemCD = DataUtil.CStr(dr["ItemCD"]);
                item.PrevNum = DataUtil.CInt(dr["PrevNum"]);
                item.ThisNum = DataUtil.CInt(dr["ThisNum"]);
                item.AddNum = DataUtil.CInt(dr["AddNum"]);
                item.BeforeNum = DataUtil.CInt(dr["BeforeNum"]);
                item.UsedNum = DataUtil.CInt(dr["UsedNum"]);
                item.AfterNum = DataUtil.CInt(dr["AfterNum"]);
                item.Price = DataUtil.CInt(dr["Price"]);
                item.Money = DataUtil.CInt(dr["Money"]);
                
               
                item.pre_PrevNum = DataUtil.CInt(dr["pre_PrevNum"]);
                item.pre_ThisNum = DataUtil.CInt(dr["pre_ThisNum"]);
                item.pre_AddNum = DataUtil.CInt(dr["pre_AddNum"]);
                item.pre_BeforeNum = DataUtil.CInt(dr["pre_BeforeNum"]);
                item.pre_UsedNum = DataUtil.CInt(dr["pre_UsedNum"]);
                item.pre_AfterNum = DataUtil.CInt(dr["pre_AfterNum"]);
                item.pre_Price = DataUtil.CInt(dr["pre_Price"]);
                item.pre_Money = DataUtil.CInt(dr["pre_Money"]);

                //仕様数ない商品を出力しない
                group.ItemList.Add(item);
            }

            return model;
        }
    }


    public class FDDbClientModel
    {
        public string ShopCD;
        public DateTime HoDate;

        public string ClientCD;
        public int Seq;

        public int SoldMoney;
        public int GetMoney;
        public int DiffMoney;

        public int SlipNO;
        public string TransactionType;
        public string KanriClientCD;
        public string SysTypeCD;

        


        public List<FDDbGroupModel> GroupList = new List<FDDbGroupModel>();

        public int Sum()
        {
            return GroupList.Sum(e => e.Sum());
        }
    }

    public class FDDbGroupModel
    {
        public int SoldMoney;
        public int GetMoney;
        public int DiffMoney;

        public string ShopCD;
        public DateTime HoDate;
        public string ClientCD;
        public string TaxTypeCD;
        public int TaxRate;

        /// <summary>
        /// 今回売上額符号 : 今回売上額がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string ThisSoldMoneyCD { get; set; }

        /// <summary>
        /// 今回売上額 : 当該顧客の今回売上額（符号なし編集）　税込み金額('03.12.15UPD)
        /// </summary>
        public string ThisSoldMoney { get; set; }

        /// <summary>
        /// 今回入金額符号 : 今回入金額がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string ThisGetMoneyCD { get; set; }

        /// <summary>
        /// 今回入金額 : 当該顧客の今回入金額（符号なし編集） 前回未収額+今回売上額('03.12.15UPD)
        /// </summary>
        public string ThisGetMoney { get; set; }

        /// <summary>
        /// 課税額符号 : 課税額がﾏｲﾅｽ："-"、以外："0"
        /// </summary>
        public string TaxCD { get; set; }

        /// <summary>
        /// 課税額 : 今回売上額に対する内消費税額（符号なし編集）
        /// </summary>
        public string Tax { get; set; }

        public List<FDDbItemModel> ItemList = new List<FDDbItemModel>();

        public int Sum()
        {
                if (ItemList.Count == 0) return 0;
                return ItemList.Sum(e => e.Money);
        }

        public bool AllItemZero()
        {
            return (ItemList.Count(e => e.UsedNum != 0) == 0);
        }
    }

    public class FDDbItemModel
    {
        public string ShopCD;
        public string ClientCD;
        public string TaxTypeCD;
        public int TaxRate;

        public string ItemCD;
        public int PrevNum;
        public int ThisNum;
        public int AddNum;
        public int BeforeNum;
        public int UsedNum;
        public int AfterNum;
        public int Price;
        public int Money;

        public int pre_PrevNum;
        public int pre_ThisNum;
        public int pre_AddNum;
        public int pre_BeforeNum;
        public int pre_UsedNum;
        public int pre_AfterNum;
        public int pre_Price;
        public int pre_Money;

        public int Tax
        {
            get
            {
                return Money * TaxRate / 100;
            }
        }
    }
}
