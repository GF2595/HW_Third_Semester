using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IteratorNamespace
{
    public interface Iterator
    {
        /// <summary>
        /// Returns next element of the tree
        /// </summary>
        /// <returns>Next element of the tree</returns>
        int next();

        /// <summary>
        /// Checks if tree bypass is finished
        /// </summary>
        /// <returns>'True' if previous element was the last one, 'false' otherwise</returns>
        bool isEmpty();

        /// <summary>
        /// Restarts the bypass of the tree
        /// </summary>
        void Reset();

        /// <summary>
        /// Deletes current element from the tree
        /// </summary>
        void remove();
    }
}
