# README #

1 .- Crear una base de datos nueva en SQL Server y pasar el script "OpenBeerDataBase.sql" que se encuentra en la carpeta "NoSQLBeerDB_Script" de esta solución.

2 .- Cambiar las conexiones a base de datos de la solución. Se puede hacer de dos maneras:

2.1.1 .- En el fichero "NoSQLBeerCore/SqlServer/BeerDB.edmx", botón derecho, "actualizar modelo desde base de datos..." y estableciendo una nueva conexión a la base de datos que hayas creado.
2.1.2 .- Abrir el fichero "NoSQLBeerCore/App.Config" y copiar el siguiente nodo a los App config de los otros proyectos "FromSqlServerToElasticSearch", "FromSqlServerToNeo4j"... para que se puedan conectar.
<connectionStrings>
	<add name="..." connectionString="....." .... />
</connectionStrings>

2.2 .- Modificar manualmente el fichero "NoSQLBeerCore/App.Config" y cambiar los valores del attributo "connectionString" para que apunte a la base de datos que hayas creado.

### What is this repository for? ###

* Quick summary
Solución en Visual Studio 2012 para migrar los datos de la base de datos SQLServer que se crea con el script "NoSQLBeerDB_Script/OpenBeerDataBase.sql" a las bases de datos NoSQL como Neo4j, ElasticSearch,...

* Version
0.0.01
