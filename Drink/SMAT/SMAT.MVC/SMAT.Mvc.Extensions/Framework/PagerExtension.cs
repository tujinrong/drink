using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions.Framework
{
    public class PagerExtension : ComponentExtension, IPagerExtension
    {
        private string dataHandlerAttribute { get; set; }

        internal PagerExtension(HtmlHelper helper,string id)
            : base(helper)
        {
            this.IdAttribute = id;
        }

        public IPagerExtension DataHandler(string dataHandler)
        {
            this.dataHandlerAttribute = dataHandler;
            return this;
        }

        public IPagerExtension HtmlAttributes(object attrs)
        {
            this.attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(attrs));
            return this;
        }

        protected override string CreateHtml()
        {
            TagBuilder input = new TagBuilder("div");
            if (!string.IsNullOrEmpty(this.IdAttribute)) input.MergeAttribute("id", this.IdAttribute);
            if (!string.IsNullOrEmpty(this.NameAttribute)) input.MergeAttribute("name", this.NameAttribute);
            input.MergeAttributes(attrs);
            return input.ToString();
        }

        protected override string CreateScript()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("jQuery(\"#");
            sb.Append(this.IdAttribute);
            sb.Append("\").smatPager({");
            sb.AppendFormat("dataHandler:\"{0}\"", this.dataHandlerAttribute);
            sb.Append("});");
            return sb.ToString();
        }
    }
}
