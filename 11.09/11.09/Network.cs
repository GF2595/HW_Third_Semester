using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NetworkNamespace
{
    /// <summary>
    /// Represents network
    /// </summary>
    class Network
    {
        private List<List<bool>> networkConfiguration = new List<List<bool>>();
        private List<Computer> computers = new List<Computer>();
        private List<string> operatingSystems = new List<string>();
        private List<Virus> viruses = new List<Virus>();
        private List<List<string>> virusesTransferredOnCurrentTick;

        /// <summary>
        /// Class constructor. Constructs network using information from file
        /// </summary>
        /// <param name="configurationFilePath">File path</param>
        public Network(string configurationFilePath)
        {
            StreamReader streamReader = new StreamReader(configurationFilePath);
            string buffer = "";

            buffer = streamReader.ReadLine();
            int computersQuantity = 0;
            int.TryParse(buffer, out computersQuantity);

            for (int i = 0; i < computersQuantity; i++)
            {
                networkConfiguration.Add(new List<bool>());

                for (int j = 0; j < computersQuantity; j++)
                {
                    networkConfiguration[i].Add(false);
                }
            }

                buffer = streamReader.ReadLine();
            int systemsQuantity = 0;
            int.TryParse(buffer, out systemsQuantity);
            
            buffer = streamReader.ReadLine();
            int virusesQuantity = 0;
            int.TryParse(buffer, out virusesQuantity);

            streamReader.ReadLine();            
            for (int i = 0; i < systemsQuantity; i++)
            {
                operatingSystems.Add(streamReader.ReadLine());
            }

            streamReader.ReadLine();
            for (int i = 0; i < virusesQuantity; i++)
            {
                buffer = streamReader.ReadLine();
                string virusName = "";

                int j = 0;
                while (buffer[j] != ' ')
                {
                    virusName += buffer[j];
                    j++;
                }
                j++;

                viruses.Add(new Virus(virusName));

                for (int k = 0; k < systemsQuantity; k++)
                {                    
                    string temp = "";
                    float tempFloat = 0;

                    while (buffer[j] != ' ')
                    {
                        temp += buffer[j];
                        j++;
                    }

                    float.TryParse(temp, out tempFloat);
                    j++;

                    viruses[i].addOS(tempFloat);
                }
            }

            for (int i = 0; i < computersQuantity; i++)
            {
                streamReader.ReadLine();
                int computerOS = 0;

                int.TryParse(streamReader.ReadLine(), out computerOS);
                computers.Add(new Computer(operatingSystems[computerOS - 1]));

                parseString(i, streamReader.ReadLine(), delegate(int first, int second)
                {
                    computers[first].infect(viruses[second - 1].getName());
                });

                parseString(i, streamReader.ReadLine(), delegate(int first, int second)
                {
                    networkConfiguration[first][second - 1] = true;
                });
            }
        }
        
        private delegate void twoIntsDependingFunction(int firstInt, int secondInt);

        /// <summary>
        /// Parses <paramref="buffer"> and performs <paramref name="func"/> with results
        /// </summary>
        /// <param name="i">First parameter for func</param>
        /// <param name="buffer">String to parse</param>
        /// <param name="func">Function to perform</param>
        private void parseString(int i, string buffer, twoIntsDependingFunction func)
        {
            if (buffer != " ")
            {
                int j = 0;

                while (j < buffer.Length)
                {
                    string temp = "";

                    while (buffer[j] != ' ')
                    {
                        temp += buffer[j];
                        j++;
                    }

                    int tempNumber = 0;
                    int.TryParse(temp, out tempNumber);
                                                
                    j++;

                    func(i, tempNumber);
                }
            }
        }

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
            /// <param name="_name">Name of virus</param>
            internal Virus(string _name)
            {
                name = _name;
                OSInfectioningProbability = new List<float>();
            }

            /// <summary>
            /// Adds infectioning probability
            /// </summary>
            /// <param name="infectioningProbability">Infectioning probability</param>
            internal void addOS(float infectioningProbability)
            {
                OSInfectioningProbability.Add(infectioningProbability);
            }

            /// <summary>
            /// Checks infectioning probability for current OS
            /// </summary>
            /// <param name="OSNumber">OS number</param>
            /// <returns>Infectioning probability</returns>
            internal float checkOSInfectioningProbability(int OSNumber)
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
            internal string getName()
            {
                return this.name;
            }
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

                computers[i - 1].getVirusesList().ForEach(delegate(string virusName)
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
            int i = 1;

            computers.ForEach(delegate(Computer computer)
            {
                Console.Write("Computer #{0}: ", i);

                computers[i - 1].getVirusesList().ForEach(delegate(string virusName)
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
        /// PErforms one network working tick
        /// </summary>
        /// <returns>'True' if network is stable, 'false' otherwise</returns>
        private bool Tick()
        {            
            virusesTransferredOnCurrentTick = new List<List<string>>();
            
            for (int i = 0; i < computers.Count(); i++)
            {
                virusesTransferredOnCurrentTick.Add(new List<string>());
            }

            bool systemStable = true;

            for (int i = 0; i < computers.Count(); i++)
            {
                for (int j = 0; j < computers.Count(); j++)
                {
                    Random rand = new Random();

                    if (networkConfiguration[i][j])
                    {
                        computers[i].getVirusesList().ForEach(delegate(string virusName)
                        {
                            if (virusesTransferredOnCurrentTick[i].IndexOf(virusName) == -1)
                            {
                                float infectioningProbability = findVirus(virusName).checkOSInfectioningProbability(operatingSystems.IndexOf(computers[j].getOS())) * 100;

                                if (infectioningProbability != 0 && !computers[j].isInfectedByVirus(virusName))
                                {
                                    systemStable = false;

                                    int randomNumber = rand.Next(100);
                                    if (randomNumber < infectioningProbability)
                                    {
                                        computers[j].infect(virusName);

                                        virusesTransferredOnCurrentTick[j].Add(virusName);
                                    }
                                }
                            }
                        });
                    }
                }
            }

            virusesTransferredOnCurrentTick = null;
            return systemStable;
        }

        /// <summary>
        /// Finds Virus class object in list of viruses of network by its name
        /// </summary>
        /// <param name="name">Virus name</param>
        /// <returns>Virus object</returns>
        private Virus findVirus(string name)
        {
            Virus seekedVirus = null;

            viruses.ForEach(delegate(Virus virus)
            {
                if (virus.getName() == name)
                {
                    seekedVirus = virus;
                }
            });

            return seekedVirus;
        }
    }
}
