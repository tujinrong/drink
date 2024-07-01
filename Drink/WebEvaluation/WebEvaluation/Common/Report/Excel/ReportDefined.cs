using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEvaluation.Common
{
    public class ReportDefined
    {
        public int DetailBegin { get; set; }

        public int DetailRows { get; set; }

        public Dictionary<String, DefinedItem> Items { get; set; }

        public void AddItem(DefinedItem item)
        {
            if (this.Items == null)
            {
                this.Items = new Dictionary<String, DefinedItem>();
            }
            if (Items.ContainsKey(item.FieldName))
            {
                this.Items.Remove(item.FieldName);
                this.Items.Add(item.FieldName, item);
            }
            else
            {
                this.Items.Add(item.FieldName, item);
            }

        }

        public void SetItemInfo(String key, ICell cell)
        {
            if (Items.ContainsKey(key))
            {
                DefinedItem item = this.Items[key];
                item.AddPoint(cell.ColumnIndex, cell.RowIndex);
            }
        }


        public Dictionary<String, DefinedItem> GetFieldItems() {
		Dictionary<String,DefinedItem> fieldItems = new Dictionary<String, DefinedItem>();
		
		foreach(KeyValuePair<String,DefinedItem> entry in this.Items){
			fieldItems.Add(entry.Value.Field, entry.Value);
		}
		return fieldItems;
	}
    }
}