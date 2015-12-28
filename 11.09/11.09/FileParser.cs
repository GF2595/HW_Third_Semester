using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NetworkNamespace
{
    class FileParser
    {
        /// <summary>
        /// Parses file to fulfill Lists
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="networkConfiguration">List</param>
        /// <param name="computers">List</param>
        /// <param name="operatingSystems">List</param>
        /// <param name="viruses">List</param>
        static public void ParseFile(string filePath, out List<List<bool>> networkConfiguration, out List<Computer> computers, out List<string> operatingSystems, out List<Virus> viruses)
        {
            networkConfiguration = new List<List<bool>>();
            computers = new List<Computer>();
            operatingSystems = new List<string>();
            viruses = new List<Virus>();

            StreamReader streamReader = new StreamReader(filePath);
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

                    viruses[i].AddOS(tempFloat);
                }
            }

            List<Computer> tempComputersList = new List<Computer>(computers);
            List<List<bool>> tempConfiguration = new List<List<bool>>(networkConfiguration);
            List<Virus> tempVirusesList = new List<Virus>(viruses);

            for (int i = 0; i < computersQuantity; i++)
            {
                streamReader.ReadLine();
                int computerOS = 0;

                int.TryParse(streamReader.ReadLine(), out computerOS);
                tempComputersList.Add(new Computer(operatingSystems[computerOS - 1]));

                ParseString(i, streamReader.ReadLine(), delegate(int first, int second)
                {
                    tempComputersList[first].Infect(tempVirusesList[second - 1].GetName());
                });

                ParseString(i, streamReader.ReadLine(), delegate(int first, int second)
                {
                    tempConfiguration[first][second - 1] = true;
                });
            }

            computers = tempComputersList;
            networkConfiguration = tempConfiguration;
        }

        private delegate void twoIntsDependingFunction(int firstInt, int secondInt);

        /// <summary>
        /// Parses <paramref="buffer"> and performs <paramref name="func"/> with results
        /// </summary>
        /// <param name="i">First parameter for func</param>
        /// <param name="buffer">String to parse</param>
        /// <param name="func">Function to perform</param>
        static private void ParseString(int i, string buffer, twoIntsDependingFunction func)
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
    }
}