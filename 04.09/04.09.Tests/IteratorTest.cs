using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BinaryTreeNamespace;
using System.Collections.Generic;

namespace IteratorNamespace.Tests
{
    [TestClass]
    public class IteratorTest
    {
        [TestMethod]
        public void ResetTest()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(4);
            tree.Add(2);
            tree.Add(3);

            Iterator<int> iterator = new BinaryTree<int>.BinaryTreeIterator(tree);

            int temp = iterator.Next();
            iterator.Next();
            iterator.Reset();

            Assert.AreEqual(temp, iterator.Next());
        }

        [TestMethod]
        public void RemoveTest()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(4);
            tree.Add(2);
            tree.Add(3);

            Iterator<int> iterator = new BinaryTree<int>.BinaryTreeIterator(tree);

            iterator.Next();
            iterator.Remove();
            iterator.Reset();

            Assert.AreEqual(3, iterator.Next());
        }

        [TestMethod]
        public void ForEachTest()
        {
            BinaryTree<int> tree = new BinaryTree<int>();
            tree.Add(4);
            tree.Add(2);
            tree.Add(3);
            int[] values = { 2, 3, 4 };
            int i = 0;

            foreach(int value in tree)
            {
                Assert.AreEqual(value, values[i]);
                i++;
            }
        }
    }
}
