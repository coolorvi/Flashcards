using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Spectre.Console;
using System.Data;

namespace Flashcards.Database
{
    public class Initialize
    {
        private string _connectionString = null!;
        public void InitializeDb()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            if (connectionString == null)
            {
                AnsiConsole.MarkupLine("[red]Oops! Connection string not found.[/]");
                return;
            }

            _connectionString = connectionString;

            var masterConnectionString = _connectionString.Replace("Database=Flashcards", "Database=master");

            using IDbConnection masterConnection = new SqlConnection(masterConnectionString);

            var request = "IF DB_ID('Flashcards') IS NULL CREATE DATABASE Flashcards";

            masterConnection.Execute(request);

            using IDbConnection connection = new SqlConnection(_connectionString);

            request = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Stacks') CREATE TABLE Stacks (ID INT IDENTITY(1,1) PRIMARY KEY, Name NVARCHAR(300), CountCards INT)";

            connection.Execute(request);

            request = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Cards') CREATE TABLE Cards (ID INT IDENTITY(1,1) PRIMARY KEY, IdStack INT FOREIGN KEY REFERENCES Stacks(ID), Title NVARCHAR(300), Description NVARCHAR(1000));";

            connection.Execute(request);

            request = "IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'Sessions') CREATE TABLE Sessions (ID INT IDENTITY(1,1) PRIMARY KEY, Date DATETIME NOT NULL, Score INT)";

            connection.Execute(request);
        }

    }
}
