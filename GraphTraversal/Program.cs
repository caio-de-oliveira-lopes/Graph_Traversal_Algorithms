using System.Diagnostics;

namespace GraphTraversal
{
    public class Program
    {
        static readonly Random Random = new();
        static void Main(string[] args)
        {
            string inputPath;
            if (args.Length > 0)
                inputPath = args[0];
            else
            {
                Console.WriteLine("No Input Path Was Informed.");
                return;
            }

            string? line;
            Graph myGraph;
            long elapsedMilliseconds;
            HashSet<int> candidates = new();
            foreach (Graph.GraphAdjacency graphAdjacency in Enum.GetValues<Graph.GraphAdjacency>())
            {
                try
                {
                    StreamReader sr = new(inputPath);

                    line = sr.ReadLine();

                    if (line is null) return;

                    // First Line
                    int numberOfVertices = int.Parse(line);

                    Console.WriteLine($"Initializing Graph Using Adjacency {graphAdjacency}.");
                    myGraph = new(numberOfVertices, Graph.GraphOrientation.Bidirected, graphAdjacency);

                    while (sr.Peek() >= 0)
                    {
                        line = sr.ReadLine();
                        if (line is null) return;

                        string[] splitedLines = line.Split(" ");
                        int v = int.Parse(splitedLines.First());
                        int w = int.Parse(splitedLines.Last());

                        myGraph.AddEdge(v, w);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    Console.WriteLine("Error When Reading Input File.");
                    return;
                }

                while (candidates.Count < 10)
                    candidates.Add(Random.Next(0, myGraph.NumberOfVertices));

                foreach (int canditate in candidates)
                {
                    foreach (Graph.GraphTraversalMethod graphTraversalMethod in Enum.GetValues<Graph.GraphTraversalMethod>())
                    {
                        elapsedMilliseconds = myGraph.Traversal(canditate, graphTraversalMethod, false);
                        Console.WriteLine($"Elapsed Milliseconds on {graphTraversalMethod}: {elapsedMilliseconds}.");
                    }
                }

                Process currentProcess = Process.GetCurrentProcess();
                long memoryUsage = currentProcess.WorkingSet64;
                Console.WriteLine($"Adjacency {graphAdjacency} Memory Usage: {memoryUsage / 1048576}MB.\n");
            }
        }
    }
}