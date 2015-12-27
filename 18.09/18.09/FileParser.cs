using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GraphNamespace
{
    class FileParser
    {
        /// <summary>
        /// Parses file to fulfill lists
        /// </summary>
        /// <param name="filePath">File path</param>
        /// <param name="graph">Graph table</param>
        /// <param name="robots">Robots location list</param>
        public static void ParseFile(string filePath, out List<List<bool>> graph, out List<bool> robots)
        {
            graph = new List<List<bool>>();
            robots = new List<bool>();

            StreamReader streamReader = new StreamReader(filePath);
            string buffer = "";

            int dotsQuantity = 0;
            buffer = streamReader.ReadLine();
            int.TryParse(buffer, out dotsQuantity);

            for (int i = 0; i < dotsQuantity; i++)
            {
                List<bool> temp = new List<bool>();

                for (int j = 0; j < dotsQuantity; j++)
                {
                    temp.Add(false);
                }

                graph.Add(temp);
            }

            int robotsQuantity = 0;
            buffer = streamReader.ReadLine();
            int.TryParse(buffer, out robotsQuantity);

            for (int i = 0; i < dotsQuantity; i++)
            {
                robots.Add(false);
            }

            streamReader.ReadLine();

            for (int i = 0; i < dotsQuantity; i++)
            {
                buffer = streamReader.ReadLine();

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

                        int dotNumber = 0;
                        int.TryParse(temp, out dotNumber);

                        j++;

                        graph[i][dotNumber - 1] = true;
                    }
                }
            }

            streamReader.ReadLine();

            for (int i = 0; i < robotsQuantity; i++)
            {
                buffer = streamReader.ReadLine();

                int dotNumber = 0;
                int.TryParse(buffer, out dotNumber);

                //if (dotNumber != 0)
                //{
                    robots[dotNumber - 1] = true;
                //}
            }
        }
    }
}
