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

        public MyGraphic()
        {
            history.AddList(new List<Line>());
        }
        
        public void AddNewLine(PointF newStartPoint, PointF newEndPoint)
        {
            Line newLine = new Line(newStartPoint, newEndPoint);
            lines.Add(newLine);
            List<Line> linesToHistory = new List<Line>();
            CopyList<Line>.Copy(lines, out linesToHistory);
            history.AddList(linesToHistory);
        }

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

        public void DrawNewLine(ref PaintEventArgs e, PointF startPoint, PointF endPoint)
        {
            e.Graphics.DrawLine(pen, startPoint, endPoint);
        }

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

        public void Undo()
        {
            history.Undo(ref lines);
        }

        public void Redo()
        {
            history.Redo(ref lines);
        }
    }
}
