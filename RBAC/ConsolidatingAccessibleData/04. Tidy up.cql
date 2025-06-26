MATCH (n) CALL(n) { DETACH DELETE (n) };
CALL apoc.schema.assert({}, {});

DROP ROLE specialAccess IF EXISTS;
DROP ROLE publicViewer IF EXISTS;
DROP USER publicUser IF EXISTS;
DROP USER specialAccessUser IF EXISTS;