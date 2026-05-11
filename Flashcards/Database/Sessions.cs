using Dapper;
using Microsoft.Data.SqlClient;

namespace Flashcards.Database
{
    internal class Sessions
    {
        private readonly string _connectionString;

        public Sessions(string connectionString)
        {
            _connectionString = connectionString;
        }
    }
}
