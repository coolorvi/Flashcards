using Dapper;
using Microsoft.Data.SqlClient;

namespace Flashcards.Database
{
    internal class Cards
    {
        private readonly string _connectionString;

        public Cards(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Flashcards.Models.Card> ReadAllCards()
        {
            using var connection = new SqlConnection(_connectionString);

            var request = "SELECT * FROM Cards";

            var cards = connection.Query<Flashcards.Models.Card>(request);

            return cards.ToList();
        }

        public void AddCard(int idStack, string title, string description)
        {
            using var connection = new SqlConnection(_connectionString);

            var request = "INSERT INTO Cards (IdStack, Title, Description) VALUES (@IdStack, @Title, @Description)";

            connection.Execute(request, new { IdStack = idStack, Title = title, Description = description });
        }

        public void DeleteCard(int idCard)
        {
            using var connection = new SqlConnection(_connectionString);

            var request = "DELETE FROM Cards WHERE Id = @Id";

            connection.Execute(request, new { Id = idCard });
        }

        public void UpdateStackId(int idCard, int newIdStack)
        {
            using var connection = new SqlConnection(_connectionString);

            var request = "UPDATE Cards SET IdStack = @IdStack WHERE Id = @Id";

            connection.Execute(request, new { IdStack = newIdStack, Id = idCard });
        }

        public void UpdateFlashcardInfo(int idCard, string newTitle, string newDescription)
        {
            using var connection = new SqlConnection(_connectionString);

            var request = "UPDATE Cards SET Title = @Title, Description = @Description WHERE Id = @Id";

            connection.Execute(request, new { Title = newTitle, Description = newDescription, Id = idCard });
        }
    }
}
