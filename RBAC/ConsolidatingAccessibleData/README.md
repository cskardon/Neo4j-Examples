# Consolidating Accessible Data based on Permissions

## Key Facts

| Works in | ? |  Notes |
-------|---------|---|
| Aura |  ✅ | Professional+ SKU |
| Local | ✅ | Enterprise, APOC Core |

## What does it demo?

You have a 'resolved' node that is has multiple sources, and you want to see a consolidated view of that node. 

In this example, certain users can only see certain parts of the DB, so if they execute the same query they will get a different result.

## Setup

You need an instance that can create USER accounts and GROUPS.

1. Run the contents of the [`00. Create Database.cql`](00.%20Create%20Database.cql) script 
   1. ⚠️ This will WIPE your existing DB ⚠️
2. Run the contents of the [`01. Setup Users and Permissions.cql`](01.%20Setup%20Users%20And%20Permissions.cql) file.
3. Login as `publicViewer` (password: 'neo4jneo4j')
   1. Execute the script in [`02. Query.cql`](02.%20Query.cql)
4. Login as `specialAccessUser` (password: 'neo4jneo4j')
   1. Execute the script in [`02. Query.cql`](02.%20Query.cql)
5. Compare the differences in results
6. _OPTIONAL_ Tidy up the database using the [`04. Tidy up.cql`](04.%20Tidy%20up.cql) file