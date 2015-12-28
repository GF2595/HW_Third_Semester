using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02._09
{
    /// <summary>
    /// Represents editor history
    /// </summary>
    public class History
    {
        private List<List<Line>> undoList = new List<List<Line>>();
        private List<List<Line>> redoList = new List<List<Line>>();

        public void Undo(ref List<Line> lines)
        {
            if (undoList.Count > 1)
            {
                List<Line> linesToRedo = new List<Line>();
                CopyList<Line>.Copy(lines, out linesToRedo);
                redoList.Add(linesToRedo);
                CopyList<Line>.Copy(undoList[undoList.Count - 2], out lines);
                undoList.RemoveAt(undoList.Count - 1);
            }
        }

        public void Redo(ref List<Line> lines)
        {
            if (redoList.Count != 0)
            {
                CopyList<Line>.Copy(redoList[redoList.Count - 1], out lines);
                List<Line> linesToUndo = new List<Line>();
                CopyList<Line>.Copy(lines, out linesToUndo);
                undoList.Add(linesToUndo);
                redoList.RemoveAt(redoList.Count - 1);
            }
        }


        public void AddList(List<Line> lines)
        {
            undoList.Add(lines);
            redoList.Clear();
        }
    }
}
