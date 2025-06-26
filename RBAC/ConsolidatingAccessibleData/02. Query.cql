//Gets virtual graph
// ResolvedEntity is an empty node acting as an intermediary to allow for collapsing
// Collapsing is done on ROLE permissions
MATCH (bear:Entity WHERE bear.name = 'Mr P Bear')<--(r:ResolvedEntity)
OPTIONAL MATCH (r)-->(other:Entity)
WHERE other <> bear
WITH r, bear, collect(other) AS nodes
WITH [r] + [bear] + nodes AS subgraph
CALL apoc.nodes.collapse(subgraph,{properties:'combine'}) YIELD from, rel, to
RETURN from;