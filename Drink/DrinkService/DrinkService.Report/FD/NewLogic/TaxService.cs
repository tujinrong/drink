using DrinkService.Data;
using DrinkService.Data.ViewModels;
using DrinkService.Models;
using DrinkService.Report.FD.Model;
using DrinkService.Utils;
using SafeNeeds.DySmat;
using SafeNeeds.DySmat.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.FD.Logic
{
    public  class TaxService 
    {
        public const string taxType標準税率 = "0";
        public const string taxType軽減税率 = "1";
        //税率dic
        //public static Dictionary<string, int> taxDic;
        static List<TaxModel> TaxList = new List<TaxModel>();

        public static bool IsNewTax(DateTime hoDate)
        {
            LoadTax();
            TaxModel model1 = TaxList.Find(e => e.TaxStartDay <= hoDate && e.TaxEndDay > hoDate && e.TaxTypeCD == taxType標準税率);
            TaxModel model2= TaxList.Find(e => e.TaxStartDay <= hoDate && e.TaxEndDay > hoDate && e.TaxTypeCD == taxType軽減税率);
            if (model1 == null || model2 == null) return false;
            return (model1.Tax != model2.Tax);
        }

        public static int GetTax(DateTime HoDate, string TaxTypeCD)
        {
            LoadTax();

            TaxModel model = TaxList.Find(e => e.TaxStartDay <= HoDate && e.TaxEndDay > HoDate && e.TaxTypeCD == TaxTypeCD);
            if (model == null) return 8;
            return model.Tax;
        }

       
        static DateTime timeStamp;
        private static void LoadTax()
        {
            lock ("LoadTax")
            {
                if (DateTime.Now.Subtract(timeStamp).TotalMinutes < 10) return;

                //defult
                //taxDic = new Dictionary<string, int>();
                string sql = @"
                select *
                from M_Tax
            ";

                DataTable dt = SQLHelper.GetDataTable(sql);

                TaxList = new List<TaxModel>();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        TaxModel model = new TaxModel();
                        TaxList.Add(model);
                        model.TaxStartDay = DataUtil.CDate(dr["TaxStartDay"]);
                        model.TaxEndDay = DataUtil.CDate(dr["TaxEndDay"]);
                        model.TaxTypeCD = DataUtil.CStr(dr["TaxTypeCD"]);
                        model.Tax = DataUtil.CInt(dr["Tax"]);

                    }
                }

                timeStamp = DateTime.Now;
            }

        }

    }

    public class TaxModel
    {
        public string TaxTypeCD { get; set; }
        public DateTime TaxStartDay { get; set; }
        public DateTime TaxEndDay { get; set; }
        public int Tax { get; set; }
    }
}
