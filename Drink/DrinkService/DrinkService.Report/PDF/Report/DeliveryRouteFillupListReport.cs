using DrinkService.Report.Common;
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
    public class DeliveryRouteFillupListReport
    {
        // TempFile
        const string TempFile = "DeliveryRouteFillupListTemplate.pdf";

        // 帳票の座標Dictionary
        private Dictionary<string, MetaModel> metaDic = new Dictionary<string, MetaModel>();

        /// <summary>
        /// 帳票の作成(Stream)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stream"></param>
        public bool CreatePDF(List<DeliveryRouteFillupListModel> models, Stream stream)
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
        public bool CreatePDF(List<DeliveryRouteFillupListModel> models, string file)
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

        private void CreatePDF(List<DeliveryRouteFillupListModel> models, DrinkPDF report)
        {
            //型版を添加し
            report.AddTemplate(Config.TemplatePath + TempFile);
            report.MarginX = 0;
            report.MarginY = 0;

            //帳票の座標
            addMetaList();

            foreach (DeliveryRouteFillupListModel model in models)
            {
                report.NewPage(0, EnumDirection.横);
                report.BeginText(EnumFont.MSゴシック, 11.0f);

                report.DrawText(model.shopName, metaDic["店舗名"], EnumAlign.Left);
                report.DrawText(model.masterName, metaDic["担当者"], EnumAlign.Left);
                report.DrawText(model.date, metaDic["date"], EnumAlign.Left);
                report.DrawText(model.fillDate, metaDic["補充日"], EnumAlign.Left);
                report.DrawText(model.page, metaDic["page"], EnumAlign.Left);

                double h = 0.7;

                for (int i = 0; i < model.details.Count; i++)
                {
                    report.DrawText(model.details[i].productCode, metaDic["商品コード"], h * i, EnumAlign.Left);
                    report.DrawText(model.details[i].productName, metaDic["商品名"], h * i, EnumAlign.Left);
                    report.DrawText(model.details[i].fillupNum, metaDic["補充数"], h * i, EnumAlign.Right);
                }
            }
            report.Close();
        }

        private void addMetaList()
        {
            MetaModel metaShopName = new MetaModel("店舗名", 2.4, 9.5, 10, 0.678017222297903);
            metaDic.Add(metaShopName.Name, metaShopName);

            MetaModel metaFillDate = new MetaModel("補充日", 3.85, 10.0, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaFillDate.Name, metaFillDate);

            MetaModel metaMasterName = new MetaModel("担当者", 3.85, 10.5, 8.068200000000001, 0.678017222297903);
            metaDic.Add(metaMasterName.Name, metaMasterName);

            MetaModel metaDate = new MetaModel("date", 23.6411912404157, 9.5, 5.068200000000001, 0.678017222297903);
            metaDic.Add(metaDate.Name, metaDate);

            MetaModel metaPage = new MetaModel("page", 26.1711912404157, 10.0, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaPage.Name, metaPage);

            MetaModel metaProductCode = new MetaModel("商品コード", 2.4, 12, 5, 0.678017222297903);
            metaDic.Add(metaProductCode.Name, metaProductCode);

            MetaModel metaProductName = new MetaModel("商品名", 5.5, 12, 9, 0.678017222297903);
            metaDic.Add(metaProductName.Name, metaProductName);

            MetaModel metaFillNum = new MetaModel("補充数", 15, 12, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaFillNum.Name, metaFillNum);
        }
    }
}
