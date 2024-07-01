using DrinkService.Data;
using DrinkService.Data.ViewModels;
using DrinkService.Models;
using DrinkService.Report.FD.Model;
using DrinkService.Utils;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.FD.Logic
{
    public partial class ShopWithSysLogic
    {

        private static int tax;

        public static List<object> GetFDDataOld(DataTable dt, string SysTypeCD, ref Dictionary<string, int> clientSlipNos,
            out int shopLastSlipNo)
        {

            List<object> result = new List<object>();

            tax = GetFax();

            string key = "";
            string tkey = "";
            int indexAll = 0;
            int indexClient = 0;
            int indexItem = 1;
            int clientMoneySum = 0;
            int clientLastMoney = 0;
            int amount = 0;
            int usedCount = 0;
            List<ShopWithSysItemModel> sysList = new List<ShopWithSysItemModel>();
            List<ShopNoSysItemModel> noSysList = new List<ShopNoSysItemModel>();
            ShopWithSysItemModel lastSysModel = null;
            ShopNoSysItemModel lastNoSysModel = null;

            ShopWithSysClientModel currentShopSysModel = null;
            ShopNoSysClientModel currentShopNoSysModel = null;

            foreach (DataRow dr in dt.Rows)
            {
                string tempKey = DataUtil.CStr(dr["ShopCD"]) + "_" + DataUtil.CStr(dr["ClientCD"]) + "_" + DataUtil.CStr(dr["Seq"]);
                string tempKey2 = DataUtil.CStr(dr["ShopCD"]) + "_" + DataUtil.CStr(dr["ClientCD"]);
                if (tempKey != key)
                {
                    key = tempKey;
                    indexItem = 1;

                    if (tempKey2 != tkey)
                    {
                        tkey = tempKey2;
                        indexClient++;
                    }
                    //last money = getMoney - clientMoneySum
                    if (indexClient > 1)
                    {
                        int lastMoney = amount - (clientMoneySum - clientLastMoney);

                        if (SysTypeCD == "1")
                        {
                            ShopWithSysItemModel model;

                            string transactionType = "";
                            if (dt.Rows.Count > 1)
                            {
                                transactionType = DataUtil.CStr(dt.Rows[dt.Rows.IndexOf(dr) - 1]["TransactionType"]);
                            }
                            else
                            {
                                transactionType = DataUtil.CStr(dt.Rows[0]["TransactionType"]);
                            }

                            if (sysList.Count > 0)
                            {
                                model = sysList[sysList.Count - 1] as ShopWithSysItemModel;
                            }
                            else
                            {
                                model = lastSysModel;

                            }

                            //取引種別が「売掛」以外の場合
                            if (transactionType != "2")
                            {
                                if (lastMoney < 0)
                                {
                                    model.PositiveSaleCD = "-";
                                    model.PositiveSale = DataUtil.CStr(lastMoney * -1);
                                    model.ScheduleDeliveryMoneyCD = "-";
                                    model.ScheduleDeliveryMoney = DataUtil.CStr(lastMoney * -1);
                                }
                                else
                                {
                                    model.PositiveSaleCD = "0";
                                    model.PositiveSale = DataUtil.CStr(lastMoney);
                                    model.ScheduleDeliveryMoneyCD = "0";
                                    model.ScheduleDeliveryMoney = DataUtil.CStr(lastMoney);
                                }
                            }

                            if (usedCount == 0)
                            {
                                if (amount > 0)
                                {
                                    //取引種別が「売掛」以外の場合
                                    if (transactionType != "2")
                                    {
                                        //予定売上
                                        model.ScheduleDeliveryMoneyCD = "0";
                                        model.ScheduleDeliveryMoney = DataUtil.CStr(amount);

                                        //実績売上
                                        model.PositiveSaleCD = "0";
                                        model.PositiveSale = DataUtil.CStr(amount);
                                    }
                                    result.Add(currentShopSysModel);
                                    result.Add(model);
                                }

                            }
                            else
                            {
                                result.Add(currentShopSysModel);
                                result.AddRange(sysList);
                            }
                        }
                        else
                        {
                            ShopNoSysItemModel model;
                            string transactionType = "";
                            if (dt.Rows.Count > 1)
                            {
                                transactionType = DataUtil.CStr(dt.Rows[dt.Rows.IndexOf(dr) - 1]["TransactionType"]);
                            }
                            else
                            {
                                transactionType = DataUtil.CStr(dt.Rows[0]["TransactionType"]);
                            }
                            if (noSysList.Count > 0)
                            {
                                model = noSysList[noSysList.Count - 1] as ShopNoSysItemModel;
                            }
                            else
                            {
                                model = lastNoSysModel;
                            }

                            //取引種別が「売掛」以外の場合
                            if (transactionType != "2")
                            {
                                if (lastMoney < 0)
                                {
                                    model.PositiveSale = "-" + DataUtil.CStr(lastMoney * -1);
                                    model.ScheduleDeliveryMoney = "-" + DataUtil.CStr(lastMoney * -1);
                                }
                                else
                                {
                                    model.PositiveSale = DataUtil.CStr(lastMoney);
                                    model.ScheduleDeliveryMoney = DataUtil.CStr(lastMoney);
                                }
                            }

                            if (usedCount == 0)
                            {
                                if (amount > 0)
                                {
                                    //取引種別が「売掛」以外の場合
                                    if (transactionType != "2")
                                    {
                                        //納品売上
                                        model.ScheduleDeliveryMoney = DataUtil.CStr(amount);

                                        //実績売上
                                        model.PositiveSale = DataUtil.CStr(amount);
                                    }
                                    result.Add(currentShopNoSysModel);
                                    result.Add(model);
                                }

                            }
                            else
                            {
                                result.Add(currentShopNoSysModel);
                                result.AddRange(noSysList);
                            }
                        }
                    }

                    amount = DataUtil.CInt(dr["GetMoney"]);
                    clientMoneySum = 0;
                    clientLastMoney = 0;
                    usedCount = 0;
                    sysList.Clear();
                    noSysList.Clear();

                    if (SysTypeCD == "1")
                    {
                        currentShopSysModel = GetClientData(dr, indexAll, indexClient, ref clientSlipNos);
                    }
                    else
                    {
                        currentShopNoSysModel = GetNoSysClientData(dr, indexAll, indexClient);
                    }
                }

                if (DataUtil.CInt(dr["UsedNum"]) != 0)
                {
                    clientMoneySum += DataUtil.CInt(dr["Money"]);
                    clientLastMoney = DataUtil.CInt(dr["Money"]);
                    usedCount += System.Math.Abs(DataUtil.CInt(dr["UsedNum"]));

                    if (SysTypeCD == "1")
                    {
                        sysList.Add(GetItemData(dr, indexAll, indexClient, indexItem, clientSlipNos));
                    }
                    else
                    {
                        noSysList.Add(GetNoSysItemData(dr, indexAll, indexClient));
                    }

                    indexItem++;
                }
                else
                {
                    if (SysTypeCD == "1")
                    {
                        lastSysModel = GetItemData(dr, indexAll, indexClient, indexItem, clientSlipNos);
                    }
                    else
                    {
                        lastNoSysModel = GetNoSysItemData(dr, indexAll, indexClient);
                    }
                }

                if (indexAll == dt.Rows.Count - 1)
                {
                    int lastMoney = amount - (clientMoneySum - clientLastMoney);

                    if (SysTypeCD == "1")
                    {
                        ShopWithSysItemModel model;
                        if (sysList.Count > 0)
                        {
                            model = sysList[sysList.Count - 1] as ShopWithSysItemModel;
                        }
                        else
                        {
                            model = lastSysModel;
                        }

                        //取引種別が「売掛」以外の場合
                        if (DataUtil.CStr(dr["TransactionType"]) != "2")
                        {
                            if (lastMoney < 0)
                            {
                                model.PositiveSaleCD = "-";
                                model.PositiveSale = DataUtil.CStr(lastMoney * -1);
                                model.ScheduleDeliveryMoneyCD = "-";
                                model.ScheduleDeliveryMoney = DataUtil.CStr(lastMoney * -1);
                            }
                            else
                            {
                                model.PositiveSaleCD = "0";
                                model.PositiveSale = DataUtil.CStr(lastMoney);
                                model.ScheduleDeliveryMoneyCD = "0";
                                model.ScheduleDeliveryMoney = DataUtil.CStr(lastMoney);
                            }
                        }

                        if (usedCount == 0)
                        {
                            if (amount > 0)
                            {
                                //取引種別が「売掛」以外の場合
                                if (DataUtil.CStr(dr["TransactionType"]) != "2")
                                {
                                    //予定売上
                                    model.ScheduleDeliveryMoneyCD = "0";
                                    model.ScheduleDeliveryMoney = DataUtil.CStr(amount);

                                    //実績売上
                                    model.PositiveSaleCD = "0";
                                    model.PositiveSale = DataUtil.CStr(amount);
                                }
                                result.Add(currentShopSysModel);
                                result.Add(model);
                            }
                        }
                        else
                        {
                            result.Add(currentShopSysModel);
                            result.AddRange(sysList);
                        }
                    }
                    else
                    {
                        ShopNoSysItemModel model;
                        if (noSysList.Count > 0)
                        {
                            model = noSysList[noSysList.Count - 1] as ShopNoSysItemModel;
                        }
                        else
                        {
                            model = lastNoSysModel;
                        }


                        //取引種別が「売掛」以外の場合
                        if (DataUtil.CStr(dr["TransactionType"]) != "2")
                        {
                            if (lastMoney < 0)
                            {
                                model.PositiveSale = "-" + DataUtil.CStr(lastMoney * -1);
                                model.ScheduleDeliveryMoney = "-" + DataUtil.CStr(lastMoney * -1);
                            }
                            else
                            {
                                model.PositiveSale = DataUtil.CStr(lastMoney);
                                model.ScheduleDeliveryMoney = DataUtil.CStr(lastMoney);
                            }
                        }
                        if (usedCount == 0)
                        {
                            if (amount > 0)
                            {
                                //取引種別が「売掛」以外の場合
                                if (DataUtil.CStr(dr["TransactionType"]) != "2")
                                {
                                    //納品売上
                                    model.ScheduleDeliveryMoney = DataUtil.CStr(amount);

                                    //実績売上
                                    model.PositiveSale = DataUtil.CStr(amount);
                                }
                                result.Add(currentShopNoSysModel);
                                result.Add(model);
                            }

                        }
                        else
                        {
                            result.Add(currentShopNoSysModel);
                            result.AddRange(noSysList);
                        }
                    }
                }


                indexAll++;

            }

            shopLastSlipNo = lastSlipNo;
            return result;
        }


        private static ShopNoSysClientModel GetNoSysClientData(DataRow row, int indexAll, int indexClient)
        {
            ShopNoSysClientModel model = new ShopNoSysClientModel();

            model.Type = "Client";

            model.RecoeCD = "1";

            //固定値　"007800"
            model.ShuttleCD = "007800";
            model.ReportDate = DataUtil.CDate(row["HoDate"]).ToString("yyyyMMdd");

            //店舗単位の連番（前ゼロ付与）
            model.SlipNO = DataUtil.CStr(row["Seq"]);
            model.ClientCD = DataUtil.CStr(row["KanriClientCD"]);

            model.ManageCD = DataUtil.CStr(row["KanriClientCD"]);
            model.OrderCD = "000000000";

            //前回未収額
            //if (DataUtil.CDec(row["pre_DiffMoney"]) < 0)
            //{
            //    model.PreUnGetMoney = "-" + DataUtil.CStr(DataUtil.CDec(row["pre_DiffMoney"]) * -1);
            //}
            //else
            //{
            //    model.PreUnGetMoney = DataUtil.CStr(row["pre_DiffMoney"]);
            //}
            model.PreUnGetMoney = "0";

            //取引種別が「売掛」以外の場合
            model.TransactionType = DataUtil.CStr(row["TransactionType"]);
            if (DataUtil.CStr(row["TransactionType"]) != "2")
            {
                //今回売上額
                if (DataUtil.CDec(row["GetMoney"]) < 0)
                {
                    model.ThisSoldMoney = "-" + DataUtil.CStr(DataUtil.CDec(row["GetMoney"]) * -1);
                }
                else
                {
                    model.ThisSoldMoney = DataUtil.CStr(row["GetMoney"]);
                }

                //今回入金額
                if (DataUtil.CDec(row["GetMoney"]) < 0)
                {
                    model.ThisGetMoney = "-" + DataUtil.CStr(DataUtil.CDec(row["GetMoney"]) * -1);
                    //課税額
                    model.Tax = DataUtil.CStr((Math.Floor(DataUtil.CDbl(row["GetMoney"]) * tax / (100 + tax))) * -1);
                }
                else
                {
                    model.ThisGetMoney = DataUtil.CStr(row["GetMoney"]);
                    //課税額
                    model.Tax = DataUtil.CStr(Math.Floor(DataUtil.CDbl(row["GetMoney"]) * tax / (100 + tax)));
                }
            }
            else
            {
                //今回売上額
                if (DataUtil.CDec(row["SoldMoney"]) < 0)
                {
                    model.ThisSoldMoney = "-" + DataUtil.CStr(DataUtil.CDec(row["SoldMoney"]) * -1); //課税額
                    model.Tax = DataUtil.CStr((Math.Floor(DataUtil.CDbl(row["SoldMoney"]) * tax / (100 + tax))) * -1);
                }
                else
                {
                    model.ThisSoldMoney = DataUtil.CStr(row["SoldMoney"]);
                    //課税額
                    model.Tax = DataUtil.CStr(Math.Floor(DataUtil.CDbl(row["SoldMoney"]) * tax / (100 + tax)));
                }

                //今回入金額
                if (DataUtil.CDec(row["GetMoney"]) < 0)
                {
                    model.ThisGetMoney = "-" + DataUtil.CStr(DataUtil.CDec(row["GetMoney"]) * -1);
                }
                else
                {
                    model.ThisGetMoney = DataUtil.CStr(row["GetMoney"]);
                }
            }


            //実績区分
            model.PositiveID = "00";


            model.Preliminary = " ";

            return model;
        }


        private static ShopNoSysItemModel GetNoSysItemData(DataRow row, int indexAll, int indexClient)
        {
            ShopNoSysItemModel model = new ShopNoSysItemModel();

            model.Type = "Item";

            model.RecoeCD = "2";
            model.ShuttleCD = "00" + "7800";
            model.ReportDate = DataUtil.CDate(row["HoDate"]).ToString("yyyyMMdd");
            model.SlipNO = DataUtil.CStr(row["Seq"]);
            model.ClientCD = DataUtil.CStr(row["KanriClientCD"]);

            model.TaxTypeCD = DataUtil.CStr(row["TaxTypeCD"]);


            model.WorkCD = "0";

            if (DataUtil.CStr(row["ItemCD"]).Length > 1)
            {
                model.WorkCD = "0" + DataUtil.CStr(row["ItemCD"]).Substring(0, 2);
            }
            else
            {
                model.WorkCD = "0";
            }


            if (DataUtil.CStr(row["ItemCD"]).Length > 5)
            {
                model.ItemCD1 = DataUtil.CStr(row["ItemCD"]).Substring(2, 4);
            }
            else
            {
                model.ItemCD1 = "0";
            }

            if (DataUtil.CStr(row["ItemCD"]).Length == 8)
            {
                model.ItemCD2 = DataUtil.CStr(row["ItemCD"]).Substring(6, 2);
                model.ItemCD = DataUtil.CStr(row["ItemCD"]).Substring(2, 6);
            }
            else
            {
                model.ItemCD2 = "0";
                model.ItemCD = DataUtil.CStr(row["ItemCD"]);
            }

            //固定値"00000000"
            model.MatNO = "00000000";
            //納品予定数（ﾏｲﾅｽ編集）
            if (DataUtil.CDec(row["UsedNum"]) < 0)
            {
                model.ScheduleDelivery = "-" + DataUtil.CStr(DataUtil.CDec(row["UsedNum"]) * -1);
            }
            else
            {
                model.ScheduleDelivery = DataUtil.CStr(row["UsedNum"]);
            }
            //納品売上
            if (DataUtil.CDec(row["Money"]) < 0)
            {
                model.ScheduleDeliveryMoney = "-" + DataUtil.CStr(DataUtil.CDec(row["Money"]) * -1);
            }
            else
            {
                model.ScheduleDeliveryMoney = DataUtil.CStr(row["Money"]);
            }

            model.PositiveID = "00";

            model.BaseMoney = "      0";

            //実績納品数符号
            if (DataUtil.CDec(row["UsedNum"]) < 0)
            {
                model.PositiveDelivery = "-" + DataUtil.CStr(DataUtil.CDec(row["UsedNum"]) * -1);
            }
            else
            {
                model.PositiveDelivery = DataUtil.CStr(row["UsedNum"]);
                if (string.IsNullOrEmpty(model.PositiveDelivery))
                {
                    model.PositiveDelivery = "0";
                }
            }

            //実績回収数符号
            model.PositiveBack = "0";

            //実績客中残数
            //if (DataUtil.CDec(row["AfterNum"]) < 0)
            //{
            //    model.PositiveRemain = "-" + DataUtil.CStr(DataUtil.CDec(row["AfterNum"]) * -1);
            //}
            //else
            //{
            //    model.PositiveRemain = DataUtil.CStr(row["AfterNum"]);
            //}
            model.PositiveRemain = "0";

            //実績売上
            if (DataUtil.CDec(row["Money"]) < 0)
            {
                model.PositiveSale = "-" + DataUtil.CStr(DataUtil.CDec(row["Money"]) * -1);
            }
            else
            {
                model.PositiveSale = DataUtil.CStr(row["Money"]);
            }

            //メンテ区分
            model.MaintenanceCD = "0";

            //実績区分
            model.PositiveID = "00";

            model.Preliminary = " ";

            return model;
        }

        private static int GetFax()
        {
            //defult
            tax = 8;
            string sql = @"
                select *
                from M_system
                where ProjNo = 1    
            ";
            DataTable dt = SQLHelper.GetDataTable(sql);

            if (dt.Rows.Count > 0)
            {
                tax = DataUtil.CInt(dt.Rows[0]["Tax"]);
            }
            return tax;
        }


        private static ShopWithSysClientModel GetClientData(DataRow row, int indexAll, int indexClient, ref Dictionary<string, int> clientSlipNos)
        {
            ShopWithSysClientModel model = new ShopWithSysClientModel();

            model.Type = "Client";

            model.ShopCD = DataUtil.CStr(row["ShopCD"]);

            //固定値　"007800"
            model.ShuttleCD = "00" + "7800";
            model.ReportDate = DataUtil.CDate(row["HoDate"]).ToString("yyyyMMdd");

            model.ClientCD = DataUtil.CStr(row["KanriClientCD"]);

            //店舗単位の連番（前ゼロ付与）
            //model.SlipNO = DataUtil.CStr(row["Seq"]);
            int slipNo = DataUtil.CInt(row["SlipNO"]);
            if (slipNo > 0)
            {
                model.SlipNO = DataUtil.CStr(slipNo);
                model.IsNew = false;
            }
            else
            {
                model.SlipNO = DataUtil.CStr(++lastSlipNo);
                if (lastSlipNo > 999999)
                {
                    lastSlipNo = 1;
                    model.SlipNO = "1";
                }
                model.IsNew = true;
                clientSlipNos.Add(model.ClientCD, lastSlipNo);
            }

            //固定値　"000"
            model.Seq = "000";
            model.SubCD = "0";
            model.ManageCD = "0";
            model.PreUnGetMoneyCD = "0";
            model.PreUnGetMoney = "0";

            ////前回未収額
            //if (DataUtil.CDec(row["pre_DiffMoney"]) < 0)
            //{
            //    model.PreUnGetMoneyCD = "-";
            //    model.PreUnGetMoney = DataUtil.CStr(DataUtil.CDec(row["pre_DiffMoney"]) * -1);
            //}
            //else 
            //{
            //    model.PreUnGetMoneyCD = "0";
            //    model.PreUnGetMoney = DataUtil.CStr(row["pre_DiffMoney"]);
            //}

            model.TransactionType = DataUtil.CStr(row["TransactionType"]);
            //取引種別が「売掛」以外の場合
            if (DataUtil.CStr(row["TransactionType"]) != "2")
            {
                //今回売上額
                if (DataUtil.CDec(row["GetMoney"]) < 0)
                {
                    model.ThisSoldMoneyCD = "-";
                    model.ThisSoldMoney = DataUtil.CStr(DataUtil.CDec(row["GetMoney"]) * -1);
                }
                else
                {
                    model.ThisSoldMoneyCD = "0";
                    model.ThisSoldMoney = DataUtil.CStr(row["GetMoney"]);
                }

                //今回入金額
                if (DataUtil.CDec(row["GetMoney"]) < 0)
                {
                    model.ThisGetMoneyCD = "-";
                    model.ThisGetMoney = DataUtil.CStr(DataUtil.CDec(row["GetMoney"]) * -1);

                    //課税額
                    model.TaxCD = "-";
                    model.Tax = DataUtil.CStr((Math.Floor(DataUtil.CDbl(row["GetMoney"]) * tax / (100 + tax))) * -1);
                }
                else
                {
                    model.ThisGetMoneyCD = "0";
                    model.ThisGetMoney = DataUtil.CStr(row["GetMoney"]);

                    model.TaxCD = "0";
                    model.Tax = DataUtil.CStr(Math.Floor(DataUtil.CDbl(row["GetMoney"]) * tax / (100 + tax)));
                }
            }
            else
            {
                //今回売上額
                if (DataUtil.CDec(row["SoldMoney"]) < 0)
                {
                    model.ThisSoldMoneyCD = "-";
                    model.ThisSoldMoney = DataUtil.CStr(DataUtil.CDec(row["SoldMoney"]) * -1);

                    //課税額
                    model.TaxCD = "-";
                    model.Tax = DataUtil.CStr((Math.Floor(DataUtil.CDbl(row["SoldMoney"]) * tax / (100 + tax))) * -1);
                }
                else
                {
                    model.ThisSoldMoneyCD = "0";
                    model.ThisSoldMoney = DataUtil.CStr(row["SoldMoney"]);

                    //課税額
                    model.TaxCD = "0";
                    model.Tax = DataUtil.CStr(Math.Floor(DataUtil.CDbl(row["SoldMoney"]) * tax / (100 + tax)));
                }

                //今回入金額
                if (DataUtil.CDec(row["GetMoney"]) < 0)
                {
                    model.ThisGetMoneyCD = "-";
                    model.ThisGetMoney = DataUtil.CStr(DataUtil.CDec(row["GetMoney"]) * -1);
                }
                else
                {
                    model.ThisGetMoneyCD = "0";
                    model.ThisGetMoney = DataUtil.CStr(row["GetMoney"]);
                }
            }



            //課税額
            //model.TaxCD = "0";
            //model.Tax = "0";

            //実績区分
            model.PositiveID = "00";

            model.FreeRate = "0";

            model.Preliminary = " ";

            DateTime dateNow = CommonUtils.GetDateTimeNow();
            model.Year = dateNow.ToString("yyyy");
            model.Month = dateNow.ToString("MM");
            model.Day = dateNow.ToString("dd");
            return model;
        }


        private static ShopWithSysItemModel GetItemData(DataRow row, int indexAll, int indexClient, int indexItem, Dictionary<string, int> clientSlipNos)
        {
            ShopWithSysItemModel model = new ShopWithSysItemModel();

            model.Type = "Item";

            model.UsedCount = System.Math.Abs(DataUtil.CInt(row["UsedNum"]));

            model.ShopCD = DataUtil.CStr(row["ShopCD"]);
            model.TaxTypeCD = DataUtil.CStr(row["TaxTypeCD"]);

            //固定値　"007800"
            model.ShuttleCD = "00" + "7800";


            model.ReportDate = DataUtil.CDate(row["HoDate"]).ToString("yyyyMMdd");

            model.ClientCD = DataUtil.CStr(row["KanriClientCD"]);
            //顧客レコードで採番した番号と同値（前ゼロ付与）
            //model.SlipNO = DataUtil.CStr(row["Seq"]);
            int slipNo = DataUtil.CInt(row["SlipNO"]);
            if (slipNo > 0)
            {
                model.SlipNO = DataUtil.CStr(slipNo);
            }
            else
            {
                model.SlipNO = DataUtil.CStr(clientSlipNos[model.ClientCD]);
            }

            //商品明細単位に連番（使用商品が３明細であれば001,002,003）
            model.Seq = DataUtil.CStr(indexItem);

            model.WorkCD = "0";

            if (DataUtil.CStr(row["ItemCD"]).Length > 1)
            {
                model.WorkCD = "0" + DataUtil.CStr(row["ItemCD"]).Substring(0, 2);
            }
            else
            {
                model.WorkCD = "0";
            }


            if (DataUtil.CStr(row["ItemCD"]).Length > 5)
            {
                model.ItemCD1 = DataUtil.CStr(row["ItemCD"]).Substring(2, 4);
            }
            else
            {
                model.ItemCD1 = "0";
            }

            if (DataUtil.CStr(row["ItemCD"]).Length == 8)
            {
                model.ItemCD2 = DataUtil.CStr(row["ItemCD"]).Substring(6, 2);
                model.ItemCD = DataUtil.CStr(row["ItemCD"]).Substring(2, 6);
            }
            else
            {
                model.ItemCD2 = "0";
                model.ItemCD = DataUtil.CStr(row["ItemCD"]);
            }

            //固定値　"0"
            model.ItemPreliminary = "0";

            model.MatNO = "0";
            model.CTMoneyCD = "0";
            model.CTMoney = "0";
            //納品予定数
            if (DataUtil.CDec(row["UsedNum"]) < 0)
            {
                model.ScheduleDeliveryCD = "-";
                model.ScheduleDelivery = DataUtil.CStr(DataUtil.CDec(row["UsedNum"]) * -1);
            }
            else
            {
                model.ScheduleDeliveryCD = "0";
                model.ScheduleDelivery = DataUtil.CStr(row["UsedNum"]);
            }
            //予定売上
            if (DataUtil.CDec(row["Money"]) < 0)
            {
                model.ScheduleDeliveryMoneyCD = "-";
                model.ScheduleDeliveryMoney = DataUtil.CStr(DataUtil.CDec(row["Money"]) * -1);
            }
            else
            {
                model.ScheduleDeliveryMoneyCD = "0";
                model.ScheduleDeliveryMoney = DataUtil.CStr(row["Money"]);
            }

            model.PositiveID = "0";



            //実績納品数符号
            if (DataUtil.CDec(row["UsedNum"]) < 0)
            {
                model.PositiveDeliveryCD = "-";
                model.PositiveDelivery = DataUtil.CStr(DataUtil.CDec(row["UsedNum"]) * -1);
            }
            else
            {
                model.PositiveDeliveryCD = "0";
                model.PositiveDelivery = DataUtil.CStr(row["UsedNum"]);
            }

            //実績回収数符号
            model.PositiveBackCD = "0";
            model.PositiveBack = "0";

            //実績客中残数
            //if (DataUtil.CDec(row["AfterNum"]) < 0)
            //{
            //    model.PositiveRemainCD = "-";
            //    model.PositiveRemain = DataUtil.CStr(DataUtil.CDec(row["AfterNum"]) * -1);
            //}
            //else
            //{
            //    model.PositiveRemainCD = "0";
            //    model.PositiveRemain = DataUtil.CStr(row["AfterNum"]);
            //}
            model.PositiveRemainCD = "0";
            model.PositiveRemain = "0";

            //実績売上
            if (DataUtil.CDec(row["Money"]) < 0)
            {
                model.PositiveSaleCD = "-";
                model.PositiveSale = DataUtil.CStr(DataUtil.CDec(row["Money"]) * -1);
            }
            else
            {
                model.PositiveSaleCD = "0";
                model.PositiveSale = DataUtil.CStr(row["Money"]);
            }

            //メンテ区分
            model.MaintenanceCD = "0";

            //実績区分
            model.PositiveID = "00";

            model.Preliminary = " ";

            DateTime dateNow = CommonUtils.GetDateTimeNow();
            model.Year = dateNow.ToString("yyyy");
            model.Month = dateNow.ToString("MM");
            model.Day = dateNow.ToString("dd");


            return model;
        }



    }
}
