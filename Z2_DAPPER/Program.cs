using System;
using Dapper;
using System.Data.SqlClient;

namespace Z2_DAPPER
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = @"Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
            using var connection = new SqlConnection(connectionString);

              //dapper robiony
              
            var sql = "SELECT * FROM REGION";
            var regions = connection.Query<Region>(sql);
            
                       var region = new Region() 
                       { 
                           RegionDescription = "dapper obiekt", 
                           RegionId = 101
                       };

                       var insertSql = "INSERT INTO REGION(regionId, regionDescription) VALUES (@RegionId, @regionDescription)";
                       var result = connection.Execute(insertSql,

                           new { id = 100, desc = "dapper"}
                           );

            var DB = new DB();
            DB.Select(connection);

            var region1 = new Region()
            {
                RegionDescription = "dapper obiekt",
                RegionId = 102
            };

            DB.Select(connection);
            DB.Insert(connection, region);
            DB.Delete(connection, 101);
            DB.SelectOrder(connection, 10290);



        }
    }
}
