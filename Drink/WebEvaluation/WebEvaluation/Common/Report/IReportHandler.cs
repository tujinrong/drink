using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEvaluation.Common
{

    public interface IReportHandler
    {
        void AfterCreateReportSheet(HSSFWorkbook wb, ISheet sheet);


        string GetSheetName(IReportData pageData,int index);

        void AfterCreateReportBook(HSSFWorkbook wb);
    }
}