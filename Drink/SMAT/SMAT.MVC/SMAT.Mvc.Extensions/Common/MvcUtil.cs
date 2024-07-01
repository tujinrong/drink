using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SMAT.MVC.SMAT.Mvc.Extensions.Common
{
    public static class MvcUtil
    {
        public static string CAttrStr(string i_val)
        {
            if (string.IsNullOrEmpty(i_val)) {
                return "";
            }

            string r = i_val.Replace(@"\", @"\\").Replace("\"", "\\\"").ToString();

            Regex reg = new Regex(@"script", RegexOptions.IgnoreCase);
            MatchCollection matchs = reg.Matches(r);
            foreach (Match item in matchs)
            {
                if (item.Success)
                {
                    Console.WriteLine(item.Value); 

                    r = r.Replace(item.Value, item.Value.Substring(0, 3) + "\" + \"" + item.Value.Substring(3, 3));
                }
            }

            return r;
        }
    }
}
