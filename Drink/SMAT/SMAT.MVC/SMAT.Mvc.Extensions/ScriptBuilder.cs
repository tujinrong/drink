using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Smat.Mvc.Extensions
{
    public class ScriptBuilder
    {
        private static ScriptBuilder ScriptManagerInstance { get; set; }
        private IDictionary<string, string> Scripts { get; set; }

        private ScriptBuilder()
        {
            this.Scripts = new Dictionary<string, string>();
        }

        public static ScriptBuilder Instance()
        {
            if (ScriptBuilder.ScriptManagerInstance == null)
            {
                ScriptBuilder.ScriptManagerInstance = new ScriptBuilder();
            }
            return ScriptBuilder.ScriptManagerInstance;
        }

        public ScriptBuilder AddScript(string script)
        {
            this.Scripts.Add(DateTime.Now.Ticks.ToString(), script);
            return this;
        }

        public ScriptBuilder AddScript(string name, string script)
        {
            try
            {
                if (this.Scripts.ContainsKey(name) == false)
                {
                    this.Scripts.Add(name, script);
                }
                else
                {
                    this.Scripts[name] = script;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }
            
            return this;
        }

        public MvcHtmlString Render()
        {
            StringBuilder sb = new StringBuilder();

            try
            {
                sb.AppendLine("<script type='text/javascript'>");
                sb.AppendLine("if (typeof jQuery != 'undefined') {");
                sb.AppendLine("  jQuery(document).ready(function () {");
                foreach (string key in this.Scripts.Keys)
                {
                    sb.AppendLine("    " + this.Scripts[key]);
                }
                sb.AppendLine("  }); ");
                sb.AppendLine("}");
                sb.AppendLine("</script>");

                this.Scripts.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
            }

            return new MvcHtmlString(sb.ToString());
        }
    }
}
