using WebEvaluation.DAL;
using WebEvaluation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebEvaluation.Controllers.Filters;
using WebEvaluation.Models;
using WebEvaluation.DataModels;
using System.Data.Entity;
using System.Reflection;
using WebEvaluation.Utils;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;
using PagedList;

namespace WebEvaluation.Controllers
{
    public class BaseController : Controller
    {
        private EvaluationContext db = new EvaluationContext();

        public ActionResult ImportIndex<T>(string fileName,string action, List<ImportErrorViewModel> errorList,List<T> dataList, List<T> addList, List<T> updateList, List<T> deleteList,object param)
        {
            PropertyInfo[] fields = typeof(T).GetProperties();
            List<ImportViewModel> viewModelList = new List<ImportViewModel>();

            if (errorList.Count == 0)
            {
                viewModelList.Add(new ImportViewModel { Type = "処理", Fields = CommonUtils.GetFiledNames<T>() });

                for (int i = 0; i < addList.Count; i++)
                {
                    T data = addList[i];
                    List<string> fieldValue = new List<string>();
                    for (int j = 0; j < fields.Length; j++)
                    {
                        PropertyInfo field = fields[j];
                        var val = field.GetValue(data);
                        if (null == val)
                        {
                            fieldValue.Add("");
                        }
                        else
                        {
                            fieldValue.Add(val.ToString());
                        }
                    }
                    viewModelList.Add(new ImportViewModel { Type = "追加", Fields = fieldValue });
                }

                for (int i = 0; i < updateList.Count; i++)
                {
                    T data = updateList[i];
                    List<string> fieldValue = new List<string>();
                    for (int j = 0; j < fields.Length; j++)
                    {
                        PropertyInfo field = fields[j];
                        var val = field.GetValue(data);
                        if (null == val)
                        {
                            fieldValue.Add("");
                        }
                        else
                        {
                            fieldValue.Add(val.ToString());
                        } 
                    }
                    viewModelList.Add(new ImportViewModel { Type = "更新", Fields = fieldValue });
                }

                for (int i = 0; i < deleteList.Count; i++)
                {
                    T data = deleteList[i];
                    List<string> fieldValue = new List<string>();
                    for (int j = 0; j < fields.Length; j++)
                    {
                        PropertyInfo field = fields[j];
                        var val =  field.GetValue(data);
                        if (null == val)
                        {
                            fieldValue.Add("");
                        }
                        else
                        {
                            fieldValue.Add(val.ToString()); 
                        } 
                    }
                    viewModelList.Add(new ImportViewModel { Type = "削除", Fields = fieldValue });
                }

                ViewBag.action = action;
                ViewBag.fileName = fileName;
                ViewBag.param = param;

                ViewBag.dataCount = dataList.Count;
                ViewBag.addCount = addList.Count;
                ViewBag.updateCount = updateList.Count;
                ViewBag.sameCount = dataList.Count - updateList.Count - addList.Count;
                ViewBag.deleteCount = deleteList.Count;

                return ImportConfirm(viewModelList);
            }
            else
            {
                bool hasExistError = false;
                bool hasValueError = false;

                List<ImportViewModel> valueErrorList = new List<ImportViewModel>();
                List<string> existList = new List<string>();
                List<string> notExistField = new List<string>();

                for (int i = 0; i < errorList.Count; i++)
                {
                    if (errorList[i].ErrorType.Contains("FieldNotExistError") || errorList[i].ErrorType.Contains("FieldSameExistError"))
                    {
                        
                        string filed = errorList[i].ErrorField;
                        for (int j = 0; j < fields.Length; j++)
                        {
                            if (filed == ((DisplayAttribute)fields[j].GetCustomAttribute(typeof(DisplayAttribute), true)).Name)
                            {
                                if (hasExistError)
                                {
                                    if (errorList[i].ErrorType.Contains("FieldSameExistError"))
                                    {
                                        existList[j] = "該当列が複数存在する";
                                    }
                                    else {
                                        existList[j] = "存在しない";
                                    }
                                    
                                }
                                else
                                {
                                    if (errorList[i].ErrorType.Contains("FieldSameExistError"))
                                    {
                                        existList.Add("複数列存在");
                                    }
                                    else
                                    {
                                        existList.Add("存在しない");
                                    }
                                }
                                notExistField.Add(filed);
                            }
                            else
                            {
                                if (!hasExistError) 
                                {
                                    existList.Add("存在");
                                }  
                            }
                        }
                        
                        hasExistError = true;
                    }
                    else
                    {
                        if (!hasValueError) {
                            int rowNum = errorList[i].ErrorRow;

                            T data = dataList[rowNum];
                            List<string> fieldName = CommonUtils.GetFiledNames<T>();
                            List<string> fieldValue = new List<string>();

                            for (int j = 0; j < fields.Length; j++)
                            {
                                PropertyInfo field = fields[j];

                                var val = field.GetValue(data);
                                if (null == val)
                                {
                                    fieldValue.Add("");
                                }
                                else
                                {
                                    fieldValue.Add(val.ToString());
                                } 

                                if (errorList[i].ErrorField == ((DisplayAttribute)field.GetCustomAttribute(typeof(DisplayAttribute), true)).Name)
                                {
                                    LinkAttribute linkAttr = ((LinkAttribute)field.GetCustomAttribute(typeof(LinkAttribute)));
                                    if (linkAttr != null)
                                    {
                                        ViewBag.isCreateLink = true;
                                        ViewBag.linkText = linkAttr.linkText;
                                        string regStr = linkAttr.routeReg;
                                        Regex regex = new Regex("\\#(.*?)\\#", RegexOptions.IgnoreCase | RegexOptions.Multiline);
                                        if (regex.IsMatch(regStr))
                                        {
                                            MatchCollection matchCollection = regex.Matches(regStr);
                                            foreach (Match match in matchCollection)
                                            {
                                                String definedKey = match.Value.Replace("#", "");

                                                for (int k = 0; k < fields.Length; k++)
                                                {
                                                    PropertyInfo fieldv = fields[k];
                                                    if (fieldv.Name == definedKey)
                                                    {
                                                        var valv = fieldv.GetValue(data);
                                                       
                                                        if (null != valv)
                                                        {
                                                            if (fieldv.Name == "StaffName")
                                                            {
                                                                valv = Server.UrlEncode(valv.ToString());
                                                            }
                                                           regStr = regStr.Replace(match.Value.ToString(), valv.ToString());
                                                        }
                                                    }

                                                }
                                            }
                                        }
                                        ViewBag.href = regStr;
                                        
                                    }
                                }
                            }
                            valueErrorList.Add(new ImportViewModel { Type = "行番号", Fields = fieldName });
                            valueErrorList.Add(new ImportViewModel { Type = (rowNum + 1).ToString(), Fields = fieldValue });
                            List<string> ErrorFields = new List<string>();
                            ErrorFields.Add(errorList[i].ErrorField);
                            ViewBag.ErrorFields = ErrorFields;
                            ViewBag.ErrorDetail = (rowNum + 2) + "行目、" + errorList[i].ErrorDetail;
                            hasValueError = true;
                        }
                       
                    }
                }

                if (hasExistError)
                {
                    ViewBag.ErrorFields = notExistField;
                    viewModelList.Add(new ImportViewModel { Type = "項目見出", Fields = CommonUtils.GetFiledNames<T>() });
                    viewModelList.Add(new ImportViewModel { Type = "存在チェック", Fields = existList });  
                }
                else
                {
                    viewModelList.AddRange(valueErrorList);
                    ViewBag.hasValueError = true; 
                }

                ViewBag.action = action;
                ViewBag.param = param;

                return ImportError(viewModelList);
            }
        }

        public ActionResult ImportConfirm(List<ImportViewModel> viewModelList)
        {
           
            if (viewModelList != null)
            {
                List<ImportViewModel> jsonModelList = new List<ImportViewModel>();
                jsonModelList.AddRange(viewModelList);
                viewModelList.Clear();
                viewModelList.Add(jsonModelList[0]);
                jsonModelList.Remove(jsonModelList[0]);
                base.ViewData.Model = viewModelList;

                List<List<ImportViewModel>> jsonList = new List<List<ImportViewModel>>();

                List<ImportViewModel> indexList = new List<ImportViewModel>();

                for (int i = 0; i < jsonModelList.Count; i++)
                {
                    if (indexList.Count == 100)
                    {
                        jsonList.Add(indexList);
                        indexList = new List<ImportViewModel>();
                    }
                    else
                    {
                        indexList.Add(jsonModelList[i]);
                        if (i == jsonModelList.Count - 1)
                        {
                            jsonList.Add(indexList);
                        }
                    }
                }

                JavaScriptSerializer jss = new JavaScriptSerializer();
                string hehe = jss.Serialize(jsonList);
                base.ViewBag.jsonList = jss.Serialize(jsonList);
            }
            ViewResult result = new ViewResult();
            result.ViewName = "ImportConfirm";
            result.ViewData = base.ViewData;
            result.TempData = base.TempData;
            result.ViewEngineCollection = this.ViewEngineCollection;
            return result;

        }

        public ActionResult ImportError(List<ImportViewModel> viewModelList)
        {

            if (viewModelList != null)
            {
                base.ViewData.Model = viewModelList;
            }
            ViewResult result = new ViewResult();
            result.ViewName = "ImportError";
            result.ViewData = base.ViewData;
            result.TempData = base.TempData;
            result.ViewEngineCollection = this.ViewEngineCollection;
            return result;

        }
    }

    public class DyPagedList<T> : PagedList<T>
    {
        public DyPagedList(IEnumerable<T> superset, int pageNumber, int pageSize)
            : this(superset.AsQueryable<T>(), pageNumber, pageSize)

        {

        }

        public DyPagedList(IQueryable<T> superset, int pageNumber, int pageSize)
            : base(superset.AsQueryable<T>(), pageNumber, pageSize)
        {

        }

        public DyPagedList(IEnumerable<T> superset, int pageNumber, int pageSize, int total)
            : this(superset.AsQueryable<T>(), pageNumber, pageSize, total)
        {

        }
        public DyPagedList(IQueryable<T> superset, int pageNumber, int pageSize, int total)
            : base(superset.AsQueryable<T>(), pageNumber, pageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException("pageNumber", pageNumber, "PageNumber cannot be below 1.");
            }
            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException("pageSize", pageSize, "PageSize cannot be less than 1.");
            }

            base.TotalItemCount = total;
            base.PageSize = pageSize;
            base.PageNumber = pageNumber;
            base.PageCount = (base.TotalItemCount > 0) ? ((int)Math.Ceiling((double)(((double)base.TotalItemCount) / ((double)base.PageSize)))) : 0;
            base.HasPreviousPage = base.PageNumber > 1;
            base.HasNextPage = base.PageNumber < base.PageCount;
            base.IsFirstPage = base.PageNumber == 1;
            base.IsLastPage = base.PageNumber >= base.PageCount;
            base.FirstItemOnPage = ((base.PageNumber - 1) * base.PageSize) + 1;
            int num = (base.FirstItemOnPage + base.PageSize) - 1;
            base.LastItemOnPage = (num > base.TotalItemCount) ? base.TotalItemCount : num;
            if ((superset != null) && (base.TotalItemCount > 0))
            {
                base.Subset.Clear();
                base.Subset.AddRange(superset.ToList<T>());
            }

        }



        public void setTotalItemCount(int i)
        {
            this.TotalItemCount = i;
        }

        public void setPageCount(int i)
        {
            this.PageCount = i;
        }

        public void setPageSize(int i)
        {
            this.PageSize = i;
        }

        public void setPageNumber(int i)
        {
            this.PageNumber = i;
        }
    }
    
}