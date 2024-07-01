using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smat.Mvc.Extensions.Framework
{
    public class FormActionExtension
    {
        internal SmatMvcForm SmatMvcForm;

        public FormActionExtension(SmatMvcForm smatMvcForm)
        {
            this.SmatMvcForm = smatMvcForm;
        }

        public IFormAction Action(string action)
        {
            return this.SmatMvcForm.AddAction(action);
        }
    }
}
