using DrinkService.Report.Common;
using DrinkService.Report.Model;
using SafeNeeds.SMAT.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report
{
    public class DeliveryRoutePaymentReport
    {
        // TempFile
        const string TempFile = "DeliveryRoutePaymentTemplate.pdf";

        // 帳票の座標Dictionary
        private Dictionary<string, MetaModel> metaDic = new Dictionary<string, MetaModel>();

        /// <summary>
        /// 帳票の作成(Stream)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stream"></param>
        public bool CreatePDF(List<DeliveryRoutePaymentModel> models, Stream stream)
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
        public bool CreatePDF(List<DeliveryRoutePaymentModel> models, string file)
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
        private void CreatePDF(List<DeliveryRoutePaymentModel> models, DrinkPDF report)
        {
            
            //型版を添加し
            report.AddTemplate(Config.TemplatePath + TempFile);
            report.MarginX = 0;
            report.MarginY = 0;
            

            //帳票の座標
            addMetaList();

            foreach (DeliveryRoutePaymentModel model in models)
            {
                report.NewPage(0, EnumDirection.横);
                report.BeginText(EnumFont.MSゴシック, 11.0f);

                report.DrawText(model.shopName, metaDic["店舗名"], EnumAlign.Left);
                report.DrawText(model.masterName, metaDic["担当者名"], EnumAlign.Left);
                report.DrawText(model.outputTime, metaDic["出力日"], EnumAlign.Left);
                report.DrawText(model.fillDate, metaDic["補充日"], EnumAlign.Left);
                report.DrawText(model.page, metaDic["page"], EnumAlign.Right);
                report.DrawText(model.totalAmount, metaDic["入金額合計"], EnumAlign.Right);
                report.DrawText(model.cash, metaDic["現金"], EnumAlign.Right);
            } 
            report.Close();
        }

        private void addMetaList()
        {
            MetaModel metaShopName = new MetaModel("店舗名", 3.6411912404157, 9.3, 8.068200000000001, 0.678017222297903);
            metaDic.Add(metaShopName.Name, metaShopName);

            MetaModel metaMasterName = new MetaModel("担当者名", 5.0, 10.0, 8.068200000000001, 0.678017222297903);
            metaDic.Add(metaMasterName.Name, metaMasterName);

            MetaModel metaOutput = new MetaModel("出力日", 22.9411912404157, 9.3, 5.068200000000001, 0.678017222297903);
            metaDic.Add(metaOutput.Name, metaOutput);

            MetaModel metaFillDate = new MetaModel("補充日", 9, 12.6, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaFillDate.Name, metaFillDate);

            MetaModel metaPage = new MetaModel("page", 24.55411912404157, 10.0, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaPage.Name, metaPage);

            MetaModel metaTotalAmount = new MetaModel("入金額合計", 15, 13.4, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaTotalAmount.Name, metaTotalAmount);

            MetaModel metaCash = new MetaModel("現金", 15, 14.8, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaCash.Name, metaCash);
        }
    }
}
