using SafeNeeds.SMAT.Reports;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.Common
{
    public class DrinkPDF : SMATPDF
    {
         public DrinkPDF(Stream stream)
            : base(stream)
        {

        }
        public DrinkPDF(string pdfFile)
            : base(pdfFile)
        {
            string dir = pdfFile.Substring(0, pdfFile.LastIndexOf("\\"));
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
        }
        public DrinkPDF(string pdfFile, string templateFile)
            : base(pdfFile, templateFile)
        {

        }

        /// <summary>
        /// テキストの描画(leading)
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <param name="meta"></param>
        /// <param name="leading"></param>
        /// <param name="alg"></param>
        /// <history>
        /// add by tei 2013.08.02
        /// </history>
        public void DrawText(string fieldValue, MetaModel meta, EnumAlign alg = EnumAlign.Left, float leading = -1)
        {
            if (fieldValue == null) return;
            //if (fieldValue == "0") return;
            if (leading == -1)
                leading = 15.0f;
            this.DrawText(fieldValue, meta.X, meta.Y, meta.W, meta.H, leading, alg);
        }

        /// <summary>
        /// テキストの描画(leading)
        /// </summary>
        /// <param name="fieldValue"></param>
        /// <param name="meta"></param>
        /// <param name="leading"></param>
        /// <param name="alg"></param>
        /// <history>
        /// add by tei 2013.08.02
        /// </history>
        public void DrawText(string fieldValue, MetaModel meta, double MarginY, EnumAlign alg = EnumAlign.Left, float leading = -1)
        {

            if (fieldValue == null) return;
            fieldValue = fieldValue.Replace("　", "  ");
            //if (fieldValue == "0") return;
            if (leading == -1)
                leading = 15.0f;
            this.DrawText(fieldValue, meta.X, meta.Y + MarginY, meta.W, meta.H, leading, alg);
        }

        /// <summary>
        /// チェックボックスの描画
        /// </summary>
        /// <param name="meta"></param>
        public void DrawCheckBox(MetaModel meta)
        {
            this.DrawText("✓", meta, EnumAlign.Center);
            
        }

    }
}
