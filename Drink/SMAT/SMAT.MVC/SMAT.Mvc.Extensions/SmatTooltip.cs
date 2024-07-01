using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    public class SmatTooltip
    {
        internal string ContentAttribute { get; set; }

        internal SmatTooltip()
        {
        }

        public void Content(string content)
        {
            this.ContentAttribute = content;
        }


        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(",tooltip:{");
            sb.AppendFormat("content:\"{0}\"", this.ContentAttribute);
            
            sb.Append("}");

            return sb.ToString();
        }
    }
}
