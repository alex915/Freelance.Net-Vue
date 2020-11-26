namespace Models
{
    public partial class SocialMedia
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public int Type { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
