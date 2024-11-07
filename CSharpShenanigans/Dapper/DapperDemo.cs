using System.Data;
using System.Data.SqlClient;
using CSharpShenanigans.Dapper.Models;
using Dapper;

namespace CSharpShenanigans.Dapper;

public class DapperDemo
{
    string connectionString = "Server=.;Database=DapperDemo;Trusted_Connection=True;";

    public async Task TestDapperDemo()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            string sql = "SELECT Id, FirstName, LastName FROM Users";
            var users = (await connection.QueryAsync<User>(sql)).ToList();

            Console.WriteLine("Users in the database:");
            foreach (var user in users)
            {
                Console.WriteLine($"{user.FirstName} {user.LastName}");
            }

            // INSERT
            var newUser = new User { FirstName = "John", LastName = "Doe" };
            string insertSql = "INSERT INTO Users (FirstName, LastName) VALUES (@FirstName, @LastName)";
            var affectedRows = await connection.ExecuteAsync(insertSql, newUser);
            Console.WriteLine($"Inserted {affectedRows} rows new user(s).");

            // UPDATE
            var userToUpdate = new User { Id = 2, FirstName = "DomePlane" }; // Assuming Id=2 exists
            string updateSql = "UPDATE Users SET FirstName = @FirstName WHERE Id = @Id";
            affectedRows = await connection.ExecuteAsync(updateSql, userToUpdate);
            Console.WriteLine($"Updated {affectedRows} rows user(s).");

            // DELETE
            string deleteSql = "DELETE FROM Users WHERE Id = @Id"; // Assuming Id=1 exists
            affectedRows = await connection.ExecuteAsync(deleteSql, new { Id = 1 });
            Console.WriteLine($"Deleted {affectedRows} rows user(s).");
        }
    }

    public async Task TestDapperDemoStoredProcedure()
    {
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();

            using (var multi = await connection.QueryMultipleAsync("sp_GetUserAndOrders", new { UserId = 3 },
                       commandType: CommandType.StoredProcedure))
            {
                var user = (await multi.ReadAsync<User>()).FirstOrDefault();

                var orders = (await multi.ReadAsync<Order>()).ToList();

                if (user is null)
                {
                    Console.WriteLine("No user found.");
                    return;
                }

                Console.WriteLine($"User: {user.FirstName} {user.LastName}");

                Console.WriteLine($"Orders:");
                foreach (var order in orders)
                {
                    Console.WriteLine($"Order Date: {order.OrderDate}, Amount: {order.Amount}");
                }
            }
        }
    }
}
