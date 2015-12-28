using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _02._09
{
    public class MyGraphic
    {
        private List<Line> lines = new List<Line>();
        private Pen pen = new Pen(Color.Black);
        private History history = new History();

        /// <summary>
        /// Class constructor
        /// </summary>
        public MyGraphic()
        {
            history.AddList(new List<Line>());
        }
        
        /// <summary>
        /// Adds new line
        /// </summary>
        /// <param name="newStartPoint">New line start point</param>
        /// <param name="newEndPoint">New line end point</param>
        public void AddNewLine(PointF newStartPoint, PointF newEndPoint)
        {
            Line newLine = new Line(newStartPoint, newEndPoint);
            lines.Add(newLine);
            List<Line> linesToHistory = new List<Line>();
            CopyList<Line>.Copy(lines, out linesToHistory);
            history.AddList(linesToHistory);
        }

        /// <summary>
        /// Checks if mouse clicked on line end
        /// </summary>
        /// <param name="X">Click x coordinate</param>
        /// <param name="Y">Click y coordinate</param>
        /// <returns>'True' if clicked on line end, 'false' otherwise</returns>
        public bool IsEndCatched(ref float X, ref float Y)
        {
            const float eps = 3F;
            foreach (var line in lines)
            {
                double length1 = Math.Sqrt(Math.Pow(X - line.StartPoint.X, 2) + Math.Pow(Y - line.StartPoint.Y, 2));
                double length2 = Math.Sqrt(Math.Pow(X - line.EndPoint.X, 2) + Math.Pow(Y - line.EndPoint.Y, 2));
                if (length1 < eps)
                {
                    X = line.EndPoint.X;
                    Y = line.EndPoint.Y;
                    lines.Remove(line);
                    return true;
                }
                else if (length2 < eps)
                {
                    X = line.StartPoint.X;
                    Y = line.StartPoint.Y;
                    lines.Remove(line);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Checks if deleting successful
        /// </summary>
        /// <param name="X">Click x coordinate</param>
        /// <param name="Y">Click y coordinate</param>
        /// <returns>'True' if line deleted succesfully, 'false' otherwise</returns>
        public bool IsLineDeleted(float X, float Y)
        {
            const float eps = 0.25F;
            foreach (var line in lines)
            {
                double lengthToStart = Math.Sqrt(Math.Pow(line.StartPoint.X - X, 2) + Math.Pow(line.StartPoint.Y - Y, 2));
                double lengthToEnd = Math.Sqrt(Math.Pow(line.EndPoint.X - X, 2) + Math.Pow(line.EndPoint.Y - Y, 2));
                if (Math.Abs(line.Length() - lengthToStart - lengthToEnd) < eps)
                {
                    lines.Remove(line);
                    List<Line> linesToHistory = new List<Line>();
                    CopyList<Line>.Copy(lines, out linesToHistory);
                    history.AddList(linesToHistory); 
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Draws new line
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        /// <param name="startPoint">Line start point</param>
        /// <param name="endPoint">Line end point</param>
        public void DrawNewLine(ref PaintEventArgs e, PointF startPoint, PointF endPoint)
        {
            e.Graphics.DrawLine(pen, startPoint, endPoint);
        }

        /// <summary>
        /// Redraws all lines
        /// </summary>
        /// <param name="e">PaintEventArgs</param>
        public void ReDrawAllLines(ref PaintEventArgs e)
        {
            if (lines != null)
            {
                foreach (var line in lines)
                {
                    e.Graphics.DrawLine(pen, line.StartPoint, line.EndPoint);
                }
            }
        }

        /// <summary>
        /// Performs undo
        /// </summary>
        public void Undo()
        {
            history.Undo(ref lines);
        }

        /// <summary>
        /// Performs redo
        /// </summary>
        public void Redo()
        {
            history.Redo(ref lines);
        }
    }
}
