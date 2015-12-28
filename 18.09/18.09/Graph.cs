using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphNamespace
{
    public class Graph
    {
        private List<List<bool>> graphConfiguration;
        private List<bool> robots;

        /// <summary>
        /// Class constructor. Builds graph using file
        /// </summary>
        /// <param name="filePath">Configuration file path</param>
        public Graph(string filePath)
        {
            FileParser.ParseFile(filePath, out graphConfiguration, out robots);

            BuildDirectGraph();
        }

        /// <summary>
        /// Switches graph to one, on which robots' step is one
        /// </summary>
        private void BuildDirectGraph()
        {
            List<List<bool>> directGraph = new List<List<bool>>();

            for (int i = 0; i < graphConfiguration.Count(); i++)
            {
                List<bool> temp = new List<bool>();

                for (int j = 0; j < graphConfiguration.Count(); j++)
                {
                    temp.Add(false);
                }

                directGraph.Add(temp);
            }

            for (int i = 0; i < graphConfiguration.Count(); i++)
            {
                for (int j = 0; j < graphConfiguration.Count(); j++)
                {
                    if (graphConfiguration[i][j])
                    {
                        for (int k = 0; k < graphConfiguration.Count(); k++)
                        {
                            if (graphConfiguration[j][k])
                            {
                                directGraph[i][k] = true;
                                directGraph[k][i] = true;
                            }
                        }
                    }
                }
            }

            graphConfiguration = directGraph;
        }

        /// <summary>
        /// Fulfills concrete column in bools' list bu 'false'
        /// </summary>
        /// <param name="list">List</param>
        /// <param name="column">Column</param>
        /// <returns>List with changed column</returns>
        private static List<List<bool>> ClearColumn(List<List<bool>> list, int column)
        {
            for (int i = 0; i < list.Count(); i++)
            {
                list[i][column] = false;
            }

            return list;
        }

        /// <summary>
        /// Calculates spanning tree for graph out from concrete node
        /// </summary>
        /// <param name="startNode">Tree start node</param>
        /// <returns>Spanning tree</returns>
        private List<int> CalculateSpanningTree(int startNode)
        {
            List<List<bool>> tempGraphConfiguration = new List<List<bool>>();

            for (int i = 0; i < graphConfiguration.Count(); i++)
            {
                List<bool> temp = new List<bool>();

                for (int j = 0; j < graphConfiguration.Count(); j++)
                {
                    temp.Add(graphConfiguration[i][j]);
                }

                tempGraphConfiguration.Add(temp);
            }

            List<int> visitedNodes = new List<int>();
            visitedNodes.Add(startNode);

            tempGraphConfiguration = ClearColumn(tempGraphConfiguration, startNode);

            int counter = tempGraphConfiguration.Count() - 1;

            while (counter > 0)
            {
                bool nodeIsFound = false;
                int resultingNode = startNode;

                foreach (int node in visitedNodes)
                {
                    int i = 0;

                    while (i < tempGraphConfiguration.Count() && !nodeIsFound)
                    {
                        if (tempGraphConfiguration[node][i] && i != node)
                        {
                            nodeIsFound = true;
                            i--;
                        }

                        i++;
                    }

                    if (nodeIsFound)
                    {
                        resultingNode = i;
                        break;
                    }
                }

                if (nodeIsFound)
                {
                    visitedNodes.Add(resultingNode);
                    counter--;
                    nodeIsFound = false;

                    tempGraphConfiguration = ClearColumn(tempGraphConfiguration, resultingNode);
                }
                else
                {
                    break;
                }
            }

            return visitedNodes;
        }

        /// <summary>
        /// Checks if robots in graph are able to destroy themselves
        /// </summary>
        /// <returns></returns>
        public bool CheckDestroyingConsequenceExistence()
        {
            int robotsQuantity = 0;
            bool consequenceExists = true;
            
            for (int i = 0; i < graphConfiguration.Count(); i++)
            {
                robotsQuantity += robots[i] ? 1 : 0;
            }

            if (robotsQuantity > 1)
            {
                for (int i = 0; i < robots.Count(); i++)
                {
                    if (robots[i])
                    {
                        robots[i] = false;
                        List<int> minimalSpanningTree = CalculateSpanningTree(i);
                        int treeRobotsAmount = 1;

                        foreach (int node in minimalSpanningTree)
                        {
                            if (robots[node])
                            {
                                treeRobotsAmount++;
                                robots[node] = false;
                            }
                        }

                        if (treeRobotsAmount == 1)
                        {
                            consequenceExists = false;
                        }
                    }
                }
            }
                
            return consequenceExists;
        }
    }
}
