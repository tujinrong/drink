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
    public class DeliveryRoutePlacementReport
    {
        // TempFile
        const string TempFile = "DeliveryRoutePlacementTemplate.pdf";

        // 帳票の座標Dictionary
        private Dictionary<string, MetaModel> metaDic = new Dictionary<string, MetaModel>();

        /// <summary>
        /// 帳票の作成(Stream)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="stream"></param>
        public bool CreatePDF(List<DeliveryRoutePlacementModel> models, Stream stream)
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
        public bool CreatePDF(List<DeliveryRoutePlacementModel> models, string file)
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

        private void CreatePDF(List<DeliveryRoutePlacementModel> models, DrinkPDF report)
        {
            //型版を添加し
            report.AddTemplate(Config.TemplatePath + TempFile);  
            report.MarginX = 0;
            report.MarginY = 0;


            //帳票の座標
            addMetaList();

            foreach (DeliveryRoutePlacementModel model in models)
            {
                report.NewPage(0, EnumDirection.横);
                report.BeginText(EnumFont.MSゴシック, 11.0f);

                report.DrawText(model.shopName, metaDic["店舗名"], EnumAlign.Left);
                report.DrawText(model.date, metaDic["date"], EnumAlign.Left);
                report.DrawText(model.page, metaDic["page"], EnumAlign.Right);

                double h = 0.7;

                if (model.details.Count < 20 || model.details.Count == 20)
                {
                    for (int i = 0; i < model.details.Count; i++)
                    {
                        report.DrawText(model.details[i].productCode, metaDic["商品コード"], h * i, EnumAlign.Left);
                        report.DrawText(model.details[i].productName, metaDic["商品名"], h * i, EnumAlign.Left);
                        report.DrawText(model.details[i].placeNum, metaDic["配置数"], h * i, EnumAlign.Right);

                    }
                }
                else if (model.details.Count > 20)
                {
                    int m = 0;
                    for (int i = 0; i < 20; i++)
                    {
                        report.DrawText(model.details[i].productCode, metaDic["商品コード"], h * i, EnumAlign.Left);
                        report.DrawText(model.details[i].productName, metaDic["商品名"], h * i, EnumAlign.Left);
                        report.DrawText(model.details[i].placeNum, metaDic["配置数"], h * i, EnumAlign.Right);
                    }
                    for (int j = 20; j < model.details.Count; j++)
                    {
                        report.DrawText(model.details[j].productCode, metaDic["商品コード1"], h * m, EnumAlign.Left);
                        report.DrawText(model.details[j].productName, metaDic["商品名1"], h * m, EnumAlign.Left);
                        report.DrawText(model.details[j].placeNum, metaDic["配置数1"], h * m, EnumAlign.Right);
                        m++;
                    }
                }
                
                
            }
            report.Close();
        }

        private void addMetaList()
        {
            MetaModel metaShopName = new MetaModel("店舗名", 3.2, 9.5, 10, 0.678017222297903);
            metaDic.Add(metaShopName.Name, metaShopName);

            MetaModel metaDate = new MetaModel("date", 22.5, 9.5, 5.068200000000001, 0.678017222297903);
            metaDic.Add(metaDate.Name, metaDate);

            MetaModel metaPage = new MetaModel("page", 24.1, 10.0, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaPage.Name, metaPage);

            MetaModel metaProductCode = new MetaModel("商品コード", 3.2, 11.5, 5, 0.678017222297903);
            metaDic.Add(metaProductCode.Name, metaProductCode);

            MetaModel metaProductName = new MetaModel("商品名", 5.5, 11.5, 7, 0.678017222297903);
            metaDic.Add(metaProductName.Name, metaProductName);

            MetaModel metaPlaceNum = new MetaModel("配置数", 11.5, 11.5, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaPlaceNum.Name, metaPlaceNum);

            //--------------------------------------------------------------------------------------------------------------------------------------------

            MetaModel metaProductCode1 = new MetaModel("商品コード1", 15.5, 11.5, 5, 0.678017222297903);
            metaDic.Add(metaProductCode1.Name, metaProductCode1);

            MetaModel metaProductName1 = new MetaModel("商品名1", 17.8, 11.5, 7, 0.678017222297903);
            metaDic.Add(metaProductName1.Name, metaProductName1);

            MetaModel metaPlaceNum1 = new MetaModel("配置数1", 23.8, 11.5, 2.068200000000001, 0.678017222297903);
            metaDic.Add(metaPlaceNum1.Name, metaPlaceNum1);
        }
    }
}
