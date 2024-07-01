using DrinkService.Report.Common;
using DrinkService.Report.Model;
using SafeNeeds.SMAT.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using SafeNeeds.DySmat.Util;

namespace DrinkService.Report
{
    public class DeliveryRouteReport
    {
        // TempFile
        const string TempFile = "DeliveryRouteReportTemplate.pdf";

        // 帳票の座標Dictionary
        private Dictionary<string, MetaModel> metaDic = new Dictionary<string, MetaModel>();


        /// <summary>
        /// 帳票の作成(Stream)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stream"></param>
        public bool CreatePDF(List<DeliveryRouteModel> models, Stream stream)
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
        public bool CreatePDF(List<DeliveryRouteModel> models, string file)
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
        private void CreatePDF(List<DeliveryRouteModel> models, DrinkPDF report)
        {

            //型版を添加し
            report.AddTemplate(Config.TemplatePath + "DeliveryRouteReportTemplate.pdf");
            report.MarginX = 0;
            report.MarginY = 0;

            //帳票の座標
            addMetaList();

            int pageIndex = 0;
            string currentKey = string.Empty;
            foreach(DeliveryRouteModel model in models){

                if (model.kaishaName + "|" + model.times != currentKey)
                {
                    currentKey = model.kaishaName + "|" + model.times;
                    pageIndex = 0;
                }
                int count = models.Where(m => m.kaishaName == model.kaishaName && m.times == model.times).Count();
                model.page = (++pageIndex) +"/"+ count;

                report.NewPage(0, EnumDirection.縦);
                report.BeginText(EnumFont.MSゴシック, 9.0f);

                report.DrawText(model.times, metaDic["回数"], EnumAlign.Left);
                report.DrawText("会社名（設置場所）", metaDic["会社名Title"], EnumAlign.Center);
                report.DrawText(model.kaishaName, metaDic["会社名"], EnumAlign.Left);
                report.DrawText(model.addDate, metaDic["補充日"], EnumAlign.Left);
                report.DrawText(model.accessDate, metaDic["次回訪問日"], EnumAlign.Left);
                report.DrawText(model.master, metaDic["担当者"], EnumAlign.Left);
                report.DrawText(model.sale, metaDic["売上計"], EnumAlign.Left);
                report.DrawText(model.collect, metaDic["集金計"], EnumAlign.Left);
                report.DrawText(model.deficiency, metaDic["過不足"], EnumAlign.Left);
                report.DrawText(DataUtil.CStr(model.memo).Replace("\r\n", "  ").Replace("\n", "  "), metaDic["Memo"], EnumAlign.Left);
                report.DrawText(model.page, metaDic["Page"], EnumAlign.Left);
                report.DrawText(model.date, metaDic["Date"], EnumAlign.Right);
                if (string.IsNullOrEmpty(model.signData) == false)
                {
                    MemoryStream memStream = new MemoryStream(Convert.FromBase64String(model.signData.Replace("data:image/png;base64,","")));
                    System.Drawing.Image image = System.Drawing.Image.FromStream(memStream);
                    report.DrawImage(image, 6, 4, 10);
                }

                double h = 0.707;

                double constHeight = 2;

                for (int i = 0; i < model.details.Count; i++) {
                    report.DrawText(model.details[i].shelf, metaDic["棚"], constHeight + h * i, EnumAlign.Center);
                    report.DrawText(model.details[i].order, metaDic["順"], constHeight + h * i, EnumAlign.Left);
                    report.DrawText(model.details[i].productName, metaDic["商品名"], constHeight + h * i, EnumAlign.Left);
                    report.DrawText(model.details[i].price, metaDic["売価"], constHeight + h * i, EnumAlign.Right);
                    report.DrawText(model.details[i].preNum, metaDic["前回補充数"], constHeight + h * i, EnumAlign.Right);
                    report.DrawText(model.details[i].afterPreNum, metaDic["前回補充後"], constHeight + h * i, EnumAlign.Right);
                    report.DrawText(model.details[i].curStorage, metaDic["現在庫"], constHeight + h * i, EnumAlign.Right);
                    report.DrawText(model.details[i].used, metaDic["使用"], constHeight + h * i, EnumAlign.Right);
                    report.DrawText(model.details[i].fill, metaDic["補充"], constHeight + h * i, EnumAlign.Right);
                    report.DrawText(model.details[i].afterFill, metaDic["補充後"], constHeight + h * i, EnumAlign.Right);
                    report.DrawText(model.details[i].expirationDate, metaDic["賞味期限"], constHeight + h * i, EnumAlign.Left);
                }
            }
            
            report.Close();

        }

        /// <summary>
        /// 帳票の座標
        /// </summary>
        private void addMetaList()
        {
            double a = 1.40;

            MetaModel metaPage = new MetaModel("Page", 10.05, 27.2, 19.868200000000001, 5.678017222297903);
            metaDic.Add(metaPage.Name, metaPage);

            MetaModel metaDate = new MetaModel("Date", 17.4, -0.79831037460333, 3.068200000000001, 0.678017222297903);
            metaDic.Add(metaDate.Name, metaDate);

            MetaModel metaMemo = new MetaModel("Memo", 0.7, 25.8, 19.868200000000001, 1.178017222297903);
            metaDic.Add(metaMemo.Name, metaMemo);

            MetaModel metaTimes = new MetaModel("回数", 11.2, -0.19831037460333, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaTimes.Name, metaTimes);

            MetaModel metaKaishaNameTitle = new MetaModel("会社名Title", 0.7411912404157, 0.18, 8.668200000000001, 0.678017222297903);
            metaDic.Add(metaKaishaNameTitle.Name, metaKaishaNameTitle);

            MetaModel metaKaishaName = new MetaModel("会社名", 0.7411912404157, 1.3, 8.668200000000001, 2.678017222297903);
            metaDic.Add(metaKaishaName.Name, metaKaishaName);

            MetaModel metaAddDate = new MetaModel("補充日", 11.2, 0.29, 2.568200000000001, 0.678017222297903);
            metaDic.Add(metaAddDate.Name, metaAddDate);

            MetaModel metaAccessDate = new MetaModel("次回訪問日", 11.2, 0.78, 2.568200000000001, 0.678017222297903);
            metaDic.Add(metaAccessDate.Name, metaAccessDate);

            MetaModel metaMaster = new MetaModel("担当者", 11.2, 1.28, 9.3, 0.678017222297903);
            metaDic.Add(metaMaster.Name, metaMaster);

            MetaModel metaSale = new MetaModel("売上計", 11.2, 1.94, 1.668200000000001, 0.678017222297903);
            metaDic.Add(metaSale.Name, metaSale);

            MetaModel metaCollect = new MetaModel("集金計", 11.2, 2.68, 1.668200000000001, 0.678017222297903);
            metaDic.Add(metaCollect.Name, metaCollect);

            MetaModel metaDeficiency = new MetaModel("過不足", 11.2, 3.35, 1.668200000000001, 0.678017222297903);
            metaDic.Add(metaDeficiency.Name, metaDeficiency);

            MetaModel metaShelf = new MetaModel("棚", 0.55, 4.05 - 0.05 - a, 0.768200000000001, 0.678017222297903);
            metaDic.Add(metaShelf.Name,metaShelf);

            MetaModel metaOrder = new MetaModel("順", 2.5 - 0.92, 4.05 - 0.05 - a, 0.668200000000001, 0.678017222297903);
            metaDic.Add(metaOrder.Name, metaOrder);

            MetaModel metaProductName = new MetaModel("商品名", 3.0 - 1.0, 4.05 - 0.05 - a, 6, 0.678017222297903);
            metaDic.Add(metaProductName.Name, metaProductName);

            MetaModel metaPrice = new MetaModel("売価", 8.35, 4.05 - 0.05 - a, 1, 0.678017222297903);
            metaDic.Add(metaPrice.Name, metaPrice);

            MetaModel metaPreNum = new MetaModel("前回補充数", 9.85, 4.05 - 0.05 - a, 1, 0.678017222297903);
            metaDic.Add(metaPreNum.Name, metaPreNum);

            MetaModel metaAfterPreNum = new MetaModel("前回補充後", 11.4, 4.05 - 0.05 - a, 1, 0.678017222297903);
            metaDic.Add(metaAfterPreNum.Name, metaAfterPreNum);

            MetaModel metaCurStorage = new MetaModel("現在庫", 12.95, 4.05 - 0.05 - a, 1, 0.678017222297903);
            metaDic.Add(metaCurStorage.Name, metaCurStorage);

            MetaModel metaUsed = new MetaModel("使用", 14.5, 4.05 - 0.05 - a, 1, 0.678017222297903);
            metaDic.Add(metaUsed.Name, metaUsed);

            MetaModel metaFill = new MetaModel("補充", 16.0, 4.05 - 0.05 - a, 1, 0.678017222297903);
            metaDic.Add(metaFill.Name, metaFill);

            MetaModel metaAfterFill = new MetaModel("補充後", 17.55, 4.05 - 0.05 - a, 1, 0.678017222297903);
            metaDic.Add(metaAfterFill.Name, metaAfterFill);

            MetaModel metaExpirationDate = new MetaModel("賞味期限", 18.8, 4.05 - 0.05 - a, 2, 0.678017222297903);
            metaDic.Add(metaExpirationDate.Name, metaExpirationDate);
        }
    }
}
