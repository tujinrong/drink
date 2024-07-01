using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.FD.Report
{
    public class FDReport
    {
        public string ItemTypeFieldName;

        public Dictionary<string, List<FDReportItemDefined>> Defines;

        public List<object> DataSourceList;

        public void CreateFD(List<object> dataSourceList,string i_fileName)
        {
            DataSourceList = dataSourceList;
            //StringBuilder fdStr = new StringBuilder();

            StreamWriter sw = null;
            try
            {
                string dir = i_fileName.Substring(0, i_fileName.LastIndexOf("\\"));
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }

                // CSV 
                System.Text.Encoding encoding = Encoding.GetEncoding("Shift-JIS");
                sw = new StreamWriter(i_fileName, false, encoding);

                foreach (object data in DataSourceList) {
                    //fdStr.AppendLine(CreateFDLine(data));
                    sw.WriteLine(CreateFDLine(data));
                }

                sw.Close();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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


        private string CreateFDLine(object data) {

            StringBuilder fdLineStr = new StringBuilder();

            //FDReportItemDefined List
            Type type = data.GetType();
            PropertyInfo property = type.GetProperty(ItemTypeFieldName);
            string key = DataUtil.CStr(property.GetValue(data, null));

            if (Defines.ContainsKey(key) == false) {
                return "";
            }

            List<FDReportItemDefined> defineInfos = Defines[key];

            foreach (FDReportItemDefined item in defineInfos) {

                property = type.GetProperty(item.Field);
                string value = DataUtil.CStr(property.GetValue(data, null));

                if (value.Length > item.ByteLength) {
                    value = value.Substring(0, item.ByteLength);
                }

                string fdStr = string.Empty;
                if (item.PadDirection == 0)
                {
                    fdStr = value.PadRight(item.ByteLength, item.PadChar);
                }
                else 
                {
                    fdStr = value.PadLeft(item.ByteLength, item.PadChar);
                }

                fdLineStr.Append(fdStr);
            }



            return fdLineStr.ToString() ;
        } 

    }

    public class FDReportItemDefined {

        /// <summary>
        /// フィールド
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 属性
        /// </summary>
        public string Property { get; set; }

        /// <summary>
        /// バイト数
        /// </summary>
        public int ByteLength { get; set; }

        /// <summary>
        /// 相対位置
        /// </summary>
        public int BeginPosition { get; set; }

        /// <summary>
        /// 繰返し回数
        /// </summary>
        public int Repeat { get; set; }

        private char _PadChar = '0';
        /// <summary>
        /// Pad文
        /// </summary>
        public char PadChar {
            get {
                
                return _PadChar;
            }
            
            set{
                _PadChar = value;
            }
        }

        /// <summary>
        /// Pad方向　0：前 　1：後
        /// </summary>
        public int PadDirection { get; set; }

        /// <summary>
        /// 初期値
        /// </summary>
        public string DefaultValue { get; set; }
        
    }
}
