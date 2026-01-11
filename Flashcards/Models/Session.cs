namespace Flashcards.Models
{
    internal class Session
    {
        public int Id { get; set; }
        public required string Date { get; set; }
        public int Score { get; set; }
    }
}
