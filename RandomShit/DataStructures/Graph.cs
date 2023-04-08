namespace RandomShit;

internal class Graph
{
    public readonly List<Node> Nodes;
    public int NumberOfNodes => Nodes.Count;
    public List<List<int>> Matrix;
    
    private Graph()
    {
        Nodes = new List<Node>();
        Matrix = new List<List<int>>();
    }

    private static int RandomInt(int range)
    {
        return (int) Random.Shared.NextInt64(0, range - 1);
    }

    private Location OrtVorschlagen(int index)
    {
        var v = new List<int>();
        
        for (var i = 0; i < Matrix.Count; i++)
        {
            int connection = Matrix[index][i];
            if(connection > 0) v.Add(i);
        }

        int ort = RandomInt(v.Count);
        return Nodes[v[ort]].Location;
    }
}

internal class Node
{
    public Node(Location location)
    {
        Location = location;
    }

    public Location Location { get; set; }
}

internal class Location
{
    public string Name;

    public Location(string name)
    {
        Name = name;
    }
}