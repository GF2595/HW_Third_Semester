using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02._09
{
    /// <summary>
    /// Represents line
    /// </summary>
    public class Line
    {
        public PointF StartPoint { get; set; }
        public PointF EndPoint { get; set; }

        /// <summary>
        /// Class constructor
        /// </summary>
        /// <param name="startPoint">Line start point</param>
        /// <param name="endPoint">Line end point</param>
        public Line(PointF startPoint, PointF endPoint)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
        }

        /// <summary>
        /// Calculates line length
        /// </summary>
        /// <returns>Line length</returns>
        public double Length()
        {
            return Math.Sqrt(Math.Pow(StartPoint.X - EndPoint.X, 2) + Math.Pow(StartPoint.Y - EndPoint.Y, 2));
        }
    }
}
