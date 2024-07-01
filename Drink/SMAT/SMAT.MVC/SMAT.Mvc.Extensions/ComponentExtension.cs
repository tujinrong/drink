using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    public abstract class ComponentExtension 
    {
        protected HtmlHelper HtmlHelper { get; set; }

        protected IDictionary<string, object> attrs { get; set; }
        protected string IdAttribute { get; set; }
        protected string NameAttribute { get; set; }

        private ComponentExtension()
        {
        }

        protected ComponentExtension(HtmlHelper helper)
        {
            this.HtmlHelper = helper;
        }

        protected string GetPropertyValue<TModel>(object model, string propertyName) where TModel : class
        {
            PropertyInfo propertyInfo = typeof(TModel).GetProperty(propertyName);
            string value = propertyInfo.GetValue(model, null).ToString();
            return value;
        }

        protected virtual string CreateHtml()
        {
            throw new NotImplementedException();
        }

        protected virtual string CreateScript()
        {
            throw new NotImplementedException();
        }

        public MvcHtmlString ToHtmlString()
        {
            ScriptBuilder.Instance().AddScript(this.IdAttribute, this.CreateScript());
            return new MvcHtmlString(this.CreateHtml());
        }

        public override string ToString()
        {
            try
            {
                Render();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            
            return "";
        }

        public void Render()
        {
            this.HtmlHelper.ViewContext.Writer.Write(this.CreateHtml());
            this.HtmlHelper.ViewContext.Writer.Write(ScriptBuilder.Instance().AddScript(this.IdAttribute, this.CreateScript()).Render());
        }
    }
}
