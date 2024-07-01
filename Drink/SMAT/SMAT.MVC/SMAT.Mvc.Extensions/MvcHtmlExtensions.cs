
using Smat.Mvc.Extensions;
namespace System.Web.Mvc.Html
{
    public static class MvcHtmlExtensions
    {
        /// <summary>
        /// Return an instance of a SmatManager.
        /// </summary>
        /// <param name="helper">HtmlHelper.</param>
        /// <returns>Instance of a SmatManager</returns>
        public static SmatManager Smat(this HtmlHelper helper)
        {
            return new SmatManager(helper);
        }
    }
}
