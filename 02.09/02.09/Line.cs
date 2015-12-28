using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02._09
{
    public class Line
    {
        public PointF StartPoint { get; set; }
        public PointF EndPoint { get; set; }

        public Line(PointF x, PointF y)
        {
            StartPoint = x;
            EndPoint = y;
        }

        public double Length()
        {
            return Math.Sqrt(Math.Pow(StartPoint.X - EndPoint.X, 2) + Math.Pow(StartPoint.Y - EndPoint.Y, 2));
        }
    }
}
