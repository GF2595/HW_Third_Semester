using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryTreeNamespace;

namespace IteratorNamespace
{
    /// <summary>
    /// Binary tree iterator realisation
    /// </summary>
    public class BinaryTreeIterator : Iterator
    {
        private BinaryTree tree = null;
        private BinaryTree.TreeNode currentNode = null;

        /// <summary>
        /// Type constructor
        /// </summary>
        /// <param name="_tree">tree, for which iterator is being constructed</param>
        public BinaryTreeIterator(BinaryTree _tree)
        {
            this.tree = _tree;

            this.currentNode = FindLowestLeftNode(this.tree.GetHead());
        }

        /// <summary>
        /// Finds lowest left node of the tree
        /// </summary>
        /// <param name="head">Tree root</param>
        /// <returns>Lowest left node of the tree</returns>
        private static BinaryTree.TreeNode FindLowestLeftNode(BinaryTree.TreeNode head)
        {
            BinaryTree.TreeNode temp = head;

            if (temp != null)
            {
                while (temp.Left != null)
                {
                    temp = temp.Left;
                }
            }

            return temp;
        }

        /// <summary>
        /// Find next node of the tree
        /// </summary>
        /// <param name="current"></param>
        /// <returns></returns>
        private static BinaryTree.TreeNode FindNext(BinaryTree.TreeNode current)
        {
            BinaryTree.TreeNode newCurrentNode = null;
            
            if (current != null)
            {
                if (current.Right != null)
                {
                    return FindLowestLeftNode(current.Right);
                }
                else
                {
                    if (current.previous != null)
                    {
                        if (current == current.previous.Left)
                        {
                            return current.previous;
                        }
                        else
                        {
                            bool finished = false;
                            newCurrentNode = current;

                            while (!finished)
                            {
                                if (newCurrentNode.previous == null)
                                {
                                    finished = true;
                                }
                                else if (newCurrentNode == newCurrentNode.previous.Left)
                                {
                                    finished = true;
                                }

                                newCurrentNode = newCurrentNode.previous;
                            }

                            
                        }
                    }
                }
            }

            return newCurrentNode;
        }

        /// <summary>
        /// Returns next element of the tree
        /// </summary>
        /// <returns>Next element of the tree</returns>
        public int next()
        {
            int value = currentNode.GetValue();

            this.currentNode = FindNext(this.currentNode);

            return value;             
        }

        /// <summary>
        /// Checks if tree bypass is finished
        /// </summary>
        /// <returns>'True' if previous element was the last one, 'false' otherwise</returns>
        public bool isEmpty()
        {
            return this.currentNode == null;
        }

        /// <summary>
        /// Restarts the bypass of the tree
        /// </summary>
        public void reset()
        {
            this.currentNode = FindLowestLeftNode(this.tree.GetHead());
        }

        /// <summary>
        /// Deletes current element from the tree
        /// </summary>
        public void remove()
        {
            int temp = this.currentNode.GetValue();
            this.currentNode = FindNext(this.currentNode);

            this.tree.Delete(temp);
        }
    }
}
