using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Smat.Mvc.Extensions.Framework
{
    public class SmatMvcForm : MvcForm
    {
        private readonly ViewContext _viewContext;
        private bool _disposed;
        private string script;

        private IList<IFormAction> FormActionList;

        public SmatMvcForm(ViewContext viewContext)
            : base(viewContext)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            _viewContext = viewContext;

            // push the new FormContext
            _viewContext.FormContext = new FormContext();

            FormActionList = new List<IFormAction>();
        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            StringBuilder sb = new StringBuilder();

            foreach (IFormAction formAction in FormActionList)
            {
                if (sb.Length > 0)
                {
                    sb.Append(",{");
                }
                else
                {
                    sb.Append("{");
                }
                sb.AppendFormat("action:\"{0}\",actionBtn:\"{1}\"", (formAction as FormAction).ActionAttribute, (formAction as FormAction).ActionBtnAttribute);
                if (!string.IsNullOrEmpty((formAction as FormAction).ResultHandlerAttribute)) sb.AppendFormat(",resultHandler:\"{0}\"", (formAction as FormAction).ResultHandlerAttribute);
                if (!string.IsNullOrEmpty((formAction as FormAction).CheckFormFuncAttribute)) sb.AppendFormat(",checkForm:{0}", (formAction as FormAction).CheckFormFuncAttribute);
                if (!string.IsNullOrEmpty((formAction as FormAction).GetParamFuncAttribute)) sb.AppendFormat(",getParam:{0}", (formAction as FormAction).GetParamFuncAttribute);
                if (!string.IsNullOrEmpty((formAction as FormAction).SuccessFuncAttribute)) sb.AppendFormat(",success:{0}", (formAction as FormAction).SuccessFuncAttribute);
                if (!string.IsNullOrEmpty((formAction as FormAction).ErrorFuncAttribute)) sb.AppendFormat(",error:{0}", (formAction as FormAction).ErrorFuncAttribute);
                if ((formAction as FormAction).AsyncAttribute != null) sb.AppendFormat(",async:{0}", (formAction as FormAction).AsyncAttribute.Value ? "true" : "false");
                if ((formAction as FormAction).IgnoreCommonCheckAttribute != null) sb.AppendFormat(",ignoreCommonCheck:{0}", (formAction as FormAction).IgnoreCommonCheckAttribute.Value ? "true" : "false");
                if (!string.IsNullOrEmpty((formAction as FormAction).ConfirmAttribute)) sb.AppendFormat(",confirm:\"{0}\"", (formAction as FormAction).ConfirmAttribute);
                if (!string.IsNullOrEmpty((formAction as FormAction).ConfirmFuncAttribute)) sb.AppendFormat(",confirmFunc:{0}", (formAction as FormAction).ConfirmFuncAttribute);
                sb.Append("}");
            }

            string actions = sb.ToString();
            this.script = this.script.Replace("#actions#", actions);

            _viewContext.Writer.Write(this.script);
            TagBuilder tagBuilder = new TagBuilder("div");
            _viewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.EndTag));

        }

        public void SetScript(string i_script)
        {
            this.script = i_script;
        }

        public SmatMvcForm Actions(Action<FormActionExtension> action)
        {
            FormActionExtension builder = new FormActionExtension(this);
            action(builder);
            return this;
        }

        public IFormAction AddAction(string action)
        {
            FormAction formAction = new FormAction(action);
            this.FormActionList.Add(formAction);
            return formAction;
        }
    }
}