using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Flashcards.Database
{
    internal class Stacks
    {
        private readonly string _connectionString;

        public Stacks(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Flashcards.Models.Stack> ReadAllStacks()
        {
            using var connection = new SqlConnection(_connectionString);

            var request = "SELECT Name, Id FROM Stacks";

            var nameStacks = connection.Query<Flashcards.Models.Stack>(request);

            return nameStacks.ToList();
        }

        public void AddStack(string nameStack)
        {
            using var connection = new SqlConnection(_connectionString);

            var request = "INSERT INTO Stacks (Name, CountCards) VALUES (@Name, @CountCards)";

            connection.Execute(request, new { Name = nameStack, CountCards = 0 });
        }

        public void DeleteStack(int idStack)
        {
            using var connection = new SqlConnection(_connectionString);

            // Add delete linked cards 

            var request = "DELETE FROM Stacks WHERE Id = @Id";

            connection.Execute(request, new { Id = idStack });
        }

        public void UpdateNameStack(string newNameStack, int idStack)
        {
            using var connection = new SqlConnection(_connectionString);

            var request = "UPDATE Stacks SET Name = @Name WHERE Id = @Id";

            connection.Execute(request, new { Name =  newNameStack, Id = idStack });
        }
    }
}
