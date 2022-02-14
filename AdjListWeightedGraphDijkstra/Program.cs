using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GraphAdjList
{
    public class Adjacency                                                  // class of Adjacency, containing next vertex and weight(cost) of edge
    {
        public int EndVertex { get; set; }
        public int Weight { get; set; }

        public Adjacency(int e, int w)
        {
            EndVertex = e;
            Weight = w;
        }
        public Adjacency(int s, int e, int w)
        {
            EndVertex = e;
            Weight = w;
        }
    }

    class BasicGraph
    {
        const int INFINITY = 9999;
        Dictionary<int, List<Adjacency>> myGraph;

        public override string ToString()
        {
            return $"{myGraph}";
        }

        public BasicGraph()
        {
            myGraph = new Dictionary<int, List<Adjacency>>();
        }


        void AddEdge(int startVertex, Adjacency adj)
        {
            if (myGraph.ContainsKey(startVertex))                            // Key (source vertex) already contained in the dictionary so we keep adding neighbors and weights in its Adjacency list
            {
                List<Adjacency> adjList = myGraph[startVertex];
                if (adjList.Contains(adj) == false)
                {
                    adjList.Add(adj);
                }
            }
            else                                                            // Adding a new starting vertex (key) and and a list of adjacencies-weights to the Dictionary
            {
                List<Adjacency> adjList = new List<Adjacency>();
                adjList.Add(adj);
                myGraph.Add(startVertex, adjList);
            }
        }

        void Display()
        {
            foreach (var contents in myGraph.Keys)
            {
                Console.Write("{{Vertex}}\n (" + contents + ") ");
                foreach (var adjList in myGraph[contents])
                {
                    Console.Write("[With Neighbor Vertex (" + adjList.EndVertex + ") and Cost of directed edge = " + adjList.Weight + "]");
                }
                Console.WriteLine("\n");
            }
        }
        public int mindistance(int[] dist, bool[] sptSet, int numOfVerts)   // function to choose the nearest vertex with the minimum distance comparing the distances of dist array
        {
            int min = INFINITY;
            int min_index = -1;
            for (int i = 0; i < numOfVerts; i++)
            {
                if (sptSet[i] == false && dist[i] <= min)                  // checking if vertex is not visited and if distance is smaller than the previously min
                {
                    min = dist[i];
                    min_index = i;
                }
            }
                
            return min_index;
        }
        public void Dijkstra(BasicGraph g, int src, int numOfVertices)
        {
            int[] dist = new int[numOfVertices];                           // Array of min distances
            bool[] sptSet = new bool[numOfVertices];                       // Array of visited
            int[] Prev = new int[numOfVertices];                           // Array of parents when minimum distance

            for (int i = 0; i < numOfVertices; i++)
            {
                Prev[i] = -1;                                              // Fill array of parents with -1
                dist[i] = INFINITY;                                        // Fill distances to each node array with infinity
                sptSet[i] = false;                                         // Every node unvisited
            }

            dist[src] = 0;

            for (int i = 0; i < numOfVertices-1; i++)
            {
                int u = mindistance(dist, sptSet, numOfVertices);

                sptSet[u] = true;

                for (int j=0; j < myGraph[u].Count; j++)
                {
                    if (!sptSet[myGraph[u][j].EndVertex] && myGraph[u][j].Weight!=0 && dist[u]!=INFINITY && dist[u] + myGraph[u][j].Weight < dist[myGraph[u][j].EndVertex])
                    {
                        dist[myGraph[u][j].EndVertex] = dist[u] + myGraph[u][j].Weight;
                        Prev[myGraph[u][j].EndVertex] = u;
                    }
                }
            }

            Console.WriteLine();
            printSolution(dist, Prev, src);

        }

        void printSolution(int[] dist, int[] prev, int src)
        {
            Console.WriteLine("[Vertex]\t[Distance]\t[Path]");
            for (int i = 0; i < 6; i++)
            {
                Console.Write("\n{0} -> {1}\t\t{2}\t\t{3}", src, i, dist[i], src); //Printing the smallest distances
                printPath(prev, i);
            }
        }

        void printPath(int[] prev, int j)                                         // Recursive printing of the full complete path
        {
            if (prev[j] == -1)
                return;
            printPath(prev, prev[j]);
            Console.Write(" -> " + j);
        }

        static void Main(string[] args)
        {
            BasicGraph g = new BasicGraph();
            g.AddEdge(0, new Adjacency(2, 4));                                    // Adding vertices and adcacencies
            g.AddEdge(0, new Adjacency(1, 2));
            g.AddEdge(1, new Adjacency(2, 1));
            g.AddEdge(1, new Adjacency(3, 7));
            g.AddEdge(2, new Adjacency(4, 3));
            g.AddEdge(3, new Adjacency(5, 1));
            g.AddEdge(4, new Adjacency(3, 2));
            g.AddEdge(4, new Adjacency(5, 5));
            g.Display();
            g.Dijkstra(g, 0, 6);
            Console.WriteLine("\n");
            Console.ReadLine();
        }
    }
}