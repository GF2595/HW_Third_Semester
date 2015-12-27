using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BinaryTreeNamespace;
using IteratorNamespace;

namespace HW04_09Namespace
{
    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree tree = new BinaryTree();

            tree.Add(8);

            Iterator iterator = new BinaryTree.BinaryTreeIterator(tree);

            while (!iterator.isEmpty())
            {
                Console.Write("{0} ", iterator.next());
            }

            //Console.Write("{0} ", iterator.next());
            //Console.Write("{0} ", iterator.next());
            //Console.Write("{0} ", iterator.next());
            //Console.Write("{0} ", iterator.next());
            //iterator.reset();
            //Console.Write("{0} ", iterator.next());
            //Console.Write("{0} ", iterator.next());
            //Console.Write("{0} ", iterator.next());
            //iterator.remove();
            //Console.Write("{0} ", iterator.next());
            //iterator.reset();
            //Console.Write("{0} ", iterator.next());
            //Console.Write("{0} ", iterator.next());
            //Console.Write("{0} ", iterator.next());
            //Console.Write("{0} ", iterator.next());
        }
    }
}
