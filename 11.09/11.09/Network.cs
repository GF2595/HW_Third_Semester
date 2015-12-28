using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkNamespace
{
    /// <summary>
    /// Represents network
    /// </summary>
    public class Network
    {
        private List<List<bool>> networkConfiguration;
        private List<Computer> computers;
        private List<string> operatingSystems;
        private List<Virus> viruses;
        private int ticksAmount = 0;
        private Random randomizer;

        /// <summary>
        /// Class constructor. Constructs network using information from file
        /// </summary>
        /// <param name="configurationFilePath">File path</param>
        public Network(string configurationFilePath)
        {
            FileParser.ParseFile(configurationFilePath, out networkConfiguration, out computers, out operatingSystems, out viruses);

            randomizer = new Random();
        }

        /// <summary>
        /// Starts network working. Prints initial network state and initiates network running
        /// </summary>
        public void Run()
        {
            int i = 1;

            computers.ForEach(delegate(Computer computer)
            {
                Console.Write("Computer #{0}: ", i);

                computers[i - 1].GetVirusesList().ForEach(delegate(string virusName)
                {
                    Console.Write("{0} ", virusName);
                });

                Console.WriteLine();
                i++;
            });
            Console.WriteLine();

            this._Run();
        }

        /// <summary>
        /// Launches one network working tick, prints network state, prints "System stabilized" if system if stable, launches new tick otherwise
        /// </summary>
        private void _Run()
        {
            bool systemStable = this.Tick();
            ticksAmount++;
            int i = 1;

            computers.ForEach(delegate(Computer computer)
            {
                Console.Write("Computer #{0}: ", i);

                computers[i - 1].GetVirusesList().ForEach(delegate(string virusName)
                {
                    Console.Write("{0} ", virusName);
                });

                Console.WriteLine();
                i++;
            });
            Console.WriteLine();

            if (systemStable)
            {
                Console.WriteLine("System stabilized");
            }
            else
            {
                this.Run();
            }
        }

        /// <summary>
        /// Calculates statistics for each virus
        /// </summary>
        /// <returns>List with string-writed statistics for each virus</returns>
        public List<string> VirusesStatistics()
        {
            List<string> statistics = new List<string>();

            foreach (Virus virus in viruses)
            {
                string statiscticsField = virus.GetName();

                for (int i = 0; i < computers.Count(); i++)
                {
                    if (computers[i].IsInfectedByVirus(virus.GetName()))
                    {
                        statiscticsField += " " + (i + 1).ToString();
                    }
                }

                statistics.Add(statiscticsField);
            }

            return statistics;
        }

        /// <summary>
        /// Return amount of performed ticks
        /// </summary>
        /// <returns>Ticks amount</returns>
        public int TickAmountStatistics()
        {
            return ticksAmount;
        }

        /// <summary>
        /// Performs one network working tick
        /// </summary>
        /// <returns>'True' if network is stable, 'false' otherwise</returns>
        private bool Tick()
        {
            List<List<string>> virusesTransferredOnCurrentTick = new List<List<string>>();

            for (int i = 0; i < computers.Count(); i++)
            {
                virusesTransferredOnCurrentTick.Add(new List<string>());
            }

            bool systemStable = true;

            for (int i = 0; i < computers.Count(); i++)
            {
                for (int j = 0; j < computers.Count(); j++)
                {
                    if (networkConfiguration[i][j])
                    {
                        computers[i].GetVirusesList().ForEach(delegate(string virusName)
                        {
                            if (virusesTransferredOnCurrentTick[i].IndexOf(virusName) == -1)
                            {
                                float infectioningProbability = FindVirus(virusName).CheckOSInfectioningProbability(operatingSystems.IndexOf(computers[j].GetOS())) * 100;

                                if (infectioningProbability != 0 && !computers[j].IsInfectedByVirus(virusName))
                                {
                                    systemStable = false;

                                    int randomNumber = randomizer.Next(100);
                                    if (randomNumber < infectioningProbability)
                                    {
                                        computers[j].Infect(virusName);

                                        virusesTransferredOnCurrentTick[j].Add(virusName);
                                    }
                                }
                            }
                        });
                    }
                }
            }

            return systemStable;
        }

        /// <summary>
        /// Finds Virus class object in list of viruses of network by its name
        /// </summary>
        /// <param name="name">Virus name</param>
        /// <returns>Virus object</returns>
        private Virus FindVirus(string name)
        {
            Virus seekedVirus = null;

            viruses.ForEach(delegate(Virus virus)
            {
                if (virus.GetName() == name)
                {
                    seekedVirus = virus;
                }
            });

            return seekedVirus;
        }
    }
}