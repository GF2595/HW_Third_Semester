using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkNamespace;

namespace HW11_09Namespace
{
    class Program
    {
        static void Main(string[] args)
        {
            Network network = new Network("..\\..\\testConfiguration.txt");

            network.Run();
        }
    }
}
