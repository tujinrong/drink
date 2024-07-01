//*****************************************************************************
// [システム]  配置ドリンク
// 
// [機能概要]  DBContext
//
// [作成履歴]　2015/06/25  屠錦栄　初版 
//
// [レビュー]　2015/07/17  屠錦栄　 
//*****************************************************************************


using System;

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Data;
using System.Collections.Generic;
using SafeNeeds.DySmat.DB.Exception;
using SafeNeeds.DySmat.Model;

namespace SafeNeeds.DySmat
{
    public enum EnumResult
    {
        OK,
        NoData,
        Warning,
        Error,
        FatalError,
        Haita,
        KeyExist,
        CheckDelete
    }
    /// <summary>
    /// 
    /// </summary>
    public class Result
    {

        public string Message;
        public EnumResult ReturnValue = EnumResult.OK;
        public string ErrorKey;
        public object data;

        public Result()
        {
            ReturnValue = EnumResult.OK;
        }
        
        public Result(EnumResult result, string msg)
        {
            ReturnValue = result;
            Message = msg;
        }

        public Result(EnumResult result, string msg,string errorKey)
        {
            ReturnValue = result;
            Message = msg;
            ErrorKey = errorKey;
        }

        public Result(EnumResult result, String msg, Object data)
        {
            ReturnValue = result;
            Message = msg;
            this.data = data;
        }


        public Result(Exception ex)
        {
            if (ex is HaitaException)
            {
                this.ReturnValue = EnumResult.Haita;
                this.Message = ex.Message;
            }
            else
            {
                this.ReturnValue = EnumResult.FatalError;
                this.Message = ex.Message;
            }
        }
    }

    public class ViewResult : Result
    {

         public DataTable DataTable;
    }

    public class PageViewResult : ViewResult
    {
        public int PageCount;

        public List<Series> series { set; get; }


        public List<ColumnItem> categories { set; get; }


        public List<ColumnItem> crossYItems { set; get; }


        public bool multipleVField { set; get; }


        public string SQL { set; get; }
        
        public Dictionary<string, Y_EntityViewItem> GroupFormatDic { get; set; } //廖add --2016/09/20
        public Dictionary<string, Y_EntityViewItem> YItemsDic { get; set; } //廖add --2016/09/20

        
    }

    public class SmatJsonResult 
    {
        public string Message {set;get;}
        public EnumResult ReturnValue = EnumResult.OK;
    }

    // 20160408 liao add series --begin
    public class Series 
    {
        public string name { set; get; }
        public List<SeriesData> data { set; get; }
    }

    public class SeriesData
    {
        public string category { set; get; }
        public string value { set; get; }
    }

    // 20160408 liao add series  --end

    // 20160908 liao add series --begin
    public class ColumnItem
    {
        public string field { set; get; }
        public string title { set; get; }
        public string width { set; get; }
    }

    // 20160908 liao add series  --end
}