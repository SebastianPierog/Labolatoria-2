using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Dapper;

namespace Z2_DAPPER
{
    class DB
    {
        public void SelectOrder(IDbConnection connection, int id)
        {
            var sql = "SELECT * FROM Orders O JOIN Order_Details OD on O.OrderID = OD.OrderID WHERE O.OrderID = @id";
            var resultOrder = default(Order);
            var result = connection.Query<Order, OrderDetails, Order>
                (sql,(order, orderDetails) => 
                {
                    resultOrder ??= order; 
                                           

                    resultOrder.Details.Add(orderDetails);
                    return resultOrder;
                },
                new { id },
                splitOn: "OrderID"
                );
        }

        public void Select(IDbConnection connection)
        {
            var sql = "SELECT * FROM REGION";
            var regions = connection.Query<Region>(sql);
            foreach (var item in regions)
            {
                Console.WriteLine($"{item.RegionId}: {item.RegionDescription}");
            }
        }

        public int Insert(IDbConnection connection, Region region)
        {
            var insertSql = "INSERT INTO REGION(regionId, regionDescription) VALUES (@RegionId, @regionDescription)";
            return connection.Execute(insertSql, region);
        }

        public int Insert(IDbConnection connection, int id, string desc)
        {
            var insertSql = "INSERT INTO REGION(regionId, regionDescription) VALUES (@id, @desc)";
            return connection.Execute(insertSql,
                new {id = 0, desc = ""}

                );
        }

        public int Delete(IDbConnection connection, int id)
        {
            var sql = "DELETE FROM Region WHERE regionId = @id";
            return connection.Execute(sql, new { id });
        }
    }
}
