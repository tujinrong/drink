using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smat.Mvc.Extensions
{
    public interface IGridColumnInternal
    {
        string FieldAttribute { get; }
        string TitleAttribute { get; }
        string WidthAttribute { get; }
        string Evaluate();
    }


    public interface IGridColumnInternal<TModel> where TModel : class
    {
        string FieldAttribute { get; }
        string TitleAttribute { get; }
        string WidthAttribute { get; }

        string Evaluate(TModel model);
    }
}
