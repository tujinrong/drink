using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrinkService.Report.Common
{
    public class MetaModel
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private double x;

        public double X
        {
            get { return x; }
            set { x = value; }

        }
        private double y;

        public double Y
        {
            get { return y; }
            set { y = value; }
        }
        private double h;

        public double H
        {
            get { return h; }
            set { h = value; }
        }
        private double w;

        public double W
        {
            get { return w; }
            set { w = value; }
        }

        public MetaModel() { }
        public MetaModel(string name, double x, double y, double w, double h)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.h = h;
            this.w = w;
        }
    }
}
