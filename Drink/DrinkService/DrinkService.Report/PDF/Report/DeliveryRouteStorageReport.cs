using DrinkService.Report.Common;
using DrinkService.Report.Model;
using DrinkService.Report.Model;
using SafeNeeds.SMAT.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.Report
{
    public class DeliveryRouteStorageReport
    {
        const string TempFile = "DeliveryRouteStorageReportTemplate.pdf";

        private Dictionary<string, MetaModel> metaDic = new Dictionary<string, MetaModel>();

        /// <summary>
        /// 帳票の作成(Stream)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stream"></param>
        public bool CreatePDF(List<DeliveryRouteStorageModel> models, Stream stream)
        {
            try
            {
                DrinkPDF report = new DrinkPDF(stream);
                CreatePDF(models, report);
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
        public bool CreatePDF(List<DeliveryRouteStorageModel> models, string file)
        {
            try
            {
                DrinkPDF report = new DrinkPDF(file);
                CreatePDF(models, report);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// 帳票の作成
        /// </summary>
        /// <param name="model"></param>
        /// <param name="report"></param>
        private void CreatePDF(List<DeliveryRouteStorageModel> models, DrinkPDF report)
        {
            //型版を添加し
            report.AddTemplate(Config.TemplatePath + TempFile);
            report.MarginX = 0;
            report.MarginY = 0;

            //帳票の座標
            addMetaList();

            foreach (DeliveryRouteStorageModel model in models)
            {
                report.NewPage(0, EnumDirection.横);
                report.BeginText(EnumFont.MSゴシック, 11.0f);

                report.DrawText(model.shopName, metaDic["店舗名"], EnumAlign.Left);
                report.DrawText(model.outdate, metaDic["出庫日"], EnumAlign.Left);
                report.DrawText(model.masterName, metaDic["担当者"], EnumAlign.Left);
                report.DrawText(model.route, metaDic["route"], EnumAlign.Left);
                report.DrawText(model.date, metaDic["date"], EnumAlign.Right);
                report.DrawText(model.page, metaDic["page"], EnumAlign.Right);

                double h = 0.7;

                for (int i = 0; i < model.details.Count; i++)
                {
                    report.DrawText(model.details[i].productCode, metaDic["商品コード"], h * i, EnumAlign.Left);
                    report.DrawText(model.details[i].productName, metaDic["商品名"], h * i, EnumAlign.Left);
                    report.DrawText(model.details[i].realNum + "［　   ］", metaDic["実数"], h * i, EnumAlign.Right);
                    report.DrawText(model.details[i].caseNum + "［　   ］", metaDic["ケース数"], h * i, EnumAlign.Right);
                    report.DrawText(model.details[i].fractionNum + "［　   ］", metaDic["端数"], h * i, EnumAlign.Right);
                    report.DrawText("［　   ］", metaDic["戻り"], h * i, EnumAlign.Left);
                    report.DrawText("［　   ］", metaDic["廃棄"], h * i, EnumAlign.Left);
                }
            }
            report.Close();
        }

        private void addMetaList()
        {
            MetaModel metaShopName = new MetaModel("店舗名", 2.35, 9.5, 10, 0.678017222297903);
            metaDic.Add(metaShopName.Name, metaShopName);

            MetaModel metaOutDate = new MetaModel("出庫日", 3.85, 10.0, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaOutDate.Name, metaOutDate);

            MetaModel metaMasterName = new MetaModel("担当者", 3.85, 10.4, 8.068200000000001, 0.678017222297903);
            metaDic.Add(metaMasterName.Name, metaMasterName);

            MetaModel metaRouteName = new MetaModel("route", 3.85, 10.9, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaRouteName.Name, metaRouteName);

            MetaModel metaDate = new MetaModel("date", 22.2511912404157, 9, 5.068200000000001, 0.678017222297903);
            metaDic.Add(metaDate.Name, metaDate);

            MetaModel metaPage = new MetaModel("page", 25.2411912404157, 9.5, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaPage.Name, metaPage);

            MetaModel metaProductCode = new MetaModel("商品コード", 2.4, 12, 5, 0.678017222297903);
            metaDic.Add(metaProductCode.Name, metaProductCode);

            MetaModel metaProductName = new MetaModel("商品名", 5.5, 12, 9, 0.678017222297903);
            metaDic.Add(metaProductName.Name, metaProductName);

            MetaModel metaRealNum = new MetaModel("実数", 13.7, 12, 3.568200000000001, 0.678017222297903);
            metaDic.Add(metaRealNum.Name, metaRealNum);

            MetaModel metaCaseNum = new MetaModel("ケース数", 16.48, 12, 3.388200000000001, 0.678017222297903);
            metaDic.Add(metaCaseNum.Name, metaCaseNum);

            MetaModel metaFraction = new MetaModel("端数", 19.26, 12, 3.468200000000001, 0.678017222297903);
            metaDic.Add(metaFraction.Name, metaFraction);

            MetaModel metaReturnNum = new MetaModel("戻り", 23.2, 12, 1.868200000000001, 0.678017222297903);
            metaDic.Add(metaReturnNum.Name, metaReturnNum);

            MetaModel metaDisposalNum = new MetaModel("廃棄", 25.5, 12, 1.868200000000001, 0.678017222297903);
            metaDic.Add(metaDisposalNum.Name, metaDisposalNum); 

        } 
    }
}
