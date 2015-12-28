using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02._09
{
    public static class CopyList<T>
    {
        /// <summary>
        /// Copies information from <paramref name="listFrom"/> to <paramref name="listTo"/>
        /// </summary>
        /// <param name="listFrom">List to copy from</param>
        /// <param name="listTo">List to copy to</param>
        public static void Copy(List<T> listFrom, out List<T> listTo)
        {
            List<T> temp = new List<T>();

            for (int i = 0; i < listFrom.Count; ++i)
            {
                temp.Add(listFrom[i]);
            }

            listTo = temp;
        }
    }
}
