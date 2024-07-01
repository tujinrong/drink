using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEvaluation.Common
{
    public interface IReportData
    {
        List<IReportData> Detail { set; get; }
    }
}