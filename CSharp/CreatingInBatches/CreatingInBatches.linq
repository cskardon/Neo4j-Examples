<Query Kind="Program">
  <NuGetReference>Neo4j.Driver</NuGetReference>
  <Namespace>Neo4j.Driver</Namespace>
  <Namespace>Neo4j.Driver.Mapping</Namespace>
  <Namespace>Neo4j.Driver.Preview.GqlErrors</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

async Task Main()
{
    var driver = GraphDatabase.Driver("neo4j://localhost:7687", AuthTokens.Basic("neo4j", "neo4jneo4j"));
    await driver.VerifyConnectivityAsync();

    string cypher = 
@"UNWIND $nodes AS node 
  CREATE (n:Resource:Test) 
  SET n = node.Properties 
  WITH n,node.Labels AS labels 
  UNWIND labels AS label 
  CALL apoc.create.addLabels(n,[label]) 
  YIELD node AS ns 
  RETURN count(ns) AS created";
  
//    var batches = GenerateBatches(5000, 10000000);
    var batches = GenerateBatches(5000, 10000);
    
    int batchesDone = 0;
    var start = DateTime.Now;
    foreach (var nodes in batches)
    {
        var batchStart = DateTime.Now;
        await driver.ExecutableQuery(cypher).WithParameters(new { nodes }).ExecuteAsync();
        Console.WriteLine($"{(++batchesDone)},{(DateTime.Now - batchStart).TotalMilliseconds}");
    }
    Console.WriteLine("Total time: " + (DateTime.Now - start));
}

public List<List<Resource>> GenerateBatches(int sizeOfBatch, int totalSize){
    int total = 0;
    List<List<Resource>> resourceBatches = new List<List<Resource>>();
    while (total < totalSize)
    {
        resourceBatches.Add(GenerateResources(sizeOfBatch, total, 3));
        total += sizeOfBatch;
    }
    return resourceBatches;    
}

public List<Resource> GenerateResources(int number, int startingPoint, int avgNumberOfLabels){
    var output = new List<Resource>();
    for (int i = startingPoint; i < startingPoint + number; i++)
    {
        var labels = GenerateLabels(i % avgNumberOfLabels);
        output.Add(new Resource
        {
            Name = $"Name-{i}",
            Number = i,
            Labels = labels
        });
    }
    return output;
}

public List<string> GenerateLabels(int number){
    var output = new List<string>();
    for(int i = 0; i < number; i++){
        output.Add($"Label{i}");
    }
    return output;
}

public class Resource
{
    public Dictionary<string, object> Properties 
    { 
        get { return new Dictionary<string, object> { { "Name", Name }, { "Number", Number } }; }
    }
    public string Name { get;set;}
    public int Number {get;set;}
    public List<string> Labels {get;set;} = new List<string>();
}
