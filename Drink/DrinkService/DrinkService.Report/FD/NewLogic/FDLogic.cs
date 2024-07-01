using DrinkService.Data;
using DrinkService.Models;
using DrinkService.Report.FD.Model;
using DrinkService.Utils;
using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace DrinkService.Report.FD.Logic
{
    public partial class FDLogic 
    {
        /// <summary>
        /// 新消費税の対応
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="SysTypeCD"></param>
        /// <param name="clientSlipNos"></param>
        /// <param name="lastSlipNo"></param>
        /// <param name="shopLastSlipNo"></param>
        /// <returns></returns>
        public static List<object> GetFDData(DataTable dt, string SysTypeCD, ref Dictionary<string, int> clientSlipNos, int lastSlipNo,
    out int shopLastSlipNo)
        {
            List<object> result = new List<object>();

            //DBモデルの作成（使用数＝０のデータを削除）
            FDDbShopModel DbModel = FDDbShopModel.FromDataTable(dt);

            //店舗単位で処理
            foreach (var client in DbModel.ClientList)
            {
                client.SysTypeCD = SysTypeCD;
                //０のデータを削除
                FilterZero(client);

                //売り上げがない場合、処理しない
                if (client.GetMoney == 0 && client.Sum() == 0) continue;


                //取引種別が「売掛」以外の場合
                //if (client.TransactionType != "2")
                {
                    //調整処理
                    Ajust(client);
                }

                //ロジックモデルの作成
                var model = new FDLogicModel(client);

                //FD出力
                if (model.GotMoney() == 0 & model.SoldItemCount() == 0) continue;

                //出力処理　→　ファイルモデル
                int addIndex = 0;
                foreach(var header in model.HeaderList)
                {
                    if (header.DetailList.Count == 0) {
                        continue;
                    }

                    //SlipNO 設定
                    header.SetSlipNO(client, ref lastSlipNo, ref clientSlipNos, addIndex);

                    //ヘッダー部出力
                    object headerModel =(SysTypeCD == "0")?
                                        (object)header.ToNoSysModel():
                                        (object)header.ToSysModel();
                    result.Add(headerModel);

                    //明細部出力
                    foreach (var detail in header.DetailList)
                    {
                        object detailModel = (SysTypeCD == "0") ?
                                            (object)detail.ToNoSysModel(header.SlipNO) :
                                            (object)detail.ToSysModel(header.SlipNO);
                        result.Add(detailModel);

                    }
                    addIndex++;
                }
            }

            //結果を返す
            shopLastSlipNo = lastSlipNo;
            return result;
        }

        private static void FilterZero(FDDbClientModel client)
        {
            var gotMoney = client.GetMoney;
            //すべて０の場合、１件を残り
            if (client.GroupList.Count(e => e.AllItemZero() == false) == 0)
            {
                if (client.GroupList.Count > 1)
                {
                    client.GroupList.RemoveAt(1);
                }
                var list = client.GroupList[0].ItemList;
                for (var i = list.Count - 1; i > 0; i--)
                {
                    list.RemoveAt(i);
                }
                return;
            }
            else
            {
                for (var j = client.GroupList.Count - 1; j >= 0; j--)
                {
                    var group = client.GroupList[j];
                    for (var i = group.ItemList.Count - 1; i >= 0; i--)
                    {
                        var item = group.ItemList[i];
                        if (item.UsedNum == 0)
                        {
                            group.ItemList.Remove(item);
                        }
                    }
                    if (group.ItemList.Count == 0)
                    {
                        client.GroupList.Remove(group);
                    }
                }
            }

        }
        /// <summary>
        /// 調整処理
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        private static void Ajust(FDDbClientModel client)
        {
            var groupCount = client.GroupList.Count();
            if (groupCount == 0) return ;

            //調整処理
            if (groupCount==1)
            {
                Ajust1(client, client.GroupList[0]);
            }
            else
            {
                Ajust2(client, client.GroupList[0], client.GroupList[1]);
            }

        }

        /// <summary>
        /// 一つグループだけの場合調整
        /// </summary>
        /// <param name="gotMoney"></param>
        /// <param name="group"></param>
        private static void Ajust1(FDDbClientModel client, FDDbGroupModel group)
        {
            var ajust = client.GetMoney - group.Sum();
            AjustLastItem(client, group, client.GetMoney, group.Sum(), ajust);
        }

        /// <summary>
        /// 二つグループの調整
        /// </summary>
        /// <param name="gotMoney"></param>
        /// <param name="group1"></param>
        /// <param name="group2"></param>
        private static void Ajust2(FDDbClientModel client, FDDbGroupModel group1, FDDbGroupModel group2)
        {
            FDDbGroupModel groupHigh, groupLow;
            if (group1.Sum() > group2.Sum())
            {
                groupHigh = group1;
                groupLow = group2;
            }
            else
            {
                groupHigh = group2;
                groupLow = group1;
            }

            int ajust = groupHigh.Sum() + groupLow.Sum() - client.GetMoney;
            int ajust1, ajust2;
            if (ajust > groupLow.Sum() || groupLow.Sum()==0)
            {
                ajust2 = -groupLow.Sum();
                ajust1 = -(ajust - groupLow.Sum());
                int getMoeny2 = ajust1 + groupHigh.Sum() - client.GetMoney;

                AjustLastItem(client, groupHigh, client.GetMoney, groupHigh.Sum(), ajust1);


                AjustLastItem(client, groupLow, getMoeny2, groupLow.Sum(), ajust2);
            }
            else
            {
                ajust1 = 0;
                AjustLastItem(client, groupHigh, groupHigh.Sum(), groupHigh.Sum(), ajust1);
                ajust2 = -1*ajust;
                AjustLastItem(client, groupLow, client.GetMoney - groupHigh.Sum(), groupLow.Sum(), ajust2);
            }
            //AjustLastItem(groupHigh , ajust1);
            //AjustLastItem(groupHigh.ItemList, ajust2);

        }

        /// <summary>
        /// 調整処理
        /// </summary>
        /// <param name="ItemList"></param>
        /// <param name="ajust"></param>
        private static void AjustLastItem(FDDbClientModel client,FDDbGroupModel group, int GetMoney, int SoldMoney, int ajust)
        {
            int tax = group.TaxRate;

            if (SoldMoney == 0) {
                if (ajust > 0)
                {
                    var list = group.ItemList;
                    for (var i = list.Count-1; i > 0; i--)
                    {
                        list.RemoveAt(i);
                    }
                }
                else 
                {
                    group.ItemList.Clear();
                }
            }

            //取引種別が「売掛」以外の場合
            if (DataUtil.CStr(client.TransactionType) != "2")
            {
                //今回売上額
                group.SoldMoney = GetMoney;
                if (GetMoney < 0)
                {
                    group.ThisSoldMoneyCD = "-";
                    group.ThisSoldMoney = DataUtil.CStr(DataUtil.CDec(GetMoney) * -1);
                }
                else
                {
                    group.ThisSoldMoneyCD = "0";
                    group.ThisSoldMoney = DataUtil.CStr(GetMoney);
                }

                //今回入金額
                group.GetMoney = GetMoney;
                if (GetMoney < 0)
                {
                    group.ThisGetMoneyCD = "-";
                    group.ThisGetMoney = DataUtil.CStr(DataUtil.CDec(GetMoney) * -1);

                    //課税額
                    group.TaxCD = "-";
                    group.Tax = DataUtil.CStr((Math.Floor(DataUtil.CDbl(GetMoney) * tax / (100 + tax))) * -1);
                }
                else
                {
                    group.ThisGetMoneyCD = "0";
                    group.ThisGetMoney = DataUtil.CStr(GetMoney);

                    group.TaxCD = "0";
                    group.Tax = DataUtil.CStr(Math.Floor(DataUtil.CDbl(GetMoney) * tax / (100 + tax)));
                }
            }
            else
            {
                //今回売上額
                group.SoldMoney = SoldMoney;
                if (SoldMoney < 0)
                {
                    group.ThisSoldMoneyCD = "-";
                    group.ThisSoldMoney = DataUtil.CStr(DataUtil.CDec(SoldMoney) * -1);

                    //課税額
                    group.TaxCD = "-";
                    group.Tax = DataUtil.CStr((Math.Floor(DataUtil.CDbl(SoldMoney) * tax / (100 + tax))) * -1);
                }
                else
                {
                    group.ThisSoldMoneyCD = "0";
                    group.ThisSoldMoney = DataUtil.CStr(SoldMoney);

                    //課税額
                    group.TaxCD = "0";
                    group.Tax = DataUtil.CStr(Math.Floor(DataUtil.CDbl(SoldMoney) * tax / (100 + tax)));
                }

                //今回入金額
                group.GetMoney =GetMoney;
                if (GetMoney < 0)
                {
                    group.ThisGetMoneyCD = "-";
                    group.ThisGetMoney = DataUtil.CStr(DataUtil.CDec(GetMoney) * -1);
                }
                else
                {
                    group.ThisGetMoneyCD = "0";
                    group.ThisGetMoney = DataUtil.CStr(GetMoney);
                }
            }

            if (client.TransactionType != "2"){
                if (group.ItemList.Count > 0)
                {
                    var lastItem = group.ItemList[group.ItemList.Count - 1];
                    lastItem.Money += ajust;
                }
            }
        }

    }


}
