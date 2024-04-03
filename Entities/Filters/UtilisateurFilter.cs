namespace Entities.Filters
{
    public class UtilisateurFilter : IFilter
    {
        public long? UserId { get; set; }
        public string? Mail { get; set; }
        public string? Nom { get; set; }
        public string? Prenom { get; set; }
        public DateTime? CreeDe { get; set; }
        public DateTime? CreeA { get; set; }
        public bool? EstBotaniste { get; set; }
        public bool? EstModerateur { get; set; }
    }
}
