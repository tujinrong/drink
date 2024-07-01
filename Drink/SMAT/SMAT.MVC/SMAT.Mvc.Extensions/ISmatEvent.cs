using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smat.Mvc.Extensions
{
    public interface ISmatEvent
    {
        ISmatEvent Click(string onClick);
        ISmatEvent Change(string onChange);
        ISmatEvent DataBound(string onDataBound);
        ISmatEvent DataBinding(string onDataBinding);
    }
}
