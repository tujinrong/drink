
using System;
namespace Smat.Mvc.Extensions
{
    public interface IGridColumnAction
    {
        IGridColumnAction Click(string clickFunc);
        IGridColumnAction ActionType(string actionType);
        IGridColumnAction ActionConfirm(string actionConfirmFunc);
        IGridColumnAction Template(string template);
        IGridColumnAction TemplateBound(string templateFunc);
    }
}
