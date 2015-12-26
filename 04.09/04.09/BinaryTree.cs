using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BinaryTreeNamespace
{
    /// <summary>
    /// Represents binary tree
    /// </summary>
    public class BinaryTree
    {
        private TreeNode head = null;

        public BinaryTree()
        {
        }

        /// <summary>
        /// Represents <typeparamref name="BinaryTree"/> nodes
        /// </summary>
        public class TreeNode
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

        /// <summary>
        /// Return tree root
        /// </summary>
        /// <returns>Tree root</returns>
        public TreeNode GetHead()
        {
            return this.head;
        }
    }
}
