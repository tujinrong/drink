using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEvaluation.Common
{

    /// <summary>
    /// 帳票のタイプ

    /// </summary>
    public enum EnumReportType
    {
        FixedReport,
        ListReport,
    }

    public enum EnumResultType
    {
        NoData = 0,
        NoTemplate = 1,
        Success = 9,
    }

    public interface IReport
    {
        List<IReportData> Data { get; set; }

        IReportHandler Handler { get; set; }

        void CreateReport();
    }
}