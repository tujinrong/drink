using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebEvaluation.Common
{
    public class DefinedItem
    {
        
        public String FieldName{get;set;}

        public String Field { get; set; }

        public String Type { get; set; }

        public String CellValue { get; set; }

        public List<DefinedPoint> Points { get; set; }


        public void AddPoint(int x, int y)
        {
            if (this.Points == null)
            {
                this.Points = new List<DefinedPoint>();
            }

            DefinedPoint point = new DefinedPoint();
            point.X = x;
            point.Y = y;

            this.Points.Add(point);
        }
    }


    public class DefinedPoint
    {
        public int X { get; set; }

        public int Y { get; set; }
    }
}