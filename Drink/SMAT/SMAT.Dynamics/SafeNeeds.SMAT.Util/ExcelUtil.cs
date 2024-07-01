using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMAT.Dynamics.SafeNeeds.SMAT.Util
{
    class ExcelUtil
    {
        public static void FillDataSet(DataSet ds, FileStream fileStream)
        {

            if (ds == null || fileStream == null)
            {
                return;
            }
		
        }
        public static DataSet ToDataSet(FileStream fileStream)
        {
            DataSet ds = new DataSet();
            FillDataSet(ds, fileStream);
            return ds;
        }
        public static DataSet ToDataSet(List<FileStream> fileStreamList)
        {
            DataSet ds = new DataSet();
            foreach (FileStream fileStream in fileStreamList)
            {
                FillDataSet(ds, fileStream);
            }
            return ds;
        }
    }
}
