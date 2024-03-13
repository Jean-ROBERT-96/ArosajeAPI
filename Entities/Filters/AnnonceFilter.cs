namespace Entities.Filters
{
    public class AnnonceFilter
    {
        public DateTime? PosteDe { get; set; }
        public DateTime? PosteA { get; set; }
        public string? Title { get; set; }
        public long? UserId { get; set; }
        public EnumEtat? Etat { get; set; }
    }
}
