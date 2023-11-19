namespace GraphTraversal
{
    public class Program
    {
        static void Main(string[] args)
        {
            Graph g = new Graph(4, Graph.GraphOrientation.Bidirected);
            g.AddEdge(0, 1);
            g.AddEdge(0, 2);
            g.AddEdge(1, 2);
            g.AddEdge(2, 0);
            g.AddEdge(2, 3);
            g.AddEdge(3, 3);

            Console.WriteLine("Following is Breadth First "
                          + "Traversal(starting from "
                          + "vertex 2)");
            g.BFS(2);

            Console.WriteLine(
            "\n\nFollowing is Depth First Traversal "
            + "(starting from vertex 2)");

            // Function call
            g.DFS(2);
        }
    }
}