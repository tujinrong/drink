using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    public class GridColumnAction : IGridColumnAction
    {
        public string TextAttribute { get; internal set; }
        public string ClickFuncAttribute { get; internal set; }
        public string ActionTypeAttribute { get; internal set; }
        public string ActionConfirmFuncAttribute { get; internal set; }
        public string TemplateAttribute { get; internal set; }
        public string TemplateFuncAttribute { get; internal set; }
        public IDictionary<string, object> attrs { get; internal set; }

        internal GridColumnAction(string text)
        {
            this.TextAttribute = text;
        }

        public IGridColumnAction Click(string clickFunc)
        {
            this.ClickFuncAttribute = clickFunc;
            return this;
        }

        public IGridColumnAction ActionType(string actionType)
        {
            this.ActionTypeAttribute = actionType;
            return this;
        }

        public IGridColumnAction ActionConfirm(string actionConfirmFunc)
        {
            this.ActionConfirmFuncAttribute = actionConfirmFunc;
            return this;
        }

        public IGridColumnAction Template(string template)
        {
            this.TemplateAttribute = template;
            return this;
        }
        public IGridColumnAction TemplateBound(string templateFunc)
        {
            this.TemplateFuncAttribute = templateFunc;
            return this;
        }
    }
}
