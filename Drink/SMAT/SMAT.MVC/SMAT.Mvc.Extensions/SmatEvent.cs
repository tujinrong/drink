using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    public class SmatEvent : ISmatEvent
    {
        internal string clickAttribute { get; set; }
        internal string changeAttribute { get; set; }
        internal string dataBoundAttribute { get; set; }
        internal string dataBindingAttribute { get; set; }
        internal SmatEvent()
        {
        }

        public ISmatEvent Click(string onClick)
        {
            this.clickAttribute = onClick;
            return this;
        }

        public ISmatEvent Change(string onChange)
        {
            this.changeAttribute = onChange;
            return this;
        }

        public ISmatEvent DataBound(string onDataBound)
        {
            this.dataBoundAttribute = onDataBound;
            return this;
        }
        public ISmatEvent DataBinding(string onDataBinding)
        {
            this.dataBindingAttribute = onDataBinding;
            return this;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(this.clickAttribute))
            {
                sb.AppendFormat(",click:{0}", this.clickAttribute);
            }
            if (!string.IsNullOrEmpty(this.changeAttribute))
            {
                sb.AppendFormat(",change:{0}", this.changeAttribute);
            }
            if (!string.IsNullOrEmpty(this.dataBoundAttribute))
            {
                sb.AppendFormat(",dataBound:{0}", this.dataBoundAttribute);
            }
            if (!string.IsNullOrEmpty(this.dataBindingAttribute))
            {
                sb.AppendFormat(",dataBinding:{0}", this.dataBindingAttribute);
            }
            return sb.ToString();
        }
    }
}
