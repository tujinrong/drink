using Smat.Mvc.Extensions.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Smat.Mvc.Extensions
{
    public static class TemplateExtension
    {
        public static TemplateScript ScriptBuilder(this HtmlHelper helper, string id)
        {
            TagBuilder tagBuilder = new TagBuilder("script");
            tagBuilder.MergeAttribute("id", id);
            tagBuilder.MergeAttribute("type", "text");
            tagBuilder.MergeAttribute("style", "display:none;");

            helper.ViewContext.Writer.Write(tagBuilder.ToString(TagRenderMode.StartTag));
            TemplateScript script = new TemplateScript(helper.ViewContext, tagBuilder);
            return script;
        }

    }

    public class TemplateScript : MvcForm
    {
        private readonly ViewContext _viewContext;
        private TagBuilder _tagBuilder;
        private bool _disposed;

        public TemplateScript(ViewContext viewContext, TagBuilder tagBuilder)
            : base(viewContext)
        {
            if (viewContext == null)
            {
                throw new ArgumentNullException("viewContext");
            }

            _viewContext = viewContext;
            _tagBuilder = tagBuilder;
            // push the new FormContext
            _viewContext.FormContext = new FormContext();

        }

        public void Dispose()
        {
            Dispose(true /* disposing */);
            GC.SuppressFinalize(this);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _viewContext.Writer.Write(_tagBuilder.ToString(TagRenderMode.EndTag));
        }

    }
}
