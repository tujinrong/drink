using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions.Framework
{
    public interface IPagerExtension
    {
        IPagerExtension DataHandler(string dataHandler);
        IPagerExtension HtmlAttributes(object attrs);
        MvcHtmlString ToHtmlString();
        void Render();

    }
}
