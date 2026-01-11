namespace Flashcards.Models
{
    public class Card
    {
        public int Id { get; set; }
        public int IdStack { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }

    }

    public class CardDTO
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
    }
}
