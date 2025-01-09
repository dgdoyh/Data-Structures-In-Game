using System.Collections.Generic;


// Undirected Graph
public class Graph
{
    private Dictionary<string, List<string>> adjacencyList;

    public Graph()
    {
        adjacencyList = new Dictionary<string, List<string>>();
    }

    public void AddVertex(string vertex)
    {
        if (!adjacencyList.ContainsKey(vertex))
        {
            adjacencyList[vertex] = new List<string>();
        }
    }

    public void AddEdge(string vertex1, string vertex2)
    {
        if (adjacencyList.ContainsKey(vertex1) && adjacencyList.ContainsKey(vertex2))
        {
            adjacencyList[vertex1].Add(vertex2);
            adjacencyList[vertex2].Add(vertex1);
        }
    }

    public List<string> GetNeighbors(string vertex)
    {
        if (adjacencyList.ContainsKey(vertex))
        {
            return adjacencyList[vertex];
        }

        else
        {
            return new List<string>();
        }
    }
}
