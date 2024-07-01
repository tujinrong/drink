using DrinkService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using System.Data.Entity.Migrations;
using DrinkService.Data.Models;
using DrinkService.Data.ViewModels;
using DrinkService.Utils;
using System.Data.Entity;
using SafeNeeds.DySmat;
using System.Web.Script.Serialization;

namespace DrinkService.Data.Logics
{
    public class LogLogic : LogicBase
    {
        public LogLogic(EntityRequest enreq)
            : base(enreq)
        {
            _enreq = enreq;
        }


        public void WriteLog(LogContent logContent)
        {
            T_Log log = new T_Log();
            log.UpdateTime = DateTime.Now;

            log.FunctionName = logContent.FunctionName;
            log.FunctionPath = logContent.FunctionPath;
            log.FunctionType = logContent.FunctionType;
            log.Key1 = logContent.Key1;
            log.Key2 = logContent.Key2;
            log.Key3 = logContent.Key3;
            log.Key4 = logContent.Key4;
            log.Key5 = logContent.Key5;
            log.LogType = logContent.LogType;

            log.Content = logContent.GetContent();
            log.UpdateUser = _enreq.User;

            db.Logs.AddOrUpdate(log);
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                throw;
            }
        }
    }

    public class LogContent {

        public string LogType { get; set; }

        public string FunctionName { get; set; }

        public string FunctionPath { get; set; }

        public string FunctionType { get; set; }

        public string Key1 { get; set; }

        public string Key2 { get; set; }

        public string Key3 { get; set; }

        public string Key4 { get; set; }

        public string Key5 { get; set; }

        private Dictionary<string, Object> content = new Dictionary<string, object>();

        public void AddContent(string key,Object obj){
            this.content.Add(key, obj);
        }

        public string GetContent() { 
            JavaScriptSerializer serializer = new JavaScriptSerializer();

            return serializer.Serialize(content);
        }
    }
}
