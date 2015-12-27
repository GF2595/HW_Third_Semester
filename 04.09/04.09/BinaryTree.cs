using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IteratorNamespace;

namespace BinaryTreeNamespace
{
    /// <summary>
    /// Represents binary tree
    /// </summary>
    public class BinaryTree : IEnumerable
    {
        internal TreeNode head = null;

        public BinaryTree()
        {
        }

        /// <summary>
        /// Represents <typeparamref name="BinaryTree"/> nodes
        /// </summary>
        internal class TreeNode
        {
            internal TreeNode Left { get; set; }
            internal TreeNode Right { get; set; }
            internal TreeNode previous { get; set; }
            private int value = 0;

            /// <summary>
            /// Type constructor
            /// </summary>
            /// <param name="element">Value of node</param>
            /// <param name="previous">Element, which child current one is</param>
            internal TreeNode(int element, TreeNode previous)
            {
                this.value = element;
                this.previous = previous;
            }

            /// <summary>
            /// Adds <paramref name="element"/> to the tree
            /// </summary>
            /// <param name="tree">Extendable tree root</param>
            /// <param name="previous">Element, child of which extendable one is (if existing, reveives null otherwise)</param>
            /// <param name="element">Element to add</param>
            /// <returns>Extended tree</returns>
            internal static TreeNode AddElement(TreeNode tree, TreeNode previous, int element)
            {
                if (tree == null)
                {
                    tree = new TreeNode(element, previous);
                }
                else if (element < tree.value)
                {
                    tree.Left = AddElement(tree.Left, tree, element);
                }
                else if (element > tree.value)
                {
                    tree.Right = AddElement(tree.Right, tree, element);
                }

                return tree;
            }

            /// <summary>
            /// Checks if <paramref name="element"/> is in the tree
            /// </summary>
            /// <param name="tree">Tree root</param>
            /// <param name="element">Element to find</param>
            /// <returns>'True' if element is found in tree, 'false' otherwise</returns>
            internal static bool FindElement(TreeNode tree, int element)
            {
                if (tree == null)
                {
                    return false;
                }
                else if (tree.value > element)
                {
                    return FindElement(tree.Left, element);
                }
                else if (tree.value < element)
                {
                    return FindElement(tree.Right, element);
                }
                else
                {
                    return true;
                }
            }

            /// <summary>
            /// Deletes <paramref name="element"/> from the tree
            /// </summary>
            /// <param name="tree">Root of the tree, from which element should be deleted</param>
            /// <param name="element">Element to be deleted</param>
            /// <returns>Root of tree without <paramref name="element"/></returns>
            internal static TreeNode DeleteElement(TreeNode tree, int element)
            {
                if (tree.value > element)
                {
                    tree.Left = DeleteElement(tree.Left, element);
                }
                else if (tree.value < element)
                {
                    tree.Right = DeleteElement(tree.Right, element);
                }
                else if (tree.Left == null && tree.Right == null)
                {
                    tree = null;
                }
                else if (tree.Left == null || tree.Right == null)
                {
                    if (tree.Left != null)
                    {
                        tree.Left.previous = tree.previous;
                        tree = tree.Left;
                    }
                    else
                    {
                        tree.Right.previous = tree.previous;
                        tree = tree.Right;
                    }
                }
                else
                {
                    if (tree.Right.Left == null)
                    {
                        tree.Left.previous = tree.Right;
                        tree.Right.Left = tree.Left;
                        tree.Right.previous = tree.previous;
                        tree = tree.Right;
                    }
                    else
                    {
                        TreeNode tempPrevious = tree;
                        TreeNode temp = tree.Right;

                        while (temp.Left != null)
                        {
                            tempPrevious = temp;
                            temp = temp.Left;
                        }

                        tree.value = temp.value;
                        tempPrevious.Left = null;
                    }
                }

                return tree;
            }

            /// <summary>
            /// Returns node value
            /// </summary>
            /// <returns>Node value</returns>
            public int GetValue()
            {
                return this.value;
            }
        }

        /// <summary>
        /// Adds <paramref name="element"/> to the tree
        /// </summary>
        /// <param name="element">Element to be added</param>
        public void Add(int element)
        {
            this.head = TreeNode.AddElement(this.head, null, element);
        }

        /// <summary>
        /// Checks if <paramref name="element"/> is in tree
        /// </summary>
        /// <param name="element">Element to be found</param>
        /// <returns>'True' if element is in tree, 'false' otherwise</returns>
        public bool Find(int element)
        {
            return TreeNode.FindElement(this.head, element);
        }

        /// <summary>
        /// Deletes <paramref name="element"/> from the tree
        /// </summary>
        /// <param name="element">Element to be deleted</param>
        public void Delete(int element)
        {
            this.head = TreeNode.DeleteElement(this.head, element);
        }

        public IEnumerator GetEnumerator()
        {
            return new BinaryTreeIterator(this);
        }

        /// <summary>
        /// Binary tree iterator realisation
        /// </summary>
        public class BinaryTreeIterator : Iterator, IEnumerator
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

                this.currentNode = FindLowestLeftNode(this.tree.head);
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

            public bool MoveNext()
            {
                if (!this.isEmpty())
                {
                    this.currentNode = FindNext(this.currentNode);

                    return true;
                }
                else
                {
                    return false;
                }
            }

            public object Current
            {
                get
                {
                    return this.currentNode.GetValue();
                }
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
            public void Reset()
            {
                this.currentNode = FindLowestLeftNode(this.tree.head);
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

            public void Dispose()
            {
                this.tree = null;
                this.currentNode = null;
            }
        }
    }
}
