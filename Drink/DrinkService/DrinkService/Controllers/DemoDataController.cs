using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace DrinkService.Controllers
{
    public class DemoDataController : BaseController
    {
        public JsonResult Data1()
        {
            var res = new JsonResult();
            //var value = "actionValue";  


            DataTable dt = new DataTable();

            dt.Columns.Add("FIELD1", typeof(string));
            dt.Columns.Add("FIELD2", typeof(string));
            dt.Columns.Add("FIELD3", typeof(string));
            dt.Columns.Add("FIELD4", typeof(string));
            dt.Columns.Add("FIELD5", typeof(string));
            dt.Columns.Add("FIELD6", typeof(string));
            dt.Columns.Add("FIELD7", typeof(string));

            dt.Rows.Add(new string[]{ "11001",  "大阪南森町店1",  "大阪2", "業務区分1",  "田中　一郎", "店舗1",  "20" });
            dt.Rows.Add(new string[] { "11002", "大阪南森町店2", "大阪3", "業務区分2", "田中　一郎", "店舗2", "21" });
            dt.Rows.Add(new string[] { "11003", "大阪南森町店3", "大阪4", "業務区分3", "田中　一郎", "店舗3", "20" });
            dt.Rows.Add(new string[] { "11004", "大阪南森町店4", "大阪2", "業務区分4", "田中　一郎", "店舗4", "24" });
            dt.Rows.Add(new string[] { "11005", "大阪南森町店5", "大阪1", "業務区分5", "田中　一郎", "店舗5", "20" });

            res.Data = new { dataSource = SerializeDataTable(dt) };


            res.JsonRequestBehavior = JsonRequestBehavior.AllowGet;//允许使用GET方式获取，否则用GET获取是会报错。  
            return res;
        }

        /// <summary>DataTable序列化
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> SerializeDataTable(DataTable dt)
        {
            //JavaScriptSerializer serializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            foreach (DataRow dr in dt.Rows)//每一行信息，新建一个Dictionary<string,object>,将该行的每列信息加入到字典
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }
            //return serializer.Serialize(list);//调用Serializer方法 
            return list;//调用Serializer方法 
        }
    }
}