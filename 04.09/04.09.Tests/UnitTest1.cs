using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using IteratorNamespace;
using BinaryTreeNamespace;

namespace IteratorNamespace.Tests
{
    [TestClass]
    public class IteratorTest
    {
        [TestMethod]
        public void nextTest()
        {
            BinaryTree tree = new BinaryTree();
            tree.Add(4);
            tree.Add(2);
            tree.Add(3);
            tree.Add(7);
            tree.Add(8);
            
            Iterator iterator = new BinaryTreeIterator(tree);

            Assert.AreEqual(2, iterator.next());
            Assert.AreEqual(3, iterator.next());
            iterator.next();
            Assert.AreEqual(7, iterator.next());
        }

        [TestMethod]
        public void isEmptyTest()
        {
            BinaryTree tree = new BinaryTree();
            Iterator iterator = new BinaryTreeIterator(tree);

            Assert.AreEqual(true, iterator.isEmpty());
            
            tree = new BinaryTree();
            tree.Add(2);
            iterator = new BinaryTreeIterator(tree);

            Assert.AreEqual(false, iterator.isEmpty());
        }

        [TestMethod]
        public void resetTest()
        {
            BinaryTree tree = new BinaryTree();
            tree.Add(4);
            tree.Add(2);
            tree.Add(3);

            Iterator iterator = new BinaryTreeIterator(tree);

            int temp = iterator.next();
            iterator.next();
            iterator.reset();

            Assert.AreEqual(temp, iterator.next());
        }

        [TestMethod]
        public void removeTest()
        {
            BinaryTree tree = new BinaryTree();
            tree.Add(4);
            tree.Add(2);
            tree.Add(3);

            Iterator iterator = new BinaryTreeIterator(tree);

            iterator.remove();
            iterator.reset();

            Assert.AreEqual(3, iterator.next());
        }
    }
}
