using System.Collections;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    public abstract class EditorExtension : ComponentExtension
    {
        protected string DataValueFieldAttribute { get; set; }
        protected string DataTextFieldAttribute { get; set; }
        protected string ValueAttribute { get; set; }
        protected IEnumerable DataSourceAttribute { get; set; }
        protected AjaxDataSource AjaxDataSource { get; set; }
        protected bool EnableAttribute { get; set; }
        protected bool VisibleAttribute { get; set; }
        protected bool SuggestAttribute { get; set; }
        protected int MaxLengthAttribute { get; set; }
        protected int MinLengthAttribute { get; set; }
        protected int HeightAttribute { get; set; }
        protected string ClassAttribute { get; set; }
        protected SmatLabel SmatLabel { get; set; }
        protected SmatEvent SmatEvent { get; set; }
        protected SmatTooltip SmatTooltip { get; set; }
        /// <summary>
        /// Constructor.
        /// </summary>
        internal EditorExtension(HtmlHelper helper,string id)
            : base(helper)
        {
            this.IdAttribute = id;
            this.EnableAttribute = true;
            this.VisibleAttribute = true;
            this.SuggestAttribute = false;
            this.MinLengthAttribute = 1;
            this.HeightAttribute = 200;
        }
    }
}
