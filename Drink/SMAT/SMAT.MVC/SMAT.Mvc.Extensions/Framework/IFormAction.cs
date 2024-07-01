using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smat.Mvc.Extensions.Framework
{
    public interface IFormAction
    {
        IFormAction ActionBtn(string acitonBtn);
        IFormAction ResultHandler(string resultHandler);
        IFormAction CheckForm(string checkFormFunc);
        IFormAction GetParam(string getParamFunc);
        IFormAction Success(string successFunc);
        IFormAction Error(string errorFunc);
        IFormAction Async(bool async);
        IFormAction IgnoreCommonCheck(bool ignore);
        IFormAction Confirm(string configMsg);
        IFormAction ConfirmFunc(string configMsgFunc);
    }
}
