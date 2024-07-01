using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smat.Mvc.Extensions
{
    public class AjaxDataSource
    {
        internal string SelectUrlAttribute { get; set; }

        internal AjaxDataSource()
        {
        }

        public void Select(string actionName, string controllerName)
        {
            this.SelectUrlAttribute = string.Format("/{0}/{1}", controllerName, actionName);
        }

        internal string CreateScript()
        {
            return string.Format("new kendo.data.DataSource({{transport:{{read:{{url:'{0}',dataType:'json'}}}}}})", this.SelectUrlAttribute);
        }
    }
}
    