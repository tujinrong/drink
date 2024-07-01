
using Smat.Mvc.Extensions.Editor;
using Smat.Mvc.Extensions.Framework;
using Smat.Mvc.Extensions.Navigation;
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using System.Web.Mvc.Html;

namespace Smat.Mvc.Extensions
{
    public class SmatManager
    {
        protected HtmlHelper HtmlHelper { get; set; }

        private SmatManager()
        {
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="helper">HtmlHelper.</param>
        public SmatManager(HtmlHelper helper)
        {
            this.HtmlHelper = helper;
        }

        public ScriptBuilder ScriptManager()
        {
            return ScriptBuilder.Instance();
        }

        /// <summary>
        /// Return an instance of a IDatePickerExtension.
        /// </summary>
        /// <returns>Instance of a IDatePickerExtension.</returns>
        public IDatePickerExtension DatePicker(string id)
        {
            return new DatePickerExtension(this.HtmlHelper, id);
        }

        /// <summary>
        /// Return an instance of a IDateTimePickerExtension.
        /// </summary>
        /// <returns>Instance of a IDateTimePickerExtension.</returns>
        public IDateTimePickerExtension DateTimePicker(string id)
        {
            return new DateTimePickerExtension(this.HtmlHelper, id);
        }

        /// <summary>
        /// Return an instance of a ITimePickerExtension.
        /// </summary>
        /// <returns>Instance of a ITimePickerExtension.</returns>
        public ITimePickerExtension TimePicker(string id)
        {
            return new TimePickerExtension(this.HtmlHelper, id);
        }

        /// <summary>
        /// Return an instance of a IGridExtension.
        /// </summary>
        /// <returns>Instance of a IGridExtension.</returns>
        public IGridExtension Grid(string id)
        {
            return new GridExtension(this.HtmlHelper, id);
        }

        /// <summary>
        /// Return an instance of a IGridExtension.
        /// </summary>
        /// <returns>Instance of a IGridExtension.</returns>
        public IGridExtension<TModel> Grid<TModel>() where TModel : class
        {
            return new GridExtension<TModel>(this.HtmlHelper);
        }

        /// <summary>
        /// Return an instance of a IFormExtension.
        /// </summary>
        /// <returns>Instance of a IFormExtension.</returns>
        public SmatMvcForm BeginForm(string name)
        {
            return FormExtension.BeginForm(this.HtmlHelper, name, null, null);
        }

        public SmatMvcForm BeginForm(string name, object htmlAttribute)
        {
            return FormExtension.BeginForm(this.HtmlHelper, name, null, htmlAttribute);
        }

        /// <summary>
        /// Return an instance of a IReferExtension.
        /// </summary>
        /// <returns>Instance of a IReferExtension.</returns>
        public IReferExtension Refer(string id)
        {
            return new ReferExtension(this.HtmlHelper, id);
        }

        /// <summary>
        /// Return an instance of a IDropDownListExtension.
        /// </summary>
        /// <returns>Instance of a IDropDownListExtension.</returns>
        public IDropDownListExtension DropDownList(string id)
        {
            return new DropDownListExtension(this.HtmlHelper, id);
        }

        /// <summary>
        /// Return an instance of a ITextBoxExtension.
        /// </summary>
        /// <returns>Instance of a ITextBoxExtension.</returns>
        public ITextBoxExtension TextBox(string id)
        {
            return new TextBoxExtension(this.HtmlHelper,id);
        }

        /// <summary>
        /// Return an instance of a ITextAreaExtension.
        /// </summary>
        /// <returns>Instance of a ITextAreaExtension.</returns>
        public ITextAreaExtension TextArea(string id)
        {
            return new TextAreaExtension(this.HtmlHelper, id);
        }

        /// <summary>
        /// Return an instance of a IButtonExtension.
        /// </summary>
        /// <returns>Instance of a IButtonExtension.</returns>
        public IButtonExtension Button(string id)
        {
            return new ButtonExtension(this.HtmlHelper, id);
        }

        /// <summary>
        /// Return an instance of a ICheckBoxExtension.
        /// </summary>
        /// <returns>Instance of a ICheckBoxExtension.</returns>
        public ICheckBoxExtension CheckBox(string id)
        {
            return new CheckBoxExtension(this.HtmlHelper, id);
        }

        /// <summary>
        /// Return an instance of a IPagerExtension.
        /// </summary>
        /// <returns>Instance of a IPagerExtension.</returns>
        public IPagerExtension Pager(string id)
        {
            return new PagerExtension(this.HtmlHelper, id);
        }

        /// <summary>
        /// Return an instance of a IRadioButtonExtension.
        /// </summary>
        /// <returns>Instance of a IRadioButtonExtension.</returns>
        public IRadioButtonExtension RadioButton(string id)
        {
            return new RadioButtonExtension(this.HtmlHelper, id);
        }

        public TemplateScript Template(string id)
        {
            return TemplateExtension.ScriptBuilder(this.HtmlHelper, id);
        }
    }
}
