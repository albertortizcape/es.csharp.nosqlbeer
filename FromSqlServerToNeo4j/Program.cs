using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoSQLBeerCore.Neo4j;
using NoSQLBeerCore.SqlServer;

namespace FromSqlServerToNeo4j
{
    class Program
    {
        private static Uri pNeo4jUri = new Uri("http://localhost:7474/db/data");
        static void Main(string[] args)
        {
            Neo4j neo4jDB = new Neo4j(pNeo4jUri);

            FromSqlServerToNeo4j dataMigrator = new FromSqlServerToNeo4j(neo4jDB);

            // Create Neo4j DataBase
            //dataMigrator.CreateNeo4jDatabase();

            // Query SqlServer and Insert Nodes
            dataMigrator.Migrate();
        }
    }
}
