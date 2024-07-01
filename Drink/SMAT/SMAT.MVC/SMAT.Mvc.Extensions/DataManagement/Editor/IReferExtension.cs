using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smat.Mvc.Extensions
{
    public interface IReferExtension
    {
        IReferExtension Name(string name);
        IReferExtension Value(string value);
        IReferExtension Text(string text);
        IReferExtension Label(string labelText);
        IReferExtension Label(Action<SmatLabel> smatLabel);
        IReferExtension Tooltip(string content);
        IReferExtension Tooltip(Action<SmatTooltip> smatTooltip);
        IReferExtension ReferKey(string referKey);
        IReferExtension ValueField(string valueField);
        IReferExtension DisplayField(string displayField);
        IReferExtension Param(object param);
        IReferExtension GetParam(string param);
        IReferExtension AfterSetValue(string afterSetValueFunc);
        IReferExtension Enable(bool enable);
        IReferExtension Visible(bool visible);
        IReferExtension HtmlAttributes(object attrs);

        IReferExtension Events(Action<SmatEvent> smatEvent);

        void Render();
    }
}
