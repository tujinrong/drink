using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Logic;
using SafeNeeds.DySmat.Model;
using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebEvaluation.Utils
{
    public class DataIOUtils
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="i_fileName"></param>
        /// <param name="i_data"></param>
        public static DataExportResult DataTableToCsv(DataExportSetting i_setting, PageViewResult i_data)
        {
            DataExportResult result = new DataExportResult();
            result.ResultType = "Success";

            if (i_data.DataTable.Rows.Count == 0)
            {
                result.ResultType = "NoData";
                return result;
            }

            String key = Guid.NewGuid().ToString().Replace("-", "");
            result.ResourceId = key;
            result.Path = "/App_Resource" + "/" + key + ".csv";
            result.Extension = "csv";


            StreamWriter sw = null;
            try
            {

                // CSV 
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;//Encoding.GetEncoding("Shift-JIS");
                sw = new StreamWriter(i_setting.Dir + "\\" + key + ".csv", false, encoding);

                // 
                int beginIndex = 0;
                if (i_data.DataTable.Columns[0].ColumnName == "No")
                {
                    beginIndex = 1;
                }
                for (int i = 0; i <= i_data.DataTable.Rows.Count; i++)
                {
                    StringBuilder strBuilder = new StringBuilder();

                    for (int j = beginIndex; j < i_data.DataTable.Columns.Count - 1; j++)
                    {
                        if (i == 0)
                        {
                            //
                            //if (string.IsNullOrEmpty(i_data.DataTable.Columns[j].Caption) == false)
                            //{
                            //    strBuilder.Append(i_data.DataTable.Columns[j].Caption);
                            //}
                            //else
                            //{
                            //    strBuilder.Append(i_data.DataTable.Columns[j].ColumnName);
                            //}

                            strBuilder.Append(i_setting.ExportField[i_data.DataTable.Columns[j].ColumnName]);

                        }
                        else
                        {
                            //
                            if (i_data.DataTable.Columns[j].DataType.ToString() == "System.DateTime")
                            {
                                DateTime dv = DataUtil.CDate(i_data.DataTable.Rows[i - 1][j]);
                                string v = "";
                                if (dv != null && dv.Year != 1)
                                {
                                    v = string.Format("{0:yyyy/MM/dd}", dv);
                                }
                                strBuilder.Append(convCsvValue(v));
                            }
                            else
                            {
                                string tv = convCsvValue(DataUtil.CStr(i_data.DataTable.Rows[i - 1][j]));

                                if (tv.Length == 8)
                                {
                                    string s = tv.Insert(4, "/");
                                    s = s.Insert(7, "/");

                                    try
                                    {
                                        DateTime d = Convert.ToDateTime(s);
                                        tv = s;

                                    }
                                    catch
                                    {

                                    }
                                }
                                else if (tv.Length == 6)
                                {
                                    string s = tv + "01";
                                    s = s.Insert(4, "/");
                                    s = s.Insert(7, "/");

                                    try
                                    {
                                        DateTime d = Convert.ToDateTime(s);
                                        tv = tv.Insert(4, "/");

                                    }
                                    catch
                                    {

                                    }
                                }

                                strBuilder.Append(tv);
                            }
                        }


                        strBuilder.Append(",");
                    }

                    if (i == 0)
                    {
                        //
                        strBuilder.Append(i_setting.ExportField[i_data.DataTable.Columns[i_data.DataTable.Columns.Count - 1].ColumnName]);
                    }
                    else
                    {
                        //
                        if (i_data.DataTable.Columns[i_data.DataTable.Columns.Count - 1].DataType.ToString() == "System.DateTime")
                        {
                            DateTime dv = DataUtil.CDate(i_data.DataTable.Rows[i - 1][i_data.DataTable.Columns.Count - 1]);
                            string v = "";
                            if (dv != null)
                            {
                                v = string.Format("{0:yyyy/MM/dd}", dv);
                            }
                            strBuilder.Append(convCsvValue(v));
                        }
                        else
                        {
                            string tv = convCsvValue(DataUtil.CStr(i_data.DataTable.Rows[i - 1][i_data.DataTable.Columns.Count - 1]));

                            if (tv.Length == 8)
                            {
                                string s = tv.Insert(4, "/");
                                s = s.Insert(7, "/");

                                try
                                {
                                    DateTime d = Convert.ToDateTime(s);
                                    tv = s;

                                }
                                catch
                                {

                                }
                            }
                            else if (tv.Length == 6)
                            {
                                string s = tv + "01";
                                s = s.Insert(4, "/");
                                s = s.Insert(7, "/");

                                try
                                {
                                    DateTime d = Convert.ToDateTime(s);
                                    tv = tv.Insert(4, "/");

                                }
                                catch
                                {

                                }
                            }

                            strBuilder.Append(tv);
                        }
                    }

                    if (i != i_data.DataTable.Rows.Count)
                    {
                        sw.WriteLine(strBuilder);
                    }
                    else
                    {
                        sw.Write(strBuilder);
                    }
                }
                // 
                sw.Close();


                return result;

            }
            catch
            {
                return result;
            }
            finally
            {
                if (sw != null)
                {
                    // 
                    sw.Close();
                }
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="strInData"></param>
        /// <returns></returns>
        public static string convCsvValue(string strInData)
        {

            bool blnDblquateFlg = false;
            //*
            if (
                //(strInData.IndexOf('"') > 0) ||
                (strInData.IndexOf('\r') > 0) ||
                (strInData.IndexOf('\n') > 0) ||
                (strInData.IndexOf(',') > 0))
            {
                blnDblquateFlg = true;
            }
            //
            if (strInData.Length > 0)
            {
                if ((strInData.Substring(0, 1) == " ") ||
                    (strInData.Substring(strInData.Length - 1, 1) == " "))
                {
                    blnDblquateFlg = true;
                }
            }

            //
            strInData = strInData.Replace("\r\n", "\n");

            //
            if (blnDblquateFlg)
            {
                strInData = strInData.Replace("\"", "\"\"");
                strInData = "\"" + strInData + "\"";
            }
            return strInData;
        }

        public static DataExportResult ExportToExcel(DataExportSetting i_setting, PageViewResult i_data)
        {
            DataExportResult result = new DataExportResult();
            result.ResultType = "Success";

            if (i_data.DataTable.Rows.Count == 0)
            {
                result.ResultType = "NoData";
                return result;
            }

            String key = Guid.NewGuid().ToString().Replace("-", "");
            result.ResourceId = key;
            result.Path = "/App_Resource" + "/" + key + ".xls";
            result.Extension = "xls";


            DataTableToExcel(i_setting, i_data.DataTable, null, i_setting.Dir + "\\" + key + ".xls");

            return result;
        }

        public static DataImportResult DataImport(DataImportSetting i_setting)
        {
            DataImportResult result = new DataImportResult();
            result.ResultType = "Success";

            DyEntityLogic logic = new DyEntityLogic();


            //file check

            //entity info 
            Y_Entity entity = logic.GetEntity(i_setting.ProjID, i_setting.EntityName);

            //get file data
            DataTable dt = ExcelInput(i_setting, result, entity);

            //check file data
            if (result.ResultType == "Success")
            {
                CheckData(i_setting, result, dt, entity);
            }


            //save data
            if (result.ResultType == "Success")
            {
                SaveData(i_setting, result, dt, entity);
            }

            return result;
        }

        #region  export


        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void DataTableToExcel(DataExportSetting i_setting, DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = DataTableToExcel(i_setting, dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }



        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream DataTableToExcel(DataExportSetting i_setting, DataTable dtSource, string strHeaderText)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)workbook.CreateSheet();

            #region 右击文件 属性信息
            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "Company";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "Author"; //填加xls文件作者信息
                si.ApplicationName = "ApplicationName"; //填加xls文件创建程序信息
                si.LastAuthor = "LastAuthor"; //填加xls文件最后保存者信息
                si.Comments = "Comments"; //填加xls文件作者信息
                si.Title = i_setting.FileName; //填加xls文件标题信息
                si.Subject = i_setting.FileName;//填加文件主题信息
                si.CreateDateTime = System.DateTime.Now;
                workbook.SummaryInformation = si;
            }
            #endregion

            HSSFCellStyle dateStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            //有边框
            dateStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            dateStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            dateStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            dateStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

            HSSFCellStyle cellStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            //有边框
            cellStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            cellStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

            HSSFCellStyle numStyle = (HSSFCellStyle)workbook.CreateCellStyle();
            //有边框
            numStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            numStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            numStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            numStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            numStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Right;


            HSSFDataFormat format = (HSSFDataFormat)workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy/mm/dd");

            HSSFDataFormat formatNum = (HSSFDataFormat)workbook.CreateDataFormat();
            numStyle.DataFormat = formatNum.GetFormat("#,##0");



            int colIndex = 0;
            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            foreach (DataColumn item in dtSource.Columns)
            {
                if (i_setting.ExportField.ContainsKey(item.ColumnName) == false)
                {
                    continue;
                }

                arrColWidth[colIndex] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
                colIndex++;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式
                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = (HSSFSheet)workbook.CreateSheet();
                    }

                    #region 表头及样式
                    {
                        colIndex = 0;
                        if (string.IsNullOrEmpty(strHeaderText) == false)
                        {
                            HSSFRow headerRow = (HSSFRow)sheet.CreateRow(0);
                            headerRow.HeightInPoints = 25;
                            headerRow.CreateCell(0).SetCellValue(strHeaderText);

                            HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                            //  headStyle.Alignment = CellHorizontalAlignment.CENTER;
                            HSSFFont font = (HSSFFont)workbook.CreateFont();
                            font.FontHeightInPoints = 20;
                            font.Boldweight = 700;
                            headStyle.SetFont(font);
                            // sheet.AddMergedRegion(new Region(0, 0, 0, dtSource.Columns.Count - 1));
                            //headerRow.Dispose();
                            rowIndex = 1;
                        }
                    }
                    #endregion


                    #region 列头及样式
                    {
                        HSSFRow headerRow = (HSSFRow)sheet.CreateRow(rowIndex);
                        HSSFCellStyle headStyle = (HSSFCellStyle)workbook.CreateCellStyle();
                        //headStyle.Alignment = CellHorizontalAlignment.CENTER;
                        HSSFFont font = (HSSFFont)workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);

                        //有边框
                        headStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
                        headStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
                        headStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
                        headStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

                        //
                        headStyle.FillForegroundColor = HSSFColor.PaleBlue.Index;
                        headStyle.FillPattern = NPOI.SS.UserModel.FillPattern.SolidForeground;
                        headStyle.FillBackgroundColor = HSSFColor.PaleBlue.Index;

                        foreach (DataColumn column in dtSource.Columns)
                        {
                            if (i_setting.ExportField.ContainsKey(column.ColumnName) == false)
                            {
                                continue;
                            }
                            headerRow.CreateCell(colIndex).SetCellValue(i_setting.ExportField[column.ColumnName]);
                            headerRow.GetCell(colIndex).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(colIndex, (arrColWidth[colIndex] + 1) * 256);

                            colIndex++;
                        }
                        // headerRow.Dispose();
                    }
                    #endregion

                    rowIndex++;
                }
                #endregion


                #region 填充内容
                HSSFRow dataRow = (HSSFRow)sheet.CreateRow(rowIndex);
                colIndex = 0;
                foreach (DataColumn column in dtSource.Columns)
                {
                    if (i_setting.ExportField.ContainsKey(column.ColumnName) == false)
                    {
                        continue;
                    }
                    HSSFCell newCell = (HSSFCell)dataRow.CreateCell(colIndex);

                    newCell.CellStyle = cellStyle;

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型


                            if (column.Caption.StartsWith("beforeType:") && 1>2)
                            {
                                string beforeType = column.Caption.Replace("beforeType:", "");

                                if (beforeType == "System.Int16"
                                    || beforeType == "System.Int32"
                                    || beforeType == "System.Int64"
                                    || beforeType == "System.Byte")
                                {
                                    //double doubV2 = 0;
                                    //double.TryParse(drValue, out doubV2);
                                    //newCell.SetCellValue(doubV2);
                                    if (string.IsNullOrEmpty(drValue) == false)
                                    {
                                        int intV2 = 0;
                                        int.TryParse(drValue.Replace(",", ""), out intV2);

                                        newCell.SetCellValue(intV2);
                                    }
                                    newCell.CellStyle = numStyle;
                                }

                                else if (beforeType == "System.Decimal"
                                     || beforeType == "System.Double")
                                {
                                    //double doubV2 = 0;
                                    //double.TryParse(drValue, out doubV2);
                                    //newCell.SetCellValue(doubV2);
                                    if (string.IsNullOrEmpty(drValue) == false)
                                    {
                                        double doubV2 = 0;
                                        double.TryParse(drValue.Replace(",", ""), out doubV2);

                                        newCell.SetCellValue(doubV2);
                                    }
                                    newCell.CellStyle = numStyle;
                                }
                            }
                            else if (drValue.Length == 8)
                            {
                                string s = drValue.Insert(4, "/");
                                s = s.Insert(7, "/");

                                try
                                {
                                    DateTime d = Convert.ToDateTime(s);
                                    newCell.SetCellValue(s);

                                }
                                catch
                                {
                                    newCell.SetCellValue(drValue);
                                }
                            }
                            else if (drValue.Length == 6)
                            {
                                string s = drValue + "01";
                                s = s.Insert(4, "/");
                                s = s.Insert(7, "/");

                                try
                                {
                                    DateTime d = Convert.ToDateTime(s);
                                    newCell.SetCellValue(drValue.Insert(4, "/"));

                                }
                                catch
                                {
                                    newCell.SetCellValue(drValue);
                                }
                            }
                            else
                            {
                                newCell.SetCellValue(drValue);
                            }
                            break;
                        case "System.DateTime"://日期类型
                            System.DateTime dateV;
                            System.DateTime.TryParse(drValue, out dateV);
                            if (dateV.Year != 1)
                            {
                                //newCell.SetCellValue(dateV);
                                newCell.SetCellValue(string.Format("{0:yyyy/MM/dd}", dateV));

                            }

                            //newCell.CellStyle = dateStyle;//格式化显示
                            break;
                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            bool.TryParse(drValue, out boolV);
                            newCell.SetCellValue(boolV);
                            break;
                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            int.TryParse(drValue, out intV);
                            newCell.SetCellValue(intV);
                            newCell.CellStyle = numStyle;
                            break;
                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            double.TryParse(drValue, out doubV);
                            newCell.SetCellValue(doubV);
                            newCell.CellStyle = numStyle;
                            break;
                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;
                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                    colIndex++;
                }
                #endregion

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;

                //sheet.Dispose();
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }
        #endregion

        #region import
        /// <summary>
        /// Excel转换DataTable
        /// </summary>
        /// <param name="FilePath">文件的绝对路径</param>
        /// <returns>DataTable</returns>
        public static DataTable ExcelInput(DataImportSetting i_setting, DataImportResult result, Y_Entity i_entity)
        {

            //根据路径通过已存在的excel来创建HSSFWorkbook，即整个excel文档
            HSSFWorkbook workbook = new HSSFWorkbook(File.Open(i_setting.FilePath, FileMode.Open));
            HSSFSheet sheet = (HSSFSheet)workbook.GetSheetAt(0);
            //获取excel的第一个sheet


            //获取Excel的最大行数
            int rowsCount = sheet.PhysicalNumberOfRows;
            //为保证Table布局与Excel一样，这里应该取所有行中的最大列数（需要遍历整个Sheet）。
            //为少一交全Excel遍历，提高性能，我们可以人为把第0行的列数调整至所有行中的最大列数。
            int colsCount = sheet.GetRow(0).PhysicalNumberOfCells;

            EntityAdapter ea = new EntityAdapter(new EntityRequest(i_entity.ProjID, i_setting.UpdateUser, ""), i_entity.EntityName);
            List<Y_EntityField> keyFieldList = ea.GetKeyFields();

            List<string> keyFields = new List<string>();
            List<string> keyFieldsInFile = new List<string>();
            foreach (Y_EntityField field in keyFieldList)
            {
                keyFields.Add(field.FieldName);
            }

            //col position dic
            Dictionary<int, string> colPositionDic = new Dictionary<int, string>();

            for (int i = 0; i < colsCount; i++)
            {
                string colTitle = sheet.GetRow(0).GetCell(i).ToString();

                //find by fieldName
                var queryFieldName = from field in i_entity.FieldList
                                     where field.FieldName == colTitle
                                     select field;

                if (queryFieldName.Count() == 0)
                {
                    queryFieldName = from field in i_entity.FieldList
                                     where field.FieldDesc == colTitle
                                     select field;
                }

                if (queryFieldName.Count() > 0)
                {
                    colPositionDic.Add(i, queryFieldName.First().FieldName);
                    if (keyFields.Contains(queryFieldName.First().FieldName))
                    {
                        keyFieldsInFile.Add(queryFieldName.First().FieldName);
                    }
                }
            }

            if (keyFieldsInFile.Count != keyFields.Count)
            {

                result.ResultType = "FileError";
            }

            string tableName = i_entity.EntityName;
            DataTable dt = new DataTable();
            dt.TableName = tableName;
            List<Y_EntityField> fields = i_entity.FieldList;

            foreach (Y_EntityField p in fields)
            {
                Type tp;
                if (p.DataType == "Nvarchar") tp = typeof(string);
                else if (p.DataType == "Varchar") tp = typeof(string);
                else if (p.DataType == "SmallInt") tp = typeof(int);
                else if (p.DataType == "Int") tp = typeof(int);
                else if (p.DataType == "Number") tp = typeof(decimal);
                else if (p.DataType == "DateTime") tp = typeof(DateTime);
                else if (p.DataType == "Bit") tp = typeof(bool);
                else tp = typeof(string);

                DataColumn dc = new DataColumn(p.FieldName, tp);
                dt.Columns.Add(dc);
            }

            for (int x = 1; x < rowsCount; x++)
            {
                //DataRow dr = table.NewRow();
                //for (int y = 0; y < colsCount; y++)
                //{
                //    dr[y] = sheet.GetRow(x).GetCell(y).ToString();
                //}
                //table.Rows.Add(dr);

                DataRow dr = dt.NewRow();
                for (int y = 0; y < colsCount; y++)
                {
                    object v = sheet.GetRow(x).GetCell(y).ToString();

                    if (colPositionDic.ContainsKey(y) == false)
                    {
                        continue;
                    }

                    bool isNullable = fields.Find(f => f.FieldName == colPositionDic[y]).IsNullable;

                    if (dt.Columns[colPositionDic[y]].DataType == typeof(string))
                    {

                        dr[colPositionDic[y]] = DataUtil.CStr(v);
                    }
                    else if (dt.Columns[colPositionDic[y]].DataType == typeof(int))
                    {
                        if (isNullable && DataUtil.CStr(v) == "")
                        {
                            dr[colPositionDic[y]] = DBNull.Value;
                        }
                        else
                        {
                            dr[colPositionDic[y]] = DataUtil.CInt(v);
                        }
                    }
                    else if (dt.Columns[colPositionDic[y]].DataType == typeof(decimal))
                    {
                        if (isNullable && DataUtil.CStr(v) == "")
                        {
                            dr[colPositionDic[y]] = DBNull.Value;
                        }
                        else
                        {
                            dr[colPositionDic[y]] = DataUtil.CDec(v);
                        }
                    }
                    else if (dt.Columns[colPositionDic[y]].DataType == typeof(DateTime))
                    {
                        v = sheet.GetRow(x).GetCell(y).DateCellValue.ToShortDateString();
                        if (isNullable && DataUtil.CStr(v) == "")
                        {
                            dr[colPositionDic[y]] = DBNull.Value;
                        }
                        else
                        {
                            dr[colPositionDic[y]] = DataUtil.CDate(v);
                        }
                    }
                    else if (dt.Columns[colPositionDic[y]].DataType == typeof(bool))
                    {
                        if (isNullable && DataUtil.CStr(v) == "")
                        {
                            dr[colPositionDic[y]] = DBNull.Value;
                        }
                        else
                        {
                            dr[colPositionDic[y]] = DataUtil.CBool(v);
                        }
                    }
                }

                dt.Rows.Add(dr);
            }

            sheet = null;
            workbook = null;

            if (dt.Rows.Count == 0)
            {
                result.ResultType = "NoData";
            }

            return dt;
        }

        public static void CheckData(DataImportSetting i_setting, DataImportResult result, DataTable i_dt, Y_Entity i_entity)
        {
            EntityAdapter ea = new EntityAdapter(new EntityRequest(i_entity.ProjID, i_setting.UpdateUser, ""), i_entity.EntityName);

            List<DataIOResultMessage> messages = new List<DataIOResultMessage>();

            int index = 0;
            foreach (DataRow dataItem in i_dt.Rows)
            {
                index++;
                List<Y_EntityField> keyFieldList = ea.GetKeyFields();

                string[] keyFields = new string[keyFieldList.Count];
                object[] keys = new object[keyFieldList.Count];

                for (int i = 0; i < keyFieldList.Count; i++)
                {
                    keyFields[i] = keyFieldList[i].FieldName;
                    keys[i] = dataItem[keyFieldList[i].FieldName];

                    // 主键不全
                    if (string.IsNullOrEmpty(DataUtil.CStr(dataItem[keyFieldList[i].FieldName])))
                    {
                        DataIOResultMessage msg = new DataIOResultMessage();
                        msg.LineNo = "" + index;
                        msg.Type = "NoKey";
                        messages.Add(msg);
                    }
                }

                if (ea.HasData(keyFields, keys))
                {
                    DataIOResultMessage msg = new DataIOResultMessage();
                    msg.LineNo = "" + index;
                    msg.Type = "DataExisted";
                    messages.Add(msg);
                }
            }

            if (messages.Count > 0)
            {
                result.Messages = messages;
                result.ResultType = "DataError";
            }
        }

        private static void SaveData(DataImportSetting i_setting, DataImportResult result, DataTable i_dt, Y_Entity i_entity)
        {
            DataSet ds = new DataSet();
            ds.Tables.Add(i_dt);

            TableSaveRequest saveRequset = new TableSaveRequest();
            saveRequset.SaveMode = TableSaveRequest.EnumSaveMode.SaveChange;
            saveRequset.SaveSubTables = false;

            EntityAdapter ea = new EntityAdapter(new EntityRequest(i_entity.ProjID, i_setting.UpdateUser, ""), i_entity.EntityName);

            Result saveResult = ea.SaveData(saveRequset, ds);

            List<DataIOResultMessage> messages = new List<DataIOResultMessage>();
            DataIOResultMessage msg = new DataIOResultMessage();
            messages.Add(msg);
            if (saveResult.ErrorKey == "error")
            {

                msg.LineNo = "-";
                msg.Type = "SaveError";
                msg.Msg = saveResult.Message;

                result.Messages = messages;
                result.ResultType = "SaveError";
            }
            else
            {
                msg.LineNo = "-";
                msg.Type = "Success";
                msg.Msg = saveResult.Message;

                result.Messages = messages;
                result.ResultType = "Success";
            }
        }

        #endregion
    }


    public class DataExportSetting
    {
        public Dictionary<string, string> ExportField { get; set; }

        public String FileName { get; set; }

        public String Dir { get; set; }
    }

    public class DataExportResult
    {

        public String FileName { get; set; }

        public String Extension { get; set; }

        public String Path { get; set; }

        public String ResultType { get; set; }

        public String ResourceId { get; set; }

    }

    public class DataImportSetting
    {
        public Dictionary<string, string> ImportField { get; set; }

        public String FilePath { get; set; }

        public String FileType { get; set; }

        public int ProjID { get; set; }

        public String UpdateUser { get; set; }


        public string EntityName { get; set; }
    }

    public class DataImportResult
    {

        public String ResultType { get; set; }

        public String ResourceId { get; set; }

        public List<DataIOResultMessage> Messages { get; set; }

    }

    public class DataIOResultMessage
    {

        public String LineNo { get; set; }

        public String Type { get; set; }

        public String Msg { get; set; }

    }
}
