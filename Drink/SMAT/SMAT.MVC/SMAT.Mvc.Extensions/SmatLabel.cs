using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    public class SmatLabel
    {
        internal string TextAttribute { get; set; }
        internal IDictionary<string, object> attrs { get; set; }

        internal SmatLabel()
        {
        }

        public void Text(string text)
        {
            this.TextAttribute = text;
        }

        public void Attrs(object attrs)
        {
            this.attrs = ((IDictionary<string, object>)HtmlHelper.AnonymousObjectToHtmlAttributes(attrs));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(",label:{");
            sb.AppendFormat("text:\"{0}\"", this.TextAttribute);
            if (this.attrs != null)
            {
                sb.Append(",attrs:{");

                string attrsStr = "";

                foreach (var item in this.attrs)
                {
                    attrsStr += (item.Key + ":'" + item.Value + "',");
                }

                if (attrsStr.EndsWith(",")) attrsStr = attrsStr.Substring(0, attrsStr.Length - 1);
                sb.Append(attrsStr + "}");
            }
            sb.Append("}");

            return sb.ToString();
        }
    }
}
