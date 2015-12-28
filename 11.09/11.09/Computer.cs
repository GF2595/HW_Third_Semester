using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkNamespace
{
    /// <summary>
    /// Represents one computer in network
    /// </summary>
    internal class Computer
    {
        private List<string> viruses;
        private string os;

        /// <summary>
        /// Class constructor. Builds object with concrete OS
        /// </summary>
        /// <param name="_OS">Computer OS</param>
        internal Computer(string OS)
        {
            viruses = new List<string>();
            os = OS;
        }

        /// <summary>
        /// Checks, if computer is infected by concrete virus
        /// </summary>
        /// <param name="virus">Virus name</param>
        /// <returns>'True' if infected, 'false' otherwise</returns>
        internal bool IsInfectedByVirus(string virus)
        {
            return !(viruses.IndexOf(virus) == -1);
        }

        /// <summary>
        /// Adds new virus to list of viruses ifencting computer
        /// </summary>
        /// <param name="virus">Virus name</param>
        internal void Infect(string virus)
        {
            viruses.Add(virus);
        }

        /// <summary>
        /// Return list of virus infecting computer
        /// </summary>
        /// <returns>List of virus infecting computer</returns>
        internal List<string> GetVirusesList()
        {
            return viruses;
        }

        /// <summary>
        /// Returns name of computer OS
        /// </summary>
        /// <returns>Name of computer OS</returns>
        internal string GetOS()
        {
            return os;
        }
    }
}
