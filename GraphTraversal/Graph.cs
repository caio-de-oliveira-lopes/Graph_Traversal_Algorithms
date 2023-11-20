using System.Diagnostics;

namespace GraphTraversal
{
    public class Graph
    {
        public int NumberOfVertices { get; private set; }
        public GraphOrientation Orientation { get; private set; }
        public GraphAdjacency AdjacencyType { get; private set; }
        private bool[][]? AdjacencyMatrix { get; set; }
        private LinkedList<int>[]? AdjacencyLists { get; set; }

        public Graph(int v, GraphOrientation orientation, GraphAdjacency adjacencyType)
        {
            NumberOfVertices = v;
            Orientation = orientation;
            AdjacencyType = adjacencyType;
            if (AdjacencyType == GraphAdjacency.Matrix)
            {
                AdjacencyMatrix = new bool[v][];
                for (int i = 0; i < AdjacencyMatrix.Length; i++)
                    AdjacencyMatrix[i] = new bool[v];
            }
            else if (AdjacencyType == GraphAdjacency.List)
            {
                AdjacencyLists = new LinkedList<int>[v];
                for (int i = 0; i < AdjacencyLists.Length; i++)
                    AdjacencyLists[i] = new();
            }
        }

        public enum GraphOrientation
        {
            Bidirected,
            Directed
        }

        public enum GraphAdjacency
        {
            Matrix,
            List
        }

        public enum GraphTraversalMethod
        {
            BFS,
            DFS
        }

        public void AddEdge(int v, int w)
        {
            if (AdjacencyType == GraphAdjacency.List && AdjacencyLists != null)
            {
                if (!AdjacencyLists[v].Contains(w))
                    AdjacencyLists[v].AddLast(w);

                if (Orientation == GraphOrientation.Bidirected && !AdjacencyLists[w].Contains(v))
                    AdjacencyLists[w].AddLast(v);
            }
            else if (AdjacencyType == GraphAdjacency.Matrix && AdjacencyMatrix != null)
            {
                AdjacencyMatrix[v][w] = true;
                if (Orientation == GraphOrientation.Bidirected)
                    AdjacencyMatrix[w][v] = true;
            }
        }

        public long Traversal(int v, GraphTraversalMethod graphTraversalMethod, bool print)
        {
            if (graphTraversalMethod == GraphTraversalMethod.BFS)
                return BFS(v, print);
            else if (graphTraversalMethod == GraphTraversalMethod.DFS)
                return DFS(v, print);
            else
                return 0;
        }

        private LinkedList<int> GetAdjacency(int v)
        {
            if (AdjacencyType == GraphAdjacency.List && AdjacencyLists != null)
            {
                return AdjacencyLists[v];
            }
            else if (AdjacencyType == GraphAdjacency.Matrix && AdjacencyMatrix != null)
            {
                LinkedList<int> adjacency = new();
                for (int i = 0; i < AdjacencyMatrix[v].Length; i++)
                    if (AdjacencyMatrix[v][i])
                        adjacency.AddLast(i);
                return adjacency;
            }
            else
                return new();
        }

        private long BFS(int v, bool print)
        {
            Console.WriteLine($"BFS: Starting on vertex {v}");
            Stopwatch watch = new();
            watch.Start();
            bool[] visited = new bool[NumberOfVertices];
            for (int i = 0; i < NumberOfVertices; i++)
                visited[i] = false;

            LinkedList<int> queue = new();

            visited[v] = true;
            queue.AddLast(v);

            while (queue.Any())
            {
                v = queue.First();
                if (print)
                    Console.Write($"{v} ");
                queue.RemoveFirst();

                LinkedList<int> list = GetAdjacency(v);

                foreach (int val in list)
                {
                    if (!visited[val])
                    {
                        visited[val] = true;
                        queue.AddLast(val);
                    }
                }
            }
            watch.Stop();
            Console.WriteLine();
            return watch.ElapsedMilliseconds;
        }

        private void DFSUtil(int v, bool[] visited, bool print)
        {
            visited[v] = true;
            if (print)
                Console.Write($"{v} ");

            LinkedList<int> vList = GetAdjacency(v);
            foreach (int n in vList)
            {
                if (!visited[n])
                    DFSUtil(n, visited, print);
            }
        }

        private long DFS(int v, bool print)
        {
            Console.WriteLine($"DFS: Starting on vertex {v}");
            bool[] visited = new bool[NumberOfVertices];
            Stopwatch watch = new();
            watch.Start();
            DFSUtil(v, visited, print);
            watch.Stop();
            Console.WriteLine();
            return watch.ElapsedMilliseconds;
        }
    }
}
