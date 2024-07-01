using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Smat.Mvc.Extensions
{
    public class GridColumnExtension
    {
        internal GridExtension GridExtension { get; set; }

        public GridColumnExtension(GridExtension gridBuilder)
        {
            this.GridExtension = gridBuilder;
        }

        public IGridColumn Bound(string field)
        {
            return GridExtension.AddColumn(field);
        }
    }

    public class GridColumnExtension<TModel> where TModel : class
    {
        internal GridExtension<TModel> GridExtension { get; set; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tableBuilder">Instance of a GridExtension.</param>
        public GridColumnExtension(GridExtension<TModel> gridBuilder)
        {
            this.GridExtension = gridBuilder;
        }

        /// <summary>
        /// Add lambda expressions to the GridExtension.
        /// </summary>
        /// <typeparam name="TProperty">Class property that is rendered in the column.</typeparam>
        /// <param name="expression">Lambda expression identifying a property to be rendered.</param>
        /// <returns>An instance of GridColumn.</returns>
        public IGridColumn<TModel> Bound<TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            return GridExtension.AddColumn(expression);
        }
    }
}
