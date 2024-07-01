using ASMAT.Demo.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace ASMAT.Demo.Controllers
{
    public class DemoDataController : Controller
    {

        public JsonResult CodeDatas()
        {
            return Json(GetCodeData(), JsonRequestBehavior.AllowGet);
        }

        public List<Object> GetCodeData()
        {
            return new List<Object> {
                new  {  Kind = "00", CD= "01", Name="地域", RefNo=1, Memo="編集可能"}, 
                new  {  Kind = "00", CD= "02", Name="役割", RefNo=0, Memo="編集不可"},  
                new  {  Kind = "00", CD= "03", Name="棚",RefNo=2,Memo="編集可能"}, 
                new  {  Kind = "00", CD= "04", Name="商品種別", RefNo=1, Memo="編集可能"}, 
                new  {  Kind = "00", CD= "05", Name="初回", RefNo=0, Memo="編集不可"}, 
                new  {  Kind = "00", CD= "06", Name="済", RefNo=0, Memo="編集不可"}, 

                new  {  Kind = "01", CD= "01", Name="大阪"}, 
                new  {  Kind = "01", CD= "02", Name="東京"}, 

                new  {  Kind = "02", CD= "1", Name="システム管理者"}, 
                new  {  Kind = "02", CD= "2", Name="本部管理者"}, 
                new  {  Kind = "02", CD= "3", Name="店舗管理者"}, 
                new  {  Kind = "02", CD= "4", Name="店舗担当者"}, 

                new  {  Kind = "03", CD= "0", Name="上", RefNo=0}, 
                new  {  Kind = "03", CD= "1", Name="１", RefNo=1}, 
                new  {  Kind = "03", CD= "2", Name="２", RefNo=2}, 
                new  {  Kind = "03", CD= "3", Name="３", RefNo=3}, 
                new  {  Kind = "03", CD= "4", Name="４", RefNo=4}, 

                new  {  Kind = "04", CD= "10", Name="お菓子"}, 
                new  {  Kind = "04", CD= "20", Name="水"}, 
                new  {  Kind = "04", CD= "30", Name="コヒー"}, 
                new  {  Kind = "04", CD= "40", Name="炭酸飲料"}, 
                new  {  Kind = "04", CD= "50", Name="ジュース"}, 
                new  {  Kind = "04", CD= "60", Name="スポーツ飲料"}, 
                new  {  Kind = "04", CD= "70", Name="エネルギー飲料"}, 
                new  {  Kind = "04", CD= "90", Name="その他"}, 

                new  {  Kind = "05", CD= "0", Name="未"}, 
                new  {  Kind = "05", CD= "1", Name="済"}, 

                new  {  Kind = "06", CD= "0", Name=""}, 
                new  {  Kind = "06", CD= "1", Name="○"}, 

            };
        }
    }
}