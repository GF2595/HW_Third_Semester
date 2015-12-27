using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkNamespace
{
    /// <summary>
    /// Represents viruses
    /// </summary>
    internal class Virus
    {
        private string name;
        private List<float> OSInfectioningProbability;

        /// <summary>
        /// Constructor. Builds object with concrete name
        /// </summary>
        /// <param name="virusName">Name of virus</param>
        internal Virus(string virusName)
        {
            name = virusName;
            OSInfectioningProbability = new List<float>();
        }

        /// <summary>
        /// Adds infectioning probability
        /// </summary>
        /// <param name="infectioningProbability">Infectioning probability</param>
        internal void AddOS(float infectioningProbability)
        {
            OSInfectioningProbability.Add(infectioningProbability);
        }

        /// <summary>
        /// Checks infectioning probability for current OS
        /// </summary>
        /// <param name="OSNumber">OS number</param>
        /// <returns>Infectioning probability</returns>
        internal float CheckOSInfectioningProbability(int OSNumber)
        {
            if (OSNumber < OSInfectioningProbability.Count())
            {
                return OSInfectioningProbability[OSNumber];
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Return name of the virus
        /// </summary>
        /// <returns>Name of the virus</returns>
        internal string GetName()
        {
            return this.name;
        }
    }
}