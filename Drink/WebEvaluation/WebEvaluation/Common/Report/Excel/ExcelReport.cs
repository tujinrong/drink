using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;

namespace WebEvaluation.Common
{
    public class ExcelReport:IReport
    {

        public List<IReportData> Data { get; set; }


        public IReportHandler Handler { get; set; }


        public String ReportName { get; set; }


        public String ReportId { get; set; }


        public String Template { get; set; }


        public String FileName { get; set; }


        public String SavePath { get; set; }


        public EnumReportType ReportType { get; set; }


        public ReportDefined Defined { get; set; }


        public String SaveFile { get; set; }

        public bool isAutoRowHeight { get; set; }

        public String DownLoadPath()
        {
            return "/temp/" + this.FileName;
        }

        public void CreateReport() {
		
		string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;

        this.Template = basePath + @"\ReportTemplate\" + this.Template;

        this.SavePath = basePath + @"\temp\";

        String date = string.Format("{0:yyyyMMddHHmmssfff}", DateTime.Now);

        this.FileName = this.FileName +"_" + date + ".xls";
        SaveFile = this.SavePath + this.FileName;
		


		try {
			if (this.CopySourceFile(this.Template,this.SaveFile)) {
				
                HSSFWorkbook wb;
				using (FileStream file = new FileStream(this.SaveFile, FileMode.Open, FileAccess.ReadWrite))
                {
                    wb = new HSSFWorkbook(file);

                }
                    //获取模版定义信息
				this.Defined = new ReportDefined();
			
				ISheet definedSheet=wb.GetSheet("DefinitionSheet");
				
				this.Defined.DetailBegin = ((int)definedSheet.GetRow(1).GetCell(2).NumericCellValue);
				this.Defined.DetailRows = ((int)definedSheet.GetRow(2).GetCell(2).NumericCellValue);
				
				for (int rowNum = 6; rowNum <= definedSheet.LastRowNum; rowNum++) { 	
					IRow hssfRow = definedSheet.GetRow(rowNum);  
	                if (hssfRow == null) {  
	                    continue;  
	                }  
	                
	                if(hssfRow.GetCell(0).ToString().Length == 0){
	                	continue;
	                }

                    if (hssfRow.GetCell(0).ToString() == "明細部：")
                    {
	                	continue;
	                }

                    if (hssfRow.GetCell(0).ToString() == "フィールド名称")
                    {
	                	continue;
	                }
	                
	                DefinedItem item = new DefinedItem();
	                item.FieldName = hssfRow.GetCell(0).ToString();
	                item.Field = hssfRow.GetCell(1).ToString();
                    if (hssfRow.GetCell(2) != null) {
                        item.Type = hssfRow.GetCell(2).ToString();
                    }
                    
	                
	                this.Defined.AddItem(item);
	                
		        }
				
				//
				ISheet sheet=wb.GetSheetAt(0);
                Regex regex = new Regex("\\[\\*(.*?)\\*\\]", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            	
                for (int rowNum = 0; rowNum <= sheet.LastRowNum; rowNum++) { 	
					IRow hssfRow = sheet.GetRow(rowNum);  
	                if (hssfRow == null) {  
	                    continue;  
	                }  
	                //遍历row中的所有方格 
	                foreach (ICell cell in hssfRow) { 
	                	String value = cell.ToString();
	                	if(value.Length == 0){
	                		continue;
	                	}
	                	

                        if (regex.IsMatch(value))
                        {
                            MatchCollection matchCollection = regex.Matches(value);
                            foreach (Match match in matchCollection)
                            {
                                String definedKey = match.Value.Replace("[*", "").Replace("*]", "");
	                        
                                this.Defined.SetItemInfo(definedKey, cell);
                            }
                        }
	                } 
		        }
				
				
				//根据配置信息 生成文件
				String sheetName = sheet.SheetName;
				int sIndex=0;
				foreach(IReportData pageData in this.Data){

                    ISheet newSheet = null;

                    if (this.ReportType == EnumReportType.FixedReport)
                    {
                        newSheet = wb.GetSheetAt(0);
                    }
                    else 
                    {
                        newSheet = wb.CloneSheet(0);
                    
                    }
					
					sIndex ++;

                    string sn = sheetName+"_"+sIndex;
                    if (this.Handler != null)
                    {
                        string tempSn = this.Handler.GetSheetName(pageData, sIndex);
                        if (string.IsNullOrEmpty(tempSn) == false) {
                            sn = tempSn;
                        }
                    }
                        
					wb.SetSheetName(wb.NumberOfSheets-1, sn);			
					//头部信息
                    this.FillData(wb,newSheet, pageData, -1);
					
					//复制明细行
					if(this.ReportType == EnumReportType.ListReport && pageData.Detail != null){
						if(pageData.Detail.Count > 0){
							
							//行
							for(int i=0 ; i<pageData.Detail.Count-1; i ++){
                                //newSheet.ShiftRows(
                                //        this.Defined.DetailBegin+(this.Defined.DetailRows-1),
                                //        sheet.LastRowNum+i,
                                //        this.Defined.DetailRows,
                                //        true,
                                //        false);

                                ShiftRows(newSheet,
                                        this.Defined.DetailBegin + (this.Defined.DetailRows - 1),
                                        sheet.LastRowNum + (i*this.Defined.DetailRows),
                                        this.Defined.DetailRows,
                                        true,
                                        false);
								
								CopyRows(newSheet,
										this.Defined.DetailBegin-1 ,
										(this.Defined.DetailBegin-1) + (this.Defined.DetailRows-1),
										this.Defined.DetailRows);
							}

						}
					}
					
					//填写明细
					if(pageData.Detail != null){
						if(pageData.Detail.Count() > 0){
							for(int i=0 ; i<pageData.Detail.Count(); i ++){
								IReportData detailData = pageData.Detail[i];
                                this.FillData(wb,newSheet, detailData, i);
							}

						}
					}

                    if (this.Handler != null)
                    {
                        this.Handler.AfterCreateReportSheet(wb, newSheet);
                    }
				}

                if (this.ReportType == EnumReportType.FixedReport)
                {
                    //删除定义信息页
                    wb.RemoveSheetAt(1);
                }
                else
                {
                    //删除模板页
                    wb.RemoveSheetAt(0);
                    //删除定义信息页
                    wb.RemoveSheetAt(0);

                }
                //李梁　2014/09/12
                try
                {
                   NPOI.HSSF.UserModel.HSSFFormulaEvaluator.EvaluateAllFormulaCells(wb);
                }
                catch (Exception)
                {
                    
                }
                using (FileStream file = new FileStream(this.SaveFile, FileMode.Open, FileAccess.ReadWrite))
                {
                    wb.Write(file);

                }

                if (this.Handler != null) {
                    this.Handler.AfterCreateReportBook(wb);
                }
				
                
			} else {
				//"File no found!"
				return;
			}
		} catch (IOException e) {
			return;
		} 
	}


        private void FillData(HSSFWorkbook workbook, ISheet sheet, IReportData data, int rowIndex)
        {

		Dictionary<String,DefinedItem> items = this.Defined.GetFieldItems();
		
		//Data信息
        PropertyInfo[] fields = data.GetType().GetProperties();


		foreach(PropertyInfo field in fields){
			String name = field.Name;
			if(items.ContainsKey(name)){
				String value = "";

                object odata = field.GetValue(data);

                value = CStr(odata);

				
				DefinedItem item = items[name];
				if(item.Points == null){
					continue;
				}
				foreach(DefinedPoint point in item.Points){
					int y = point.Y;
					if(rowIndex >=0){
						y = y+(rowIndex*this.Defined.DetailRows);
					}
					try {
						ICell cell=  sheet.GetRow(y).GetCell(point.X);

                        String cellVal = cell.ToString().Replace("[*" + item.FieldName + "*]", value);

                        String levaeStr = cell.ToString().Replace("[*" + item.FieldName + "*]", "").Trim();

                        if (levaeStr.Length == 0 && odata is int)
                        {
                            cell.SetCellValue(Int32.Parse(cellVal));
                        }
                        else 
                        {
                            if (item.Type == "html")
                            {

                                cell.SetCellValue(this.GetHtmlText(workbook, sheet, cellVal,cell.CellStyle.GetFont(workbook)));

                                //cell.SetCellValue(new HSSFRichTextString(@"<strong><font size='3'><font><font style='background-color: rgb(255, 0, 0);'>今週架電の９月７日10:00現在</font></font></font></strong><strong>"));  
                            }
                            else {
                                cell.SetCellValue(cellVal);
                            }
                             
                        }
                        

                        if (isAutoRowHeight == true) 
                        {
                            CalcAndSetRowHeight(cell.Row);
                        }
					
					} catch (Exception e) {
						//e.printStackTrace();

					}
					}

			}
			
			
		}
	}

        private void ShiftRows(ISheet sheet, int startRow, int endRow, int rows, bool copyRowHeight, bool resetOriginalRowHeight)
        {

            sheet.ShiftRows(startRow, endRow, rows, copyRowHeight, resetOriginalRowHeight);

            //startRow = startRow - 1;

            //for (int i = 0; i < rows; i++)
            //{
            //    IRow sourceRow = null;
            //    IRow targetRow = null;
            //    ICell sourceCell = null;
            //    ICell targetCell = null;
            //    short m;
            //    startRow = startRow + 1;
            //    sourceRow = sheet.GetRow(startRow);
            //    targetRow = sheet.CreateRow(startRow + 1);
            //    targetRow.Height = sourceRow.Height;

            //    for (m = sourceRow.FirstCellNum; m < sourceRow.LastCellNum; m++)
            //    {
            //        sourceCell = sourceRow.GetCell(m);
            //        targetCell = targetRow.CreateCell(m);

            //        //targetCell.setEncoding(sourceCell.getEncoding());
            //        targetCell.CellStyle = sourceCell.CellStyle;
            //        targetCell.CellStyle = sourceCell.CellStyle;
            //        targetCell.SetCellValue(i);//設置值
            //    }
            //}
        }

        /** 
             * 复制行 ，在poi中excel的sheet，row，cell都从0开始
             *  
             * @param startRowIndex 
             *            起始行 
             * @param endRowIndex 
             *            结束行 
             * @param rowNum 
             *           移动行数，大于0为向下复制，小于0为向上复制
             */
        private void CopyRows(ISheet sheet, int startRow, int endRow, int rowNum)
        {
            int pStartRow = startRow;
            int pEndRow = endRow;
            int targetRowFrom;
            int targetRowTo;
            int columnCount;
            CellRangeAddress region = null;
            int i;
            int j;

            if (pStartRow < 0 && pEndRow < 0)
            {
                return;
            }
            else if (pStartRow < 0)
            {
                pStartRow = 0;
            }
            else if (pEndRow < 0)
            {
                pEndRow = 0;
            }
            if (pStartRow > pEndRow)
            {
                int x = pEndRow;
                pEndRow = pStartRow;
                pStartRow = x;
            }

            for (i = sheet.NumMergedRegions - 1; i >= 0; i--)
            {
                region = sheet.GetMergedRegion(i);
                if ((region.FirstRow >= pStartRow) && (region.LastRow <= pEndRow))
                {
                    targetRowFrom = region.FirstRow + rowNum;
                    targetRowTo = region.LastRow + rowNum;
                    CellRangeAddress newRegion = region.Copy();
                    newRegion.FirstRow = targetRowFrom;
                    newRegion.FirstColumn = region.FirstColumn;
                    newRegion.LastRow = targetRowTo;
                    newRegion.LastColumn = region.LastColumn;
                    sheet.AddMergedRegion(newRegion);
                    
                }
            }
            for (i = pEndRow; i >= pStartRow; i--)
            {
                IRow sourceRow = sheet.GetRow(i);
                columnCount = sourceRow.LastCellNum;
                if (sourceRow != null)
                {
                    IRow newRow = sheet.CreateRow(i + rowNum);

                    if (this.isAutoRowHeight == false) 
                    {
                        newRow.Height = sourceRow.Height;
                    }
                    for (j = 0; j < columnCount; j++)
                    {
                        ICell templateCell = sourceRow.GetCell(j);
                        if (templateCell != null)
                        {
                            ICell newCell = newRow.CreateCell(j);
                            CopyCell(templateCell, newCell);
                        }
                    }
                }
            }
        }

        HSSFRichTextString GetHtmlText(HSSFWorkbook workbook, ISheet sheet, string htmlStr, IFont font1)
        {


            string htmlStrHandle = htmlStr.Replace("&lt;br&gt;", "\r\n")
                .Replace("&amp;lt;", "<")
                .Replace("&amp;gt;", ">")
                .Replace("&amp;amp;", "&")
                .Replace("&amp;nbsp;", " ");

             

            Dictionary<string, HSSFFont> formatDic = new Dictionary<string, HSSFFont>();
            string text = "";
            int length = 0;

            handleHtmlText(workbook, sheet, ref formatDic, ref htmlStrHandle, ref text, ref length, "", font1);

            HSSFRichTextString htmlVal = new HSSFRichTextString(text);

            //HSSFFont font1 = (HSSFFont)sheet.Workbook.CreateFont();
            //if (font1 != null)
            //{
            //    font1.FontName = "Meiryo UI";
            //    font1.FontHeightInPoints = 11;
            //}
            htmlVal.ApplyFont(0, text.Length, font1);

            foreach (KeyValuePair<string, HSSFFont> kv in formatDic) {
                HSSFFont font = kv.Value;
                string[] indexStr = kv.Key.Split(',');
                htmlVal.ApplyFont(Int32.Parse(indexStr[0]), Int32.Parse(indexStr[1]), font);

            }

            return htmlVal;
        }

        private HSSFFont GetHSSFFont(HSSFWorkbook workbook,ISheet sheet, string styleStr)
        {
            if (string.IsNullOrEmpty(styleStr)) {
                return null;
            }

            styleStr = styleStr.Replace("background-color", "");

            //front color str = color: rgb(0, 255, 0);  "\\[\\*(.*?)\\*\\]"
            string colorKey = "";
            Regex regexColor = new Regex("color: rgb\\((.*?)\\)", RegexOptions.IgnoreCase | RegexOptions.Multiline);
            if (regexColor.IsMatch(styleStr))
            {
                MatchCollection matchCollection = regexColor.Matches(styleStr);
                foreach (Match match in matchCollection)
                {
                    colorKey = match.Value.Replace("color: rgb(", "").Replace(")", "");
                    break;
                }
            }

            if (colorKey == "") {
                Regex regexColor2 = new Regex("color=\"#(.*?)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                if (regexColor2.IsMatch(styleStr))
                {
                    MatchCollection matchCollection = regexColor2.Matches(styleStr);
                    foreach (Match match in matchCollection)
                    {
                        colorKey = match.Value.Replace("color=\"#", "").Replace("\"", "");
                        break;
                    }
                }
            }


            if (colorKey == "")
            {
                return null;
            }

            HSSFFont font1 = (HSSFFont)sheet.Workbook.CreateFont();

            if (colorKey != "")
            {
                string[] colorP = colorKey.Split(',');
                Color frontColor;
                if(colorKey.Length == 6){
                    frontColor = colorHx16toRGB(colorKey);
                }else{
                    frontColor = Color.FromArgb(Int32.Parse(colorP[0]), Int32.Parse(colorP[1]), Int32.Parse(colorP[2]));
                }
                
                font1.Color = GetXLColour(workbook, frontColor);
            }

            return font1;
        }

        #region [颜色：16进制转成RGB]
        /// <summary>
        /// [颜色：16进制转成RGB]
        /// </summary>
        /// <param name="strColor">设置16进制颜色 [返回RGB]</param>
        /// <returns></returns>
        public  System.Drawing.Color colorHx16toRGB(string strHxColor)
        {
            try
            {
                if (strHxColor.Length == 0)
                {//如果为空
                    return System.Drawing.Color.FromArgb(0, 0, 0);//设为黑色
                }
                else
                {//转换颜色
                    return System.Drawing.Color.FromArgb(System.Int32.Parse(strHxColor.Substring(0, 2), System.Globalization.NumberStyles.AllowHexSpecifier), System.Int32.Parse(strHxColor.Substring(2, 2), System.Globalization.NumberStyles.AllowHexSpecifier), System.Int32.Parse(strHxColor.Substring(4, 2), System.Globalization.NumberStyles.AllowHexSpecifier));
                }
            }
            catch
            {//设为黑色
                return System.Drawing.Color.FromArgb(0, 0, 0);
            }
        }
        #endregion

        private void handleHtmlText(HSSFWorkbook workbook, ISheet sheet, ref Dictionary<string, HSSFFont> formatDic, ref string htmlStr, ref string text, ref int length, string tagStart, IFont font1)
        {
            if (string.IsNullOrEmpty(htmlStr))
            {
                return;
            }
            //normal text
            if (htmlStr.StartsWith("&lt;") == false) {
                int tagInex = htmlStr.IndexOf("&lt;");

                

                if (tagInex < 0)
                {
                    text += htmlStr;
                    htmlStr = "";
                    length += htmlStr.Length;
                    return;
                }
                else {
                    string tempStr = htmlStr.Substring(0, tagInex);
                   

                    htmlStr = htmlStr.Substring(tagInex);

                    HSSFFont font = GetHSSFFont(workbook, sheet, tagStart);
                    if (font != null)
                    {
                        font.FontName = ((HSSFFont)font1).FontName;
                        font.FontHeightInPoints = ((HSSFFont)font1).FontHeightInPoints;
                        formatDic.Add(length + "," + (length + tempStr.Length), font);
                    }

                    text += tempStr;
                    length += tempStr.Length;
                    

                    handleHtmlText(workbook, sheet, ref formatDic, ref htmlStr, ref text, ref length, tagStart,font1);
                    return;
                }
            }

            //</p> tag
            if (htmlStr.StartsWith("&lt;/p&gt;"))
            {
                int tagInex = htmlStr.IndexOf("&lt;/p&gt;");
                htmlStr = htmlStr.Substring(tagInex + "&lt;/p&gt;".Length);

                string tagStartTemp = "";
                //如果是继承父节点的 color 还原一级
                if (tagStart.EndsWith("_child")) {
                    tagStartTemp = tagStart.Substring(0, tagStart.LastIndexOf("_child"));
                }
                handleHtmlText(workbook, sheet, ref formatDic, ref htmlStr, ref text, ref length, tagStartTemp, font1);
                return;
            }

            //<p>
            if (htmlStr.StartsWith("&lt;p")) {
                int tagInex = htmlStr.IndexOf("&gt;");

                string tagStartTemp = htmlStr.Substring(0, tagInex + "&gt;".Length);

                //自己没有设置color
                if (tagStartTemp.Replace("background-color", "").Contains("color: rgb(") == false && tagStartTemp.Contains("color=\"") == false)
                { 
                    //如果父标签设置了 color 继承！
                    if (tagStart.Replace("background-color", "").Contains("color: rgb(") || tagStart.Contains("color=\""))
                    {
                        tagStart += "_child";
                    }
                    else {
                        tagStart = htmlStr.Substring(0, tagInex + "&gt;".Length);
                    }
                }
                else
                {
                    tagStart = tagStartTemp;
                }

                
                htmlStr = htmlStr.Substring(tagInex + "&gt;".Length);

                if (text.EndsWith("\r\n") == false && text != "")
                {
                    text += "\r\n";
                    length += 2;
                }

                handleHtmlText(workbook, sheet, ref formatDic, ref htmlStr, ref text, ref length, tagStart, font1);

            }

            //</span>
            if (htmlStr.StartsWith("&lt;/span&gt;"))
            {
                int tagInex = htmlStr.IndexOf("&lt;/span&gt;");
                htmlStr = htmlStr.Substring(tagInex + "&lt;/span&gt;".Length);

                string tagStartTemp = "";
                //如果是继承父节点的 color 还原一级
                if (tagStart.EndsWith("_child"))
                {
                    tagStartTemp = tagStart.Substring(0, tagStart.LastIndexOf("_child"));
                }
                handleHtmlText(workbook, sheet, ref formatDic, ref htmlStr, ref text, ref length, tagStartTemp, font1);
                return;
            }
            //<span>
            if (htmlStr.StartsWith("&lt;span"))
            {
                int tagInex = htmlStr.IndexOf("&gt;");
                string tagStartTemp = htmlStr.Substring(0, tagInex + "&gt;".Length);

                //自己没有设置color
                if (tagStartTemp.Replace("background-color", "").Contains("color: rgb(") == false && tagStartTemp.Contains("color=\"") == false)
                {
                    //如果父标签设置了 color 继承！
                    if (tagStart.Replace("background-color", "").Contains("color: rgb(") || tagStart.Contains("color=\""))
                    {
                        tagStart += "_child";
                    }
                    else
                    {
                        tagStart = htmlStr.Substring(0, tagInex + "&gt;".Length);
                    }
                }
                else {
                    tagStart = tagStartTemp;
                }
                htmlStr = htmlStr.Substring(tagInex + +"&gt;".Length);

                handleHtmlText(workbook, sheet, ref formatDic, ref htmlStr, ref text, ref length, tagStart, font1);

            }

            //</font>
            if (htmlStr.StartsWith("&lt;/font&gt;"))
            {
                int tagInex = htmlStr.IndexOf("&lt;/font&gt;");
                htmlStr = htmlStr.Substring(tagInex + "&lt;/font&gt;".Length);

                string tagStartTemp = "";
                //如果是继承父节点的 color 还原一级
                if (tagStart.EndsWith("_child"))
                {
                    tagStartTemp = tagStart.Substring(0, tagStart.LastIndexOf("_child"));
                }
                handleHtmlText(workbook, sheet, ref formatDic, ref htmlStr, ref text, ref length, tagStartTemp, font1);
                return;
            }
            //<font>
            if (htmlStr.StartsWith("&lt;font"))
            {
                int tagInex = htmlStr.IndexOf("&gt;");
                string tagStartTemp = htmlStr.Substring(0, tagInex + "&gt;".Length);

                //自己没有设置color
                if (tagStartTemp.Replace("background-color", "").Contains("color: rgb(") == false && tagStartTemp.Contains("color=\"") == false)
                {
                    //如果父标签设置了 color 继承！
                    if (tagStart.Replace("background-color", "").Contains("color: rgb(") || tagStart.Contains("color=\""))
                    {
                        tagStart += "_child";
                    }
                    else
                    {
                        tagStart = htmlStr.Substring(0, tagInex + "&gt;".Length);
                    }
                }
                else
                {
                    tagStart = tagStartTemp;
                }
                htmlStr = htmlStr.Substring(tagInex + +"&gt;".Length);

                handleHtmlText(workbook, sheet, ref formatDic, ref htmlStr, ref text, ref length, tagStart, font1);

            }

            //</div> tag
            if (htmlStr.StartsWith("&lt;/div&gt;"))
            {
                int tagInex = htmlStr.IndexOf("&lt;/div&gt;");
                htmlStr = htmlStr.Substring(tagInex + "&lt;/div&gt;".Length);

                string tagStartTemp = "";
                //如果是继承父节点的 color 还原一级
                if (tagStart.EndsWith("_child"))
                {
                    tagStartTemp = tagStart.Substring(0, tagStart.LastIndexOf("_child"));
                }
                handleHtmlText(workbook, sheet, ref formatDic, ref htmlStr, ref text, ref length, tagStartTemp, font1);
                return;
            }

            //<div>
            if (htmlStr.StartsWith("&lt;div"))
            {
                int tagInex = htmlStr.IndexOf("&gt;");
                string tagStartTemp = htmlStr.Substring(0, tagInex + "&gt;".Length);

                //自己没有设置color
                if (tagStartTemp.Replace("background-color", "").Contains("color: rgb(") == false && tagStartTemp.Contains("color=\"") == false)
                {
                    //如果父标签设置了 color 继承！
                    if (tagStart.Replace("background-color", "").Contains("color: rgb(") || tagStart.Contains("color=\""))
                    {
                        tagStart += "_child";
                    }
                    else
                    {
                        tagStart = htmlStr.Substring(0, tagInex + "&gt;".Length);
                    }
                }
                else
                {
                    tagStart = tagStartTemp;
                }
                htmlStr = htmlStr.Substring(tagInex + "&gt;".Length);

                if (text.EndsWith("\r\n") == false && text != "")
                {
                    text += "\r\n";
                    length += 2;
                }

                handleHtmlText(workbook, sheet, ref formatDic, ref htmlStr, ref text, ref length, tagStart, font1);

            }

            //<!-- -->
            if (htmlStr.StartsWith("&lt;"))
            {
                int tagInex = htmlStr.IndexOf("&gt;");
                string tagStartTemp = htmlStr.Substring(0, tagInex + "&gt;".Length);

                //自己没有设置color
                if (tagStartTemp.Replace("background-color", "").Contains("color: rgb(") == false && tagStartTemp.Contains("color=\"") == false)
                {
                    //如果父标签设置了 color 继承！
                    if (tagStart.Replace("background-color", "").Contains("color: rgb(") || tagStart.Contains("color=\""))
                    {
                        tagStart += "_child";
                    }
                    else
                    {
                        tagStart = htmlStr.Substring(0, tagInex + "&gt;".Length);
                    }
                }
                else
                {
                    tagStart = tagStartTemp;
                }
                htmlStr = htmlStr.Substring(tagInex + +"&gt;".Length);

                handleHtmlText(workbook, sheet, ref formatDic, ref htmlStr, ref text, ref length, tagStart, font1);

            }

            //</o:p>
            if (htmlStr.StartsWith("&lt;/o:p&gt;"))
            {
                int tagInex = htmlStr.IndexOf("&lt;/o:p&gt;");
                htmlStr = htmlStr.Substring(tagInex + "&lt;/o:p&gt;".Length);

                string tagStartTemp = "";
                //如果是继承父节点的 color 还原一级
                if (tagStart.EndsWith("_child"))
                {
                    tagStartTemp = tagStart.Substring(0, tagStart.LastIndexOf("_child"));
                }
                handleHtmlText(workbook, sheet, ref formatDic, ref htmlStr, ref text, ref length, tagStartTemp, font1);
                return;
            }
            //<o:p>
            if (htmlStr.StartsWith("&lt;o:p"))
            {
                int tagInex = htmlStr.IndexOf("&gt;");
                string tagStartTemp = htmlStr.Substring(0, tagInex + "&gt;".Length);

                //自己没有设置color
                if (tagStartTemp.Replace("background-color", "").Contains("color: rgb(") == false && tagStartTemp.Contains("color=\"") == false)
                {
                    //如果父标签设置了 color 继承！
                    if (tagStart.Replace("background-color", "").Contains("color: rgb(") || tagStart.Contains("color=\""))
                    {
                        tagStart += "_child";
                    }
                    else
                    {
                        tagStart = htmlStr.Substring(0, tagInex + "&gt;".Length);
                    }
                }
                else
                {
                    tagStart = tagStartTemp;
                }
                htmlStr = htmlStr.Substring(tagInex + +"&gt;".Length);

                handleHtmlText(workbook, sheet, ref formatDic, ref htmlStr, ref text, ref length, tagStart, font1);

            }
        }

        private short GetXLColour(HSSFWorkbook workbook, System.Drawing.Color SystemColour)
        {
            short s = 0;
            HSSFPalette XlPalette = workbook.GetCustomPalette();
            HSSFColor XlColour = XlPalette.FindColor(SystemColour.R, SystemColour.G, SystemColour.B);
            if (XlColour == null)
            {
                
                if (NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE < 255)
                {
                    if (NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE < 64)
                    {
                        //NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE = 64;
                        //NPOI.HSSF.Record.PaletteRecord.STANDARD_PALETTE_SIZE += 1;
                        
                        try
                        {
                            XlColour = XlPalette.AddColor(SystemColour.R, SystemColour.G, SystemColour.B);
                        }
                        catch (Exception e)
                        {
                            XlColour = XlPalette.FindSimilarColor(SystemColour.R, SystemColour.G, SystemColour.B);
                        }
                    }
                    else
                    {
                        XlColour = XlPalette.FindSimilarColor(SystemColour.R, SystemColour.G, SystemColour.B);
                    }

                    s = XlColour.GetIndex();
                }

            }
            else
                s = XlColour.GetIndex();

            return s;
        }  

        private static void CopyCell(ICell srcCell, ICell distCell)
        {
            distCell.CellStyle = srcCell.CellStyle;
            if (srcCell.CellComment != null)
            {
                distCell.CellComment = srcCell.CellComment;
            }
            CellType srcCellType = srcCell.CellType;
            distCell.SetCellType(srcCellType);

            if (srcCellType == CellType.Numeric)
            {
                if (HSSFDateUtil.IsCellDateFormatted(srcCell))
                {
                    distCell.SetCellValue (srcCell.DateCellValue);
                }
                else
                {
                    distCell.SetCellValue(srcCell.NumericCellValue);
                }
            }
            else if (srcCellType == CellType.String)
            {
                distCell.SetCellValue(srcCell.RichStringCellValue);
            }
            else if (srcCellType == CellType.Blank)
            {
                // nothing21  
            }
            else if (srcCellType == CellType.Boolean)
            {
                distCell.SetCellValue(srcCell.BooleanCellValue);
            }
            else if (srcCellType == CellType.Error)
            {
                distCell.SetCellErrorValue(srcCell.ErrorCellValue);
            }
            else if (srcCellType == CellType.Formula)
            {
                distCell.SetCellFormula(srcCell.CellFormula);
            }
            else
            { // nothing29  

            }
        }


        /// <summary>
        /// 元ファイルをコピー処理
        /// </summary>
        private bool CopySourceFile(string reportTemplateFile, string reportSaveFile)
        {

            DirectoryInfo dir = new DirectoryInfo(reportSaveFile.Substring(0, reportSaveFile.LastIndexOf("\\") + 1));

            if (!dir.Exists)
            {
                dir.Create();
            }
            else
            {
                //foreach (FileInfo fileInfo in dir.GetFiles(m_reportBuilder.ReportMetaData.ReportName + "*.xls"))
                //{
                //    //if (fileInfo.Name == reportSaveFile.Substring(reportSaveFile.LastIndexOf("\\") + 1))
                //    {
                //        try
                //        {
                //            fileInfo.Attributes = FileAttributes.Normal;
                //            fileInfo.Delete();
                //        }
                //        catch { }
                //    }

                //}
            }

            try
            {
                if (reportTemplateFile != reportSaveFile)
                {
                    System.IO.File.Copy(reportTemplateFile, reportSaveFile, true);

                    FileInfo file = new FileInfo(reportSaveFile);

                    file.Attributes = FileAttributes.Normal;
                }
            }
            catch (Exception)
            {
                
                return false ;
            }
            
            return true;

        }

        private String CStr(object obj)
        {
            if (obj != null)
            {
                return obj.ToString();
            }
            return "";
        }

        private void CalcAndSetRowHeight(IRow sourceRow)
        {  
        for (int cellIndex = sourceRow.FirstCellNum; cellIndex <= sourceRow.PhysicalNumberOfCells; cellIndex++) {  
            //行高  
            double maxHeight = sourceRow.Height;  
            ICell sourceCell = sourceRow.GetCell(cellIndex);  
            //单元格的内容  
            String cellContent = getCellContentAsString(sourceCell);  
            if(null == cellContent || ""==cellContent){  
                continue;  
            }  
            //单元格的宽高及单元格信息  
            Dictionary<String, Object> cellInfoMap = getCellInfo(sourceCell);  
            int cellWidth = (int)cellInfoMap["width"];  
            int cellHeight = (int)cellInfoMap["height"];  
            if(cellHeight > maxHeight){  
                maxHeight = cellHeight;  
            }  
            ICellStyle cellStyle = sourceCell.CellStyle;  
            IFont font = cellStyle.GetFont(sourceRow.Sheet.Workbook);  
            //字体的高度  
            double fontHeight = font.FontHeight;  
              
            //cell内容字符串总宽度  
            double cellContentWidth = System.Text.Encoding.Default.GetBytes(cellContent).Length * 2 * 256;  
              
            //字符串需要的行数 不做四舍五入之类的操作  
            double stringNeedsRows =(double)cellContentWidth / cellWidth;  
            //小于一行补足一行  
            if(stringNeedsRows < 1.0){  
                stringNeedsRows = 1.0;  
            }  
              
            //需要的高度             (Math.floor(stringNeedsRows) - 1) * 40 为两行之间空白高度  
            double stringNeedsHeight = (double)fontHeight * stringNeedsRows;  
            //需要重设行高  
            if(stringNeedsHeight > maxHeight){  
                maxHeight = stringNeedsHeight;  
                //超过原行高三倍 则为5倍 实际应用中可做参数配置  
                if(maxHeight/cellHeight > 5){  
                    maxHeight = 5 * cellHeight;  
                }  
                //最后取天花板防止高度不够  
                maxHeight = Math.Ceiling(maxHeight);  
                //重新设置行高 同时处理多行合并单元格的情况  
                Boolean isPartOfRowsRegion = (Boolean)cellInfoMap["isPartOfRowsRegion"];  
                if(isPartOfRowsRegion){  
                    int firstRow = (int)cellInfoMap["firstRow"];  
                    int lastRow = (int)cellInfoMap["lastRow"];  
                    //平均每行需要增加的行高  
                    double addHeight = (maxHeight - cellHeight)/(lastRow - firstRow + 1);  
                    for (int i = firstRow; i <= lastRow; i++) {  
                        double rowsRegionHeight =sourceRow.Sheet.GetRow(i).Height + addHeight;  
                        sourceRow.Sheet.GetRow(i).Height=((short)rowsRegionHeight);  
                    }  
                }else{  
                    sourceRow.Height=((short)maxHeight);  
                }  
            }  
        }  
    }

        /** 
         * 解析一个单元格得到数据 
         * @param cell 
         * @return 
         */
        private  String getCellContentAsString(ICell cell)
        {
            if (null == cell)
            {
                return "";
            }
            String result = "";
            switch (cell.CellType)
            {
                case CellType.Numeric:
                    String s = ""+cell.NumericCellValue;
                    if (s != null)
                    {
                        if (s == ".0")
                        {
                            s = s.Substring(0, s.Length - 2);
                        }
                    }
                    result = s;
                    break;
                case CellType.String:
                    result = CStr(cell.StringCellValue);
                    break;
                case CellType.Blank:
                    break;
                case CellType.Boolean:
                    result = CStr(cell.BooleanCellValue);
                    break;
                case CellType.Error:
                    break;
                default:
                    break;
            }
            return result;
        }

        /** 
         * 获取单元格及合并单元格的宽度 
         * @param cell 
         * @return 
         */
        private static Dictionary<String, Object> getCellInfo(ICell cell)
        {
            ISheet sheet = cell.Sheet;
            int rowIndex = cell.RowIndex;
            int columnIndex = cell.ColumnIndex;

            bool isPartOfRegion = false;
            int firstColumn = 0;
            int lastColumn = 0;
            int firstRow = 0;
            int lastRow = 0;
            int sheetMergeCount = sheet.NumMergedRegions;
            for (int i = 0; i < sheetMergeCount; i++)
            {
                CellRangeAddress ca = sheet.GetMergedRegion(i);
                firstColumn = ca.FirstColumn;
                lastColumn = ca.LastColumn;
                firstRow = ca.FirstRow;
                lastRow = ca.LastRow;
                if (rowIndex >= firstRow && rowIndex <= lastRow)
                {
                    if (columnIndex >= firstColumn && columnIndex <= lastColumn)
                    {
                        isPartOfRegion = true;
                        break;
                    }
                }
            }
            Dictionary<String, Object> map = new Dictionary<String, Object>();
            int width = 0;
            int height = 0;
            bool isPartOfRowsRegion = false;
            if (isPartOfRegion)
            {
                for (int i = firstColumn; i <= lastColumn; i++)
                {
                    width += sheet.GetColumnWidth(i);
                }
                for (int i = firstRow; i <= lastRow; i++)
                {
                    height += sheet.GetRow(i).Height;
                }
                if (lastRow > firstRow)
                {
                    isPartOfRowsRegion = true;
                }
            }
            else
            {
                width = sheet.GetColumnWidth(columnIndex);
                height += cell.Row.Height;
            }
            map.Add("isPartOfRowsRegion", isPartOfRowsRegion);
            map.Add("firstRow", firstRow);
            map.Add("lastRow", lastRow);
            map.Add("width", width);
            map.Add("height", height);
            return map;
        }  
    }
}