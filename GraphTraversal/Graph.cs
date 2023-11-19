namespace GraphTraversal
{
    public class Graph
    {
        private int NumberOfVertices { get; set; }
        private GraphOrientation Orientation { get; set; }
        private LinkedList<int>[] AdjacencyLists { get; set; }

        public Graph(int v, GraphOrientation orientation)
        {
            NumberOfVertices = v;
            Orientation = orientation;
            AdjacencyLists = new LinkedList<int>[v];
            for (int i = 0; i < AdjacencyLists.Length; i++)
                AdjacencyLists[i] = new();
        }

        public enum GraphOrientation
        {
            Bidirected,
            Directed
        }

        public void AddEdge(int v, int w)
        {
            if (!AdjacencyLists[v].Contains(w))
                AdjacencyLists[v].AddLast(w);

            if (Orientation == GraphOrientation.Bidirected && !AdjacencyLists[w].Contains(v))
                AdjacencyLists[w].AddLast(v);
        }

        public void BFS(int s)
        {
            bool[] visited = new bool[NumberOfVertices];
            for (int i = 0; i < NumberOfVertices; i++)
                visited[i] = false;

            LinkedList<int> queue = new();

            visited[s] = true;
            queue.AddLast(s);

            while (queue.Any())
            {
                s = queue.First();
                Console.Write(s + " ");
                queue.RemoveFirst();

                LinkedList<int> list = AdjacencyLists[s];

                foreach (int val in list)
                {
                    if (!visited[val])
                    {
                        visited[val] = true;
                        queue.AddLast(val);
                    }
                }
            }
        }

        private void DFSUtil(int v, bool[] visited)
        {
            visited[v] = true;
            Console.Write(v + " ");

            LinkedList<int> vList = AdjacencyLists[v];
            foreach (int n in vList)
            {
                if (!visited[n])
                    DFSUtil(n, visited);
            }
        }

        public void DFS(int v)
        {
            bool[] visited = new bool[NumberOfVertices];
            DFSUtil(v, visited);
        }
    }
}
