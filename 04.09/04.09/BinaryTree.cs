using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeNamespace
{
    /// <summary>
    /// Represents binary tree
    /// </summary>
    public class BinaryTree<T> : IEnumerable where T : IComparable
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
            private T value = default(T);

            /// <summary>
            /// Type constructor
            /// </summary>
            /// <param name="element">Value of node</param>
            /// <param name="previous">Element, which child current one is</param>
            internal TreeNode(T element, TreeNode previous)
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
            internal static TreeNode AddElement(TreeNode tree, TreeNode previous, T element)
            {
                if (tree == null)
                {
                    tree = new TreeNode(element, previous);
                }
                else if (element.CompareTo(tree.value) < 0)
                {
                    tree.Left = AddElement(tree.Left, tree, element);
                }
                else if (element.CompareTo(tree.value) > 0)
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
            internal static bool FindElement(TreeNode tree, T element)
            {
                if (tree == null)
                {
                    return false;
                }
                else if (tree.value.CompareTo(element) > 0)
                {
                    return FindElement(tree.Left, element);
                }
                else if (tree.value.CompareTo(element) < 0)
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
            internal static TreeNode DeleteElement(TreeNode tree, T element)
            {
                if (tree.value.CompareTo(element) > 0)
                {
                    tree.Left = DeleteElement(tree.Left, element);
                }
                else if (tree.value.CompareTo(element) < 0)
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
            public T GetValue()
            {
                return this.value;
            }
        }

        /// <summary>
        /// Adds <paramref name="element"/> to the tree
        /// </summary>
        /// <param name="element">Element to be added</param>
        public void Add(T element)
        {
            this.head = TreeNode.AddElement(this.head, null, element);
        }

        /// <summary>
        /// Checks if <paramref name="element"/> is in tree
        /// </summary>
        /// <param name="element">Element to be found</param>
        /// <returns>'True' if element is in tree, 'false' otherwise</returns>
        public bool Find(T element)
        {
            return TreeNode.FindElement(this.head, element);
        }

        /// <summary>
        /// Deletes <paramref name="element"/> from the tree
        /// </summary>
        /// <param name="element">Element to be deleted</param>
        public void Delete(T element)
        {
            this.head = TreeNode.DeleteElement(this.head, element);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator) new BinaryTreeIterator(this);
        }

        /// <summary>
        /// Binary tree iterator realisation
        /// </summary>
        public class BinaryTreeIterator: IEnumerator, IteratorNamespace.Iterator<T>
        {
            private List<T> nodesList = new List<T>();
            private int index = -1;

            /// <summary>
            /// Adds values to nodesList
            /// </summary>
            private void FillNodeList(TreeNode currentNode)
            {
                if (currentNode.Left != null)
                {
                    FillNodeList(currentNode.Left);
                }

                nodesList.Add(currentNode.GetValue());

                if (currentNode.Right != null)
                {
                    FillNodeList(currentNode.Right);
                }
            }
            /// <summary>
            /// Type constructor
            /// </summary>
            /// <param name="_tree">Tree, for which iterator is being constructed</param>
            public BinaryTreeIterator(BinaryTree<T> tree)
            {
                FillNodeList(tree.head);
            }

            /// <summary>
            /// Finds lowest left node of the tree
            /// </summary>
            /// <param name="head">Tree root</param>
            /// <returns>Lowest left node of the tree</returns>
            private static BinaryTree<T>.TreeNode FindLowestLeftNode(BinaryTree<T>.TreeNode head)
            {
                BinaryTree<T>.TreeNode temp = head;

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
            /// Moves to the next value in tree
            /// </summary>
            /// <returns>'True' if there is no more element in tree, 'false' otherwise</returns>
            public bool MoveNext()
            {
                index++;
                return index < nodesList.Count;
            }

            public object Current
            {
                get
                {
                    return nodesList[index];
                }
            }

            /// <summary>
            /// Checks if tree bypass is finished
            /// </summary>
            /// <returns>'True' if previous element was the last one, 'false' otherwise</returns>
            public bool isEmpty()
            {
                return index == nodesList.Count();
            }

            /// <summary>
            /// Restarts the bypass of the tree
            /// </summary>
            public void Reset()
            {
                index = -1;
            }

            public T Next()
            {
                index++;

                if (index < nodesList.Count())
                {
                    return nodesList[index];
                }
                else
                {
                    throw new IndexOutOfRangeException();
                }
            }

            /// <summary>
            /// Deletes current element from the tree
            /// </summary>
            public void Remove()
            {
                T currentValue = nodesList[index];
                nodesList.Remove(currentValue);
            }
        }
    }
}
