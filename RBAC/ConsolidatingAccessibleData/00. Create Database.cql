MATCH (n) CALL(n) { DETACH DELETE (n) };
CALL apoc.schema.assert({}, {});

CREATE (a:Entity {name:'Paddington Bear', dob: date('2000-02-10')})
CREATE (b:Entity:SpecialAcces {name:'P Bear', dob: date('2000-02-10'), address:  "1 Nowhere Lane, Norfolk", source: "OpX"})
CREATE (d:Entity {name: 'Mr P Bear'})
//Notes!
// Might it be better to have the OpX as a label?

CREATE (r:ResolvedEntity)
CREATE (r)-[:PUBLIC]->(a)
CREATE (r)-[:PUBLIC]->(d)
CREATE (r)-[:OPX]->(b);