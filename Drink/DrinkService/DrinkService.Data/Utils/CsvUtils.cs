using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using DrinkService.Models;
using DrinkService.Data.ViewModels;

namespace DrinkService.Utils
{

    public class CSVAttribute : System.Attribute
    {

        public bool Output = false;

        public int FieldIndex = 0;


        public CSVAttribute()
        {

        }

    }

    public class LinkAttribute : System.Attribute
    {
        public string linkText;
        public string routeReg;

        public LinkAttribute()
        {

        }
    }

    public class CsvUtils
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="i_fileName"></param>
        /// <param name="i_data"></param>
        public static bool DataTableToCsv(string i_fileName, DataTable i_data)
        {
            StreamWriter sw = null;
            try
            {
                string dir = i_fileName.Substring(0, i_fileName.LastIndexOf("\\"));
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // CSV 
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;//Encoding.GetEncoding("Shift-JIS");
                sw = new StreamWriter(i_fileName, false, encoding);

                // 
                for (int i = 0; i <= i_data.Rows.Count; i++)
                {
                    StringBuilder strBuilder = new StringBuilder();

                    for (int j = 0; j < i_data.Columns.Count - 1; j++)
                    {
                        if (i == 0)
                        {
                            //
                            if (string.IsNullOrEmpty(i_data.Columns[j].Caption) == false)
                            {
                                strBuilder.Append(i_data.Columns[j].Caption);
                            }
                            else
                            {
                                strBuilder.Append(i_data.Columns[j].ColumnName);
                            }
                        }
                        else
                        {
                            //
                            strBuilder.Append(convCsvValue(CStr(i_data.Rows[i - 1][j], null)));
                        }


                        strBuilder.Append(",");
                    }

                    if (i == 0)
                    {
                        //
                        strBuilder.Append(i_data.Columns[i_data.Columns.Count - 1].Caption);
                    }
                    else
                    {
                        //
                        strBuilder.Append(convCsvValue(CStr(i_data.Rows[i - 1][i_data.Columns.Count - 1], null)));
                    }

                    if (i != i_data.Rows.Count)
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


                return true;

            }
            catch
            {
                return false;
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
        /// <param name="i_fileName"></param>
        /// <param name="i_data"></param>
        public static bool ModlesToCsv<T>(string i_fileName, List<T> i_data)
        {

            if (i_data == null || i_data.Count == 0)
            {
                return false;
            }

            StreamWriter sw = null;
            try
            {
                string dir = i_fileName.Substring(0, i_fileName.LastIndexOf("\\"));
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // CSV 
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                sw = new StreamWriter(i_fileName, false, encoding);

                //header info
                Object temp = i_data[0];

                PropertyInfo[] hearderFields = temp.GetType().GetProperties();
                StringBuilder hearderBuilder = new StringBuilder();

                int index = 0;
                foreach (PropertyInfo hField in hearderFields)
                {
                    if (hField.GetCustomAttribute(typeof(CSVAttribute)) != null && ((CSVAttribute)hField.GetCustomAttribute(typeof(CSVAttribute))).Output == false)
                    {
                        continue;
                    }
                    object[] values = hField.GetCustomAttributes(typeof(DisplayAttribute), true);
                    if (values.Length == 1)
                    {
                        DisplayAttribute description = values[0] as DisplayAttribute;
                        if (index == 0)
                        {
                            hearderBuilder.Append(description.Name);
                        }
                        else
                        {
                            hearderBuilder.Append("," + description.Name);
                        }
                    }
                    else
                    {
                        if (index == 0)
                        {
                            hearderBuilder.Append(hField.Name);
                        }
                        else
                        {
                            hearderBuilder.Append("," + hField.Name);
                        }
                    }

                    index++;
                }
                sw.WriteLine(hearderBuilder);

                // 
                for (int i = 0; i < i_data.Count; i++)
                {
                    PropertyInfo[] fieldsTemp = i_data[i].GetType().GetProperties();

                    List<PropertyInfo> fields = new List<PropertyInfo>();

                    foreach (PropertyInfo info in fieldsTemp)
                    {
                        if (info.GetCustomAttribute(typeof(CSVAttribute)) != null && ((CSVAttribute)info.GetCustomAttribute(typeof(CSVAttribute))).Output == false)
                        {
                            continue;
                        }

                        fields.Add(info);
                    }


                    StringBuilder strBuilder = new StringBuilder();

                    for (int j = 0; j < fields.Count - 1; j++)
                    {

                        strBuilder.Append(convCsvValue(CStr(fields[j].GetValue(i_data[i]), fields[j])));


                        strBuilder.Append(",");
                    }

                    strBuilder.Append(convCsvValue(CStr(fields[fields.Count - 1].GetValue(i_data[i]), fields[fields.Count - 1])));


                    if (i != i_data.Count - 1)
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


                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
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
        /// <param name="i_fileName"></param>
        /// <param name="i_data"></param>
        public static bool ModlesToSql<T>(string i_fileName, List<T> i_data,string tableName)
        {

            if (i_data == null || i_data.Count == 0)
            {
                return false;
            }

            StreamWriter sw = null;

            string sqlInSert = @"INSERT INTO {0} ({1}) VALUES ({2});";

            try
            {
                string dir = i_fileName.Substring(0, i_fileName.LastIndexOf("\\"));
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // CSV 
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                sw = new StreamWriter(i_fileName, false, encoding);

                //header info
                Object temp = i_data[0];

                PropertyInfo[] hearderFields = temp.GetType().GetProperties();
                StringBuilder hearderBuilder = new StringBuilder();

                int index = 0;
                foreach (PropertyInfo hField in hearderFields)
                {
                    if (hField.GetCustomAttribute(typeof(CSVAttribute)) != null && ((CSVAttribute)hField.GetCustomAttribute(typeof(CSVAttribute))).Output == false)
                    {
                        continue;
                    }
                    object[] values = hField.GetCustomAttributes(typeof(DisplayAttribute), true);
                    if (values.Length == 1)
                    {
                        DisplayAttribute description = values[0] as DisplayAttribute;
                        if (index == 0)
                        {
                            hearderBuilder.Append(hField.Name);
                        }
                        else
                        {
                            hearderBuilder.Append("," + hField.Name);
                        }
                    }
                    else
                    {
                        if (index == 0)
                        {
                            hearderBuilder.Append(hField.Name);
                        }
                        else
                        {
                            hearderBuilder.Append("," + hField.Name);
                        }
                    }

                    index++;
                }

                //sw.WriteLine(hearderBuilder);

                // 
                for (int i = 0; i < i_data.Count; i++)
                {
                    PropertyInfo[] fieldsTemp = i_data[i].GetType().GetProperties();

                    List<PropertyInfo> fields = new List<PropertyInfo>();

                    foreach (PropertyInfo info in fieldsTemp)
                    {
                        if (info.GetCustomAttribute(typeof(CSVAttribute)) != null && ((CSVAttribute)info.GetCustomAttribute(typeof(CSVAttribute))).Output == false)
                        {
                            continue;
                        }

                        fields.Add(info);
                    }


                    StringBuilder strBuilder = new StringBuilder();

                    for (int j = 0; j < fields.Count - 1; j++)
                    {

                        strBuilder.Append("'"+convCsvValue(CStr(fields[j].GetValue(i_data[i]), fields[j]))+"'");


                        strBuilder.Append(",");
                    }

                    strBuilder.Append("'" + convCsvValue(CStr(fields[fields.Count - 1].GetValue(i_data[i]), fields[fields.Count - 1])) + "'");


                    string sql = string.Format(sqlInSert, tableName, hearderBuilder.ToString(), strBuilder.ToString());

                    if (i != i_data.Count - 1)
                    {
                        sw.WriteLine(sql);
                    }
                    else
                    {
                        sw.Write(sql);
                    }
                }
                // 
                sw.Close();


                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
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

        public static List<T> CsvToModel<T>(string i_fileName, ref List<ImportErrorViewModel> errorList, List<ICSVChecker<T>> checkers,bool hasHeader)
        {
            List<T> list = new List<T>();

            if (!File.Exists(i_fileName))
            {
                return list;
            }

            PropertyInfo[] fields = typeof(T).GetProperties();


            try
            {
                Encoding code = GetEncoding(i_fileName);

                FileStream fs = new FileStream(i_fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                StreamReader sr = new StreamReader(fs, code);

                string strLine = "";
                string[] aryLine;
                int columnCount = 0;
                //bool IsFirst = true;
                bool IsFirst = hasHeader;
                int index = 0;

                List<string> ExistError = new List<string>();

                Dictionary<string, int> filedIndex = new Dictionary<string, int>();

                if (hasHeader == false) {
                    for (int j = 0; j < fields.Length; j++)
                    {
                        filedIndex.Add(fields[j].Name, ((CSVAttribute)fields[j].GetCustomAttribute(typeof(CSVAttribute), true)).FieldIndex);
                    }
                }

                while ((strLine = sr.ReadLine()) != null)
                {
                    aryLine = strLine.Split(',');
                    columnCount = aryLine.Length;
                    if (IsFirst == true)
                    {
                        IsFirst = false;
                        
                        for (int i = 0; i < columnCount; i++)
                        {
                            for (int j = 0; j < fields.Length; j++)
                            {
                                if (aryLine[i] == ((DisplayAttribute)fields[j].GetCustomAttribute(typeof(DisplayAttribute), true)).Name)
                                {
                                    if (filedIndex.ContainsKey(fields[j].Name))
                                    {
                                        ImportErrorViewModel errorModel = new ImportErrorViewModel();

                                        errorModel.ErrorField = ((DisplayAttribute)fields[j].GetCustomAttribute(typeof(DisplayAttribute), true)).Name;
                                        errorModel.ErrorType = "FieldSameExistError";
                                        errorModel.ErrorDetail = "「" + errorModel.ErrorField + "」列が複数存在する。";

                                        CsvUtils.AddError(ref errorList, errorModel);
                                    }
                                    else
                                    {
                                        filedIndex.Add(fields[j].Name, i);
                                    }

                                }
                            }
                        }
                    }
                    else
                    {
                        if (aryLine.Length < columnCount)
                        {
                            ImportErrorViewModel errorModel = new ImportErrorViewModel();

                            errorModel.ErrorField = "";
                            errorModel.ErrorRow = index;
                            errorModel.ErrorType = "DataRowError";
                            errorModel.ErrorDetail = "データの行が不完全である。";

                            CsvUtils.AddError(ref errorList, errorModel);
                        }

                        T data = (T)Activator.CreateInstance(typeof(T));

                        for (int j = 0; j < fields.Length; j++)
                        {
                            PropertyInfo field = fields[j];
                            Type type = field.PropertyType;

                            if (filedIndex.ContainsKey(field.Name))
                            {
                                if (filedIndex[field.Name] >= aryLine.Length)
                                {
                                    continue;
                                }
                                field.SetValue(data, aryLine[filedIndex[field.Name]].Trim());

                                //最大の文字長チェック
                                if (field.GetCustomAttribute(typeof(StringLengthAttribute)) != null)
                                {
                                    int len = ((StringLengthAttribute)field.GetCustomAttribute(typeof(StringLengthAttribute))).MaximumLength;
                                    if (aryLine[filedIndex[field.Name]].Trim().Length > len)
                                    {
                                        ImportErrorViewModel errorModel = new ImportErrorViewModel();

                                        errorModel.ErrorField = ((DisplayAttribute)fields[j].GetCustomAttribute(typeof(DisplayAttribute), true)).Name;
                                        errorModel.ErrorRow = index;
                                        errorModel.ErrorType = "FieldLengthError";
                                        errorModel.ErrorDetail = "「" + errorModel.ErrorField + "」列文字の長さは" + len + "ケタで制限されている。";

                                        CsvUtils.AddError(ref errorList, errorModel);
                                    }
                                }
                            }
                        }

                        if (checkers != null)
                        {
                            foreach (ICSVChecker<T> checker in checkers)
                            {
                                checker.doCheck(ref errorList, data, index);
                            }
                        }

                        index++;
                        list.Add(data);
                    }
                }

                for (int j = 0; j < fields.Length; j++)
                {
                    PropertyInfo field = fields[j];
                    Type type = field.PropertyType;
                    if (filedIndex.ContainsKey(field.Name) == false)
                    {
                        if (field.GetCustomAttribute(typeof(RequiredAttribute)) != null && ((RequiredAttribute)field.GetCustomAttribute(typeof(RequiredAttribute))).AllowEmptyStrings == false)
                        {
                            if (!ExistError.Contains(((DisplayAttribute)fields[j].GetCustomAttribute(typeof(DisplayAttribute), true)).Name))
                            {
                                ExistError.Add(((DisplayAttribute)fields[j].GetCustomAttribute(typeof(DisplayAttribute), true)).Name);
                            }
                        }
                    }
                }

                if (ExistError.Count > 0)
                {
                    foreach (string field in ExistError)
                    {
                        ImportErrorViewModel errorModel = new ImportErrorViewModel();

                        errorModel.ErrorField = field;
                        errorModel.ErrorType = "FieldNotExistError";
                        errorModel.ErrorDetail = "「" + field + "」列が存在しません。";
                        errorList.Add(errorModel);
                    }
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return list;
        }

        public static void AddError(ref List<ImportErrorViewModel> errorList, ImportErrorViewModel error)
        {
            bool errorFlag = false;
            foreach (ImportErrorViewModel model in errorList)
            {
                if (model.ErrorField == error.ErrorField && model.ErrorType == error.ErrorType)
                {
                    errorFlag = true;
                    break;
                }
            }

            if (!errorFlag)
            {
                errorList.Add(error);
            }
        }

        public static Encoding GetEncoding(string filePath)
        {
            if (filePath == null)
            {
                throw new ArgumentNullException("filePath");
            }
            Encoding encoding1 = Encoding.Default;
            if (File.Exists(filePath))
            {
                try
                {
                    using (FileStream stream1 = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    {
                        if (stream1.Length > 0)
                        {
                            using (StreamReader reader1 = new StreamReader(stream1, true))
                            {
                                char[] chArray1 = new char[1];
                                reader1.Read(chArray1, 0, 1);
                                encoding1 = reader1.CurrentEncoding;
                                reader1.BaseStream.Position = 0;
                                if (encoding1 == Encoding.UTF8)
                                {
                                    byte[] buffer1 = encoding1.GetPreamble();
                                    if (stream1.Length >= buffer1.Length)
                                    {
                                        byte[] buffer2 = new byte[buffer1.Length];
                                        stream1.Read(buffer2, 0, buffer2.Length);
                                        for (int num1 = 0; num1 < buffer2.Length; num1++)
                                        {
                                            if (buffer2[num1] != buffer1[num1])
                                            {
                                                encoding1 = Encoding.GetEncoding("Shift-JIS");
                                                break;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        encoding1 = Encoding.GetEncoding("Shift-JIS");
                                    }
                                }
                            }
                        }
                    }
                }
                catch (Exception exception1)
                {
                    throw;
                }
                if (encoding1 == null)
                {
                    encoding1 = Encoding.UTF8;
                }
            }
            return encoding1;
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

        private static String CStr(object obj, PropertyInfo info)
        {
            if (obj != null)
            {
                if (info != null && info.GetCustomAttribute(typeof(DisplayFormatAttribute)) != null)
                {
                    string formatStr = ((DisplayFormatAttribute)info.GetCustomAttribute(typeof(DisplayFormatAttribute))).DataFormatString;

                    return string.Format(formatStr, (DateTime)obj);
                }

                return obj.ToString();
            }
            return "";
        }
    }

    public interface ICSVChecker<T>
    {
        void doCheck(ref List<ImportErrorViewModel> errorList, T data, int rowNum);
    }

    public class ImportErrorViewModel
    {

        [Display(Name = "エラーの種類")]
        public string ErrorType { get; set; }

        [Display(Name = "エラーフィールド")]
        public string ErrorField { get; set; }

        [Display(Name = "エラー詳細")]
        public string ErrorDetail { get; set; }

        [Display(Name = "エラー行番号")]
        public int ErrorRow { get; set; }
    }
}