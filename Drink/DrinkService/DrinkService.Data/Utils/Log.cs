using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Utils
{
    public class Log
    {
        public static void WriteLog(string log)
        {
            string basePath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            string logFile = basePath + "\\Log.log";
            string logTime = CommonUtils.GetDateTimeNow().ToString("yyyy/MM/dd HH:mm:ss");
            StreamWriter sw = null;
            try
            {
                string dir = logFile.Substring(0, logFile.LastIndexOf("\\"));
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }


                System.Text.Encoding encoding = Encoding.GetEncoding("Shift-JIS");
                sw = new StreamWriter(logFile, true, encoding);

                sw.WriteLine("[" + logTime + "]");
                sw.WriteLine(log);

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
    }
}
