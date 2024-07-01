using System.Collections.Generic;
using System.Text;

namespace Smat.Mvc.Extensions
{
    public class GridDataSource
    {
        private string TypeAttribute { get; set; }
        private string ReadAttribute { get; set; }

        public void Type(string type)
        {
            this.TypeAttribute = type;
        }

        public void Read(string read)
        {
            this.ReadAttribute = read;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("type: \"{0}\",",this.TypeAttribute);
            sb.Append("transport: {");
            sb.AppendFormat("read: \"{0}\"",this.ReadAttribute);
            sb.Append("}}");
            return sb.ToString();
        }
    }

    public class GridSendData
    {
        private string UpdateDataNameAttribute { get; set; }
        private string AddDataNameAttribute { get; set; }
        private string DeleteDataNameAttribute { get; set; }

        public void UpdateDataName(string updateDataName)
        {
            this.UpdateDataNameAttribute = updateDataName;
        }

        public void AddDataName(string addDataName)
        {
            this.AddDataNameAttribute = addDataName;
        }

        public void DeleteDataName(string deleteDataName)
        {
            this.DeleteDataNameAttribute = deleteDataName;
        }

        public override string ToString()
        {
            string dataSourceStr = "";
            StringBuilder sb = new StringBuilder();
            if (!string.IsNullOrEmpty(this.UpdateDataNameAttribute))
            {
                sb.AppendFormat("updateDataName:\"{0}\"", this.UpdateDataNameAttribute);
                sb.Append(",");
            }
            if (!string.IsNullOrEmpty(this.AddDataNameAttribute))
            {
                sb.AppendFormat("addDataName:\"{0}\"", this.AddDataNameAttribute);
                sb.Append(",");
            }
            if (!string.IsNullOrEmpty(this.DeleteDataNameAttribute))
            {
                sb.AppendFormat("deleteDataName:\"{0}\"", this.DeleteDataNameAttribute);
                sb.Append(",");
            }

            dataSourceStr = sb.ToString();

            if (dataSourceStr.EndsWith(",")) dataSourceStr = dataSourceStr.Substring(0, dataSourceStr.Length - 1);

            return dataSourceStr;
        }
    }
    public class GridDataSource<TModel> where TModel : class
    {
        private GridExtension<TModel> GridExtension { get; set; }
        internal IEnumerable<TModel> DataAttribute { get; set; }
        internal string PageSizeAttribute { get; set; }

        internal GridDataSource(GridExtension<TModel> gridBuilder)
        {
            this.GridExtension = gridBuilder;
        }

        public GridDataSource<TModel> Data(IEnumerable<TModel> data)
        {
            this.DataAttribute = data;
            return this;
        }

        public GridDataSource<TModel> PageSize(int pageSize)
        {
            this.PageSizeAttribute = pageSize.ToString();
            return this;
        }
    }
}
