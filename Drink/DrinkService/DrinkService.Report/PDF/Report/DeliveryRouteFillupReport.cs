using DrinkService.Report.Common;
using DrinkService.Report.Model;
using DrinkService.Utils;
using SafeNeeds.SMAT.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.Report
{
    public class DeliveryRouteFillupReport
    {

        // TempFile
        const string TempFile = "DeliveryRouteFillupTemplate_v2.pdf";
        const string TempFile2 = "DeliveryRouteFillupNotYetTemplate.pdf";

        // 帳票の座標Dictionary
        private Dictionary<string, MetaModel> metaDic = new Dictionary<string, MetaModel>();

        /// <summary>
        /// 帳票の作成(Stream)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stream"></param>
        public bool CreatePDF(List<DeliveryRouteFillupModel> models, List<DeliveryRouteFillupNotYetModel> notYetModels, Stream stream)
        {
            try
            {
                DrinkPDF report = new DrinkPDF(stream);
                CreatePDF(models,notYetModels, report);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 帳票の作成（ファイル）
        /// </summary>
        /// <param name="model"></param>
        /// <param name="file"></param>
        public bool CreatePDF(List<DeliveryRouteFillupModel> models, List<DeliveryRouteFillupNotYetModel> notYetModels, string file)
        {
            try
            {
                DrinkPDF report = new DrinkPDF(file);
                CreatePDF(models,notYetModels, report);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        private void CreatePDF(List<DeliveryRouteFillupModel> models, List<DeliveryRouteFillupNotYetModel> notYetModels, DrinkPDF report)
        {
            //型版を添加し
            report.AddTemplate(Config.TemplatePath + TempFile);
            report.MarginX = 0;
            report.MarginY = 0;

            //帳票の座標
            addMetaList();

            //出発点X
            double baseX = 1;
            //出発点Y
            double baseY = 9.9;
            //行の高さ 
            double rowH = 0.658017222297903;
            //current座標
            double currentY = baseY;
            //max座標
            double maxY = 27;
            int page = 1;
            bool BSide = false;
            string date = CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd HH:mm:ss");

            foreach (DeliveryRouteFillupModel model in models)
            {
                report.NewPage(0, EnumDirection.横);
                BSide = false;
                report.BeginText(EnumFont.MSゴシック, 11f);

                report.DrawText(model.shopName, metaDic["店舗名"], EnumAlign.Left);
                report.DrawText(model.fillDate, metaDic["補充日"], EnumAlign.Left);
                report.DrawText("担当者:" + model.masterName, metaDic["担当者"], EnumAlign.Left);
                report.DrawText(date, metaDic["date"], EnumAlign.Left);
                model.page = "Page: " + page.ToString();
                page++;
                report.DrawText(model.page, metaDic["page"], EnumAlign.Right);
                //仕切り
                report.DrawHLine(baseX, baseY, 12.84);
                currentY = baseY;

                foreach (DeliveryRouteFillupDetailItem model1 in model.details)
                {
                    //
                    if ((maxY - currentY) <= (rowH * 8))
                    {
                        if (BSide)
                        {
                            report.NewPage(0, EnumDirection.横);
                            BSide = false;
                            report.DrawText(model.shopName, metaDic["店舗名"], EnumAlign.Left);
                            report.DrawText(model.fillDate, metaDic["補充日"], EnumAlign.Left);
                            report.DrawText("担当者:" + model.masterName, metaDic["担当者"], EnumAlign.Left);
                            report.DrawText(date, metaDic["date"], EnumAlign.Left);
                            model.page = "Page: " + page.ToString();
                            page++;
                            report.DrawText(model.page, metaDic["page"], EnumAlign.Right);
                            //仕切り
                            report.DrawHLine(baseX, baseY, 12.84);

                            currentY = baseY;
                        }
                        else
                        {
                            BSide = true;
                            report.DrawHLine(baseX + 14.84, baseY, 12.84);
                            //初期化Y
                            currentY = baseY;
                        }                       
                    }

                    if (BSide)
                    {
                        //行+1
                        currentY = currentY + rowH - 0.5;

                        metaDic["顧客title2"].Y = currentY;
                        report.DrawText("顧客:", metaDic["顧客title2"], EnumAlign.Left);
                        metaDic["顧客2"].Y = currentY;
                        report.DrawText(model1.customer, metaDic["顧客2"], EnumAlign.Left);
                        //行+1
                        currentY = currentY + rowH + 0;

                        metaDic["商品コードtitle2"].Y = currentY;
                        metaDic["商品名title2"].Y = currentY;
                        metaDic["単価title2"].Y = currentY;
                        metaDic["使用数title2"].Y = currentY;
                        metaDic["補充後数title2"].Y = currentY;
                        metaDic["金額title2"].Y = currentY;
                        report.DrawText("商品コード", metaDic["商品コードtitle2"], EnumAlign.Left);
                        report.DrawText("商品名", metaDic["商品名title2"], EnumAlign.Left);
                        report.DrawText("単価", metaDic["単価title2"], EnumAlign.Left);
                        report.DrawText("使用", metaDic["使用数title2"], EnumAlign.Left);
                        report.DrawText("補充後", metaDic["補充後数title2"], EnumAlign.Left);
                        report.DrawText("金額", metaDic["金額title2"], EnumAlign.Left);
                        currentY = currentY + rowH;
                    }
                    else
                    {
                        //行+1
                        currentY = currentY + rowH - 0.5;

                        metaDic["顧客title"].Y = currentY;
                        report.DrawText("顧客:", metaDic["顧客title"], EnumAlign.Left);
                        metaDic["顧客"].Y = currentY;
                        report.DrawText(model1.customer, metaDic["顧客"], EnumAlign.Left);
                        //行+1
                        currentY = currentY + rowH + 0;

                        metaDic["商品コードtitle"].Y = currentY;
                        metaDic["商品名title"].Y = currentY;
                        metaDic["単価title"].Y = currentY;
                        metaDic["使用数title"].Y = currentY;
                        metaDic["補充後数title"].Y = currentY;
                        metaDic["金額title"].Y = currentY;
                        report.DrawText("商品コード", metaDic["商品コードtitle"], EnumAlign.Left);
                        report.DrawText("商品名", metaDic["商品名title"], EnumAlign.Left);
                        report.DrawText("単価", metaDic["単価title"], EnumAlign.Left);
                        report.DrawText("使用", metaDic["使用数title"], EnumAlign.Left);
                        report.DrawText("補充後", metaDic["補充後数title"], EnumAlign.Left);
                        report.DrawText("金額", metaDic["金額title"], EnumAlign.Left);
                        currentY = currentY + rowH;
                    }
                    

                    for (int i = 0; i < model1.customerDetail.Count; i++)
                    {
                        //
                        if (currentY >= maxY)
                        {
                            if (BSide)
                            {
                                BSide = false;
                                currentY = currentY + rowH - 0.4;
                                report.DrawHLine(baseX + 14.84, currentY, 12.84);

                                report.NewPage(0, EnumDirection.横);

                                report.DrawText(model.shopName, metaDic["店舗名"], EnumAlign.Left);
                                report.DrawText(model.fillDate, metaDic["補充日"], EnumAlign.Left);
                                report.DrawText("担当者:" + model.masterName, metaDic["担当者"], EnumAlign.Left);
                                report.DrawText(date, metaDic["date"], EnumAlign.Left);
                                model.page = "Page: " + page.ToString();
                                page++;
                                report.DrawText(model.page, metaDic["page"], EnumAlign.Right);
                                report.DrawHLine(baseX, baseY, 12.84);
                                //初期化Y
                                currentY = baseY;
                                currentY = currentY + rowH - 0.5;

                                metaDic["顧客title"].Y = currentY;
                                report.DrawText("顧客:", metaDic["顧客title"], EnumAlign.Left);
                                metaDic["顧客"].Y = currentY;
                                report.DrawText(model1.customer, metaDic["顧客"], EnumAlign.Left);
                                //行+1
                                currentY = currentY + rowH;

                                metaDic["商品コードtitle"].Y = currentY;
                                metaDic["商品名title"].Y = currentY;
                                metaDic["単価title"].Y = currentY;
                                metaDic["使用数title"].Y = currentY;
                                metaDic["補充後数title"].Y = currentY;
                                metaDic["金額title"].Y = currentY;
                                report.DrawText("商品コード", metaDic["商品コードtitle"], EnumAlign.Left);
                                report.DrawText("商品名", metaDic["商品名title"], EnumAlign.Left);
                                report.DrawText("単価", metaDic["単価title"], EnumAlign.Left);
                                report.DrawText("使用", metaDic["使用数title"], EnumAlign.Left);
                                report.DrawText("補充後", metaDic["補充後数title"], EnumAlign.Left);
                                report.DrawText("金額", metaDic["金額title"], EnumAlign.Left);
                                //行+1
                                currentY = currentY + rowH;
                            }
                            else
                            {
                                currentY = currentY + rowH - 0.4;
                                report.DrawHLine(baseX, currentY, 12.84);

                                BSide = true;
                                report.DrawHLine(baseX + 14.84, baseY, 12.84);
                                //初期化Y
                                currentY = baseY;
                                currentY = currentY + rowH - 0.5;

                                metaDic["顧客title2"].Y = currentY;
                                report.DrawText("顧客:", metaDic["顧客title2"], EnumAlign.Left);
                                metaDic["顧客2"].Y = currentY;
                                report.DrawText(model1.customer, metaDic["顧客2"], EnumAlign.Left);
                                //行+1
                                currentY = currentY + rowH;

                                metaDic["商品コードtitle2"].Y = currentY;
                                metaDic["商品名title2"].Y = currentY;
                                metaDic["単価title2"].Y = currentY;
                                metaDic["使用数title2"].Y = currentY;
                                metaDic["補充後数title2"].Y = currentY;
                                metaDic["金額title2"].Y = currentY;
                                report.DrawText("商品コード", metaDic["商品コードtitle2"], EnumAlign.Left);
                                report.DrawText("商品名", metaDic["商品名title2"], EnumAlign.Left);
                                report.DrawText("単価", metaDic["単価title2"], EnumAlign.Left);
                                report.DrawText("使用", metaDic["使用数title2"], EnumAlign.Left);
                                report.DrawText("補充後", metaDic["補充後数title2"], EnumAlign.Left);
                                report.DrawText("金額", metaDic["金額title2"], EnumAlign.Left);
                                //行+1
                                currentY = currentY + rowH; 
                            }
                        }

                        if (BSide)
                        {
                            metaDic["軽減税率区分2"].Y = currentY - 0.04;
                            metaDic["商品コード2"].Y = currentY;
                            metaDic["商品名2"].Y = currentY;
                            metaDic["単価2"].Y = currentY;
                            metaDic["使用数2"].Y = currentY;
                            metaDic["補充後数2"].Y = currentY;
                            metaDic["金額2"].Y = currentY;
                            if ("0" == model1.customerDetail[i].taxTypeCD)
                            {
                                report.DrawText("★", metaDic["軽減税率区分2"], EnumAlign.Left);
                            }
                            report.DrawText(model1.customerDetail[i].productCode, metaDic["商品コード2"], EnumAlign.Left);
                            report.BeginText(EnumFont.MSゴシック, 8f);
                            report.DrawText(model1.customerDetail[i].productName, metaDic["商品名2"], EnumAlign.Left);
                            report.BeginText(EnumFont.MSゴシック, 11f);
                            report.DrawText(model1.customerDetail[i].price, metaDic["単価2"], EnumAlign.Right);
                            report.DrawText(model1.customerDetail[i].usedNum, metaDic["使用数2"], EnumAlign.Right);
                            report.DrawText(model1.customerDetail[i].fillNum, metaDic["補充後数2"], EnumAlign.Right);
                            report.DrawText(model1.customerDetail[i].money, metaDic["金額2"], EnumAlign.Right);
                            //行+1
                            currentY = currentY + rowH;
                        }
                        else
                        {
                            metaDic["軽減税率区分"].Y = currentY-0.04;
                            metaDic["商品コード"].Y = currentY;
                            metaDic["商品名"].Y = currentY;
                            metaDic["単価"].Y = currentY;
                            metaDic["使用数"].Y = currentY;
                            metaDic["補充後数"].Y = currentY;
                            metaDic["金額"].Y = currentY;
                            if ("0" == model1.customerDetail[i].taxTypeCD)
                            {
                                report.DrawText("★", metaDic["軽減税率区分"], EnumAlign.Left);
                            }
                            report.DrawText(model1.customerDetail[i].productCode, metaDic["商品コード"], EnumAlign.Left);
                            report.BeginText(EnumFont.MSゴシック, 8f);
                            report.DrawText(model1.customerDetail[i].productName, metaDic["商品名"], EnumAlign.Left);
                            report.BeginText(EnumFont.MSゴシック, 11f);
                            report.DrawText(model1.customerDetail[i].price, metaDic["単価"], EnumAlign.Right);
                            report.DrawText(model1.customerDetail[i].usedNum, metaDic["使用数"], EnumAlign.Right);
                            report.DrawText(model1.customerDetail[i].fillNum, metaDic["補充後数"], EnumAlign.Right);
                            report.DrawText(model1.customerDetail[i].money, metaDic["金額"], EnumAlign.Right);
                            //行+1
                            currentY = currentY + rowH;
                        }
                        
                    }
                    //n = metaDic["商品コード"].Y + model1.customerDetail.Count * rowH/2 + (rowH/2);

                    if (BSide)
                    {
                        //行+1
                        currentY = currentY + rowH - 0.5;
                        report.DrawHLine(baseX + 14.84, currentY, 12.84);

                        metaDic["集金額title2"].Y = currentY;
                        metaDic["集金額2"].Y = currentY;

                        report.DrawText("集金額", metaDic["集金額title2"], EnumAlign.Left);
                        report.DrawText(model1.amount, metaDic["集金額2"], EnumAlign.Right);
                        //行+1
                        currentY = currentY + rowH;
                    }
                    else
                    {
                        //行+1
                        currentY = currentY + rowH - 0.5;
                        report.DrawHLine(baseX, currentY, 12.84);

                        metaDic["集金額title"].Y = currentY;
                        metaDic["集金額"].Y = currentY;

                        report.DrawText("集金額", metaDic["集金額title"], EnumAlign.Left);
                        report.DrawText(model1.amount, metaDic["集金額"], EnumAlign.Right);
                        //行+1
                        currentY = currentY + rowH;
                    }
                }
            }

            //=============================NotYet=====================================
            baseX = baseX + 3;
            baseY = baseY - 0.4;

            foreach (DeliveryRouteFillupNotYetModel model in notYetModels)
            {
                report.NewPage(0, EnumDirection.横);
                report.BeginText(EnumFont.MSゴシック, 15f);
                report.DrawText("（未レン）", metaDic["NotYet"], EnumAlign.Left);
                report.BeginText(EnumFont.MSゴシック, 11f);

                report.DrawText(model.shopName, metaDic["店舗名"], EnumAlign.Left);
                report.DrawText(model.fillDate, metaDic["補充日"], EnumAlign.Left);
                report.DrawText(date, metaDic["date"], EnumAlign.Left);
                model.page = "Page: " + page.ToString();
                page++;
                report.DrawText(model.page, metaDic["page"], EnumAlign.Right);
                //仕切り
                report.DrawHLine(baseX, baseY, 20);
                currentY = baseY;
                foreach (DeliveryRouteFillupNotYetDetailItem model1 in model.details)
                {
                    if ((maxY - currentY) <= (rowH * 3))
                    {
                        report.NewPage(0, EnumDirection.横);
                        report.BeginText(EnumFont.MSゴシック, 15f);
                        report.DrawText("（未レン）", metaDic["NotYet"], EnumAlign.Left);
                        report.BeginText(EnumFont.MSゴシック, 11f);

                        report.DrawText(model.shopName, metaDic["店舗名"], EnumAlign.Left);
                        report.DrawText(model.fillDate, metaDic["補充日"], EnumAlign.Left);
                        report.DrawText(date, metaDic["date"], EnumAlign.Left);
                        model.page = "Page: " + page.ToString();
                        page++;
                        report.DrawText(model.page, metaDic["page"], EnumAlign.Right);
                        //仕切り
                        report.DrawHLine(baseX, baseY, 20);

                        currentY = baseY;
                    }

                    //行+1
                    currentY = currentY + rowH - 0.5;

                    metaDic["担当者NotYet"].Y = currentY;
                    report.DrawText("担当者:" + model1.masterName, metaDic["担当者NotYet"], EnumAlign.Left);
                    //行+1
                    currentY = currentY + rowH + 0;

                    for (int i = 0; i < model1.customerDetail.Count; i++)
                    {
                        //
                        if (currentY >= maxY)
                        {
                            currentY = currentY + rowH;
                            report.DrawHLine(baseX, currentY, 20);

                            report.NewPage(0, EnumDirection.横);
                            report.BeginText(EnumFont.MSゴシック, 15f);
                            report.DrawText("（未レン）", metaDic["NotYet"], EnumAlign.Left);
                            report.BeginText(EnumFont.MSゴシック, 11f);

                            report.DrawText(model.shopName, metaDic["店舗名"], EnumAlign.Left);
                            report.DrawText(model.fillDate, metaDic["補充日"], EnumAlign.Left);
                            report.DrawText(date, metaDic["date"], EnumAlign.Left);
                            model.page = "Page: " + page.ToString();
                            page++;
                            report.DrawText(model.page, metaDic["page"], EnumAlign.Right);
                            report.DrawHLine(baseX, baseY, 20);
                            //初期化Y
                            currentY = baseY;
                            currentY = currentY + rowH;

                            metaDic["担当者NotYet"].Y = currentY;
                            report.DrawText("担当者:" + model1.masterName, metaDic["担当者NotYet"], EnumAlign.Left);
                            //行+1
                            currentY = currentY + rowH;
                        }

                        metaDic["顧客NotYet"].Y = currentY;
                        metaDic["後日今ｽﾄフラグNotYet"].Y = currentY;
                        metaDic["後日NotYet"].Y = currentY;
                        report.DrawText("顧客：" + model1.customerDetail[i].customer, metaDic["顧客NotYet"], EnumAlign.Left);
                        report.DrawText(model1.customerDetail[i].afterStopFlag, metaDic["後日今ｽﾄフラグNotYet"], EnumAlign.Left);
                        report.DrawText(model1.customerDetail[i].afterDate, metaDic["後日NotYet"], EnumAlign.Right);
                        //行+1
                        currentY = currentY + rowH;
                    }
                }
            }
            report.Close();
        }


        private void addMetaList()
        {
            double rowHeight = 0.658017222297903;
            double rowWidth = 2.068200000000001;

            MetaModel metaShopName = new MetaModel("店舗名", 2.35, 8.14, 10, rowHeight);
            metaDic.Add(metaShopName.Name, metaShopName);

            MetaModel metaFillDate = new MetaModel("補充日", 3.85, 8.64, 2.068200000000001, rowHeight);
            metaDic.Add(metaFillDate.Name, metaFillDate);

            MetaModel metaMasterName = new MetaModel("担当者", 2.35, 9.09, 8.068200000000001, rowHeight);
            metaDic.Add(metaMasterName.Name, metaMasterName);

            MetaModel metaDate = new MetaModel("date", 23.9411912404157, 8.14, 5.068200000000001, rowHeight);
            metaDic.Add(metaDate.Name, metaDate);

            MetaModel metaPage = new MetaModel("page", 25.5811912404157, 8.64, 2.068200000000001, rowHeight);
            metaDic.Add(metaPage.Name, metaPage);

            //*****************************************************************************************************

            MetaModel metaCustomer = new MetaModel("顧客", 2, 1, 20.0, rowHeight);
            metaDic.Add(metaCustomer.Name, metaCustomer);


            MetaModel metaTaxTypeCD = new MetaModel("軽減税率区分", 0.780, 0, rowWidth, rowHeight);
            metaDic.Add(metaTaxTypeCD.Name, metaTaxTypeCD);

            MetaModel metaProductCode = new MetaModel("商品コード", 1.166, 0, rowWidth, rowHeight);
            metaDic.Add(metaProductCode.Name, metaProductCode);

            MetaModel metaProductName = new MetaModel("商品名", 3.532, 0, 5.838, rowHeight);
            metaDic.Add(metaProductName.Name, metaProductName);

            MetaModel metaPrice = new MetaModel("単価", 7.91, 0, rowWidth, rowHeight);
            metaDic.Add(metaPrice.Name, metaPrice);

            MetaModel metaUsedNum = new MetaModel("使用数", 9.03, 0, rowWidth, rowHeight);
            metaDic.Add(metaUsedNum.Name, metaUsedNum);

            MetaModel metaFillNum = new MetaModel("補充後数", 10.49, 0, rowWidth, rowHeight);
            metaDic.Add(metaFillNum.Name, metaFillNum);

            MetaModel metaMoney = new MetaModel("金額", 11.72, 0, rowWidth, rowHeight);
            metaDic.Add(metaMoney.Name, metaMoney);

            MetaModel metaCustomertitle = new MetaModel("顧客title", 1, 0, rowWidth, rowHeight);
            metaDic.Add(metaCustomertitle.Name, metaCustomertitle);

            MetaModel metaProductCodetitle = new MetaModel("商品コードtitle", 1.166, 0, rowWidth, rowHeight);
            metaDic.Add(metaProductCodetitle.Name, metaProductCodetitle);

            MetaModel metaProductNametitle = new MetaModel("商品名title", 3.532, 0, rowWidth, rowHeight);
            metaDic.Add(metaProductNametitle.Name, metaProductNametitle);

            MetaModel metaPricetitle = new MetaModel("単価title", 9.21, 0, rowWidth, rowHeight);
            metaDic.Add(metaPricetitle.Name, metaPricetitle);

            MetaModel metaUsedNumtitle = new MetaModel("使用数title", 10.33, 0, rowWidth, rowHeight);
            metaDic.Add(metaUsedNumtitle.Name, metaUsedNumtitle);

            MetaModel metaFillNumtitle = new MetaModel("補充後数title", 11.44, 0, rowWidth, rowHeight);
            metaDic.Add(metaFillNumtitle.Name, metaFillNumtitle);

            MetaModel metaMoneytitle = new MetaModel("金額title", 12.91, 0, rowWidth, rowHeight);
            metaDic.Add(metaMoneytitle.Name, metaMoneytitle);

            MetaModel metaAmounttitle = new MetaModel("集金額title", 10.71, 0, rowWidth, rowHeight);
            metaDic.Add(metaAmounttitle.Name, metaAmounttitle);

            MetaModel metaAmount = new MetaModel("集金額", 11.71, 0, rowWidth, rowHeight);
            metaDic.Add(metaAmount.Name, metaAmount);

            //*****************************************************************************************************

            MetaModel metaCustomer2 = new MetaModel("顧客2", 16.84, 0, 20.0, rowHeight);
            metaDic.Add(metaCustomer2.Name, metaCustomer2);

            MetaModel metaTaxTypeCD2 = new MetaModel("軽減税率区分2", 15.620, 0, rowWidth, rowHeight);
            metaDic.Add(metaTaxTypeCD2.Name, metaTaxTypeCD2);

            MetaModel metaProductCode2 = new MetaModel("商品コード2", 16.006, 0, rowWidth, rowHeight);
            metaDic.Add(metaProductCode2.Name, metaProductCode2);

            MetaModel metaProductName2 = new MetaModel("商品名2", 18.372, 0, 5.838, rowHeight);
            metaDic.Add(metaProductName2.Name, metaProductName2);

            MetaModel metaPrice2 = new MetaModel("単価2", 22.76, 0, rowWidth, rowHeight);
            metaDic.Add(metaPrice2.Name, metaPrice2);

            MetaModel metaUsedNum2 = new MetaModel("使用数2", 23.87, 0, rowWidth, rowHeight);
            metaDic.Add(metaUsedNum2.Name, metaUsedNum2);

            MetaModel metaFillNum2 = new MetaModel("補充後数2", 25.33, 0, rowWidth, rowHeight);
            metaDic.Add(metaFillNum2.Name, metaFillNum2);

            MetaModel metaMoney2 = new MetaModel("金額2", 26.56, 0, rowWidth, rowHeight);
            metaDic.Add(metaMoney2.Name, metaMoney2);

            MetaModel metaCustomertitle2 = new MetaModel("顧客title2", 15.84, 0, rowWidth, rowHeight);
            metaDic.Add(metaCustomertitle2.Name, metaCustomertitle2);

            MetaModel metaProductCodetitle2 = new MetaModel("商品コードtitle2", 16.006, 0, rowWidth, rowHeight);
            metaDic.Add(metaProductCodetitle2.Name, metaProductCodetitle2);

            MetaModel metaProductNametitle2 = new MetaModel("商品名title2", 18.372, 0, rowWidth, rowHeight);
            metaDic.Add(metaProductNametitle2.Name, metaProductNametitle2);

            MetaModel metaPricetitle2 = new MetaModel("単価title2", 24.06, 0, rowWidth, rowHeight);
            metaDic.Add(metaPricetitle2.Name, metaPricetitle2);

            MetaModel metaUsedNumtitle2 = new MetaModel("使用数title2", 25.17, 0, rowWidth, rowHeight);
            metaDic.Add(metaUsedNumtitle2.Name, metaUsedNumtitle2);

            MetaModel metaFillNumtitle2 = new MetaModel("補充後数title2", 26.18, 0, rowWidth, rowHeight);
            metaDic.Add(metaFillNumtitle2.Name, metaFillNumtitle2);

            MetaModel metaMoneytitle2 = new MetaModel("金額title2", 27.75, 0, rowWidth, rowHeight);
            metaDic.Add(metaMoneytitle2.Name, metaMoneytitle2);

            MetaModel metaAmounttitle2 = new MetaModel("集金額title2", 25.55, 0, rowWidth, rowHeight);
            metaDic.Add(metaAmounttitle2.Name, metaAmounttitle2);

            MetaModel metaAmount2 = new MetaModel("集金額2", 26.55, 0, rowWidth, rowHeight);
            metaDic.Add(metaAmount2.Name, metaAmount2);

            //=============================NotYet=====================================
            MetaModel notYet = new MetaModel("NotYet", 13.3, 9.8, 10, rowHeight);
            metaDic.Add("NotYet", notYet);

            MetaModel metaMasterNameNotYet = new MetaModel("担当者",  4, 0, 20.0, rowHeight);
            metaDic.Add("担当者NotYet", metaMasterNameNotYet);

            MetaModel customerNotYet = new MetaModel("顧客", 4.5, 0, 12.13333333333, rowHeight);
            metaDic.Add("顧客NotYet", customerNotYet);

            MetaModel afterStopFlag = new MetaModel("後日今ｽﾄフラグ", 17, 0, 3.44, rowHeight);
            metaDic.Add("後日今ｽﾄフラグNotYet", afterStopFlag);

            MetaModel afterDate = new MetaModel("後日", 19.19, 0, rowWidth, rowHeight);
            metaDic.Add("後日NotYet", afterDate);
        }
    }
}
