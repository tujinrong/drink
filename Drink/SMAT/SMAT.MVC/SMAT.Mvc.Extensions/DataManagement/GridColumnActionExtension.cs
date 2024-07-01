using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Smat.Mvc.Extensions
{
    public class GridColumnActionExtension
    {
        internal GridColumn gridColumn { get; set; }

        public GridColumnActionExtension(GridColumn gridBuilder)
        {
            this.gridColumn = gridBuilder;
        }

        public IGridColumnAction Text(string text)
        {
            return gridColumn.AddAction(text);
        }
    }
}
