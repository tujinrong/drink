using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smat.Mvc.Extensions.Framework
{
    public class FormAction : IFormAction
    {

        public string ActionAttribute { get; internal set; }
        public string ActionBtnAttribute { get; internal set; }
        public string ResultHandlerAttribute { get; internal set; }
        public string CheckFormFuncAttribute { get; internal set; }
        public string GetParamFuncAttribute { get; internal set; }
        public string SuccessFuncAttribute { get; internal set; }
        public string ErrorFuncAttribute { get; internal set; }
        public bool? AsyncAttribute { get; internal set; }
        public bool? IgnoreCommonCheckAttribute { get; internal set; }
        public string ConfirmAttribute { get; internal set; }
        public string ConfirmFuncAttribute { get; internal set; }

        public FormAction(string action)
        {
            this.ActionAttribute = action;
        }

        public IFormAction ActionBtn(string acitonBtn)
        {
            this.ActionBtnAttribute = acitonBtn;
            return this;
        }

        public IFormAction ResultHandler(string resultHandler)
        {
            this.ResultHandlerAttribute = resultHandler;
            return this;
        }

        public IFormAction CheckForm(string checkFormFunc)
        {
            this.CheckFormFuncAttribute = checkFormFunc;
            return this;
        }
        public IFormAction GetParam(string getParamFunc)
        {
            this.GetParamFuncAttribute = getParamFunc;
            return this;
        }
        public IFormAction Success(string successFunc)
        {
            this.SuccessFuncAttribute = successFunc;
            return this;
        }

        public IFormAction Error(string errorFunc)
        {
            this.ErrorFuncAttribute = errorFunc;
            return this;
        }

        public IFormAction Async(bool async)
        {
            this.AsyncAttribute = async;
            return this;
        }

        public IFormAction IgnoreCommonCheck(bool ignore)
        {
            this.IgnoreCommonCheckAttribute = ignore;
            return this;
        }

        public IFormAction Confirm(string configMsg)
        {
            this.ConfirmAttribute = configMsg;
            return this;
        }

        public IFormAction ConfirmFunc(string configMsgFunc)
        {
            this.ConfirmFuncAttribute = configMsgFunc;
            return this;
        }
    }
}
