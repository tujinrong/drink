using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebEvaluation.Utils;

namespace WebEvaluation.Common
{
    public class ReportData : IReportData
    {
        [CSV(Output = false)]
        public List<IReportData> Detail { set; get; }
    }
}