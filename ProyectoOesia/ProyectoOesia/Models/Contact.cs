using System;

namespace Models
{
    public partial class Contact
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string UserId { get; set; }
        public DateTime? Date { get; set; }

        public virtual User User { get; set; }
        public bool Contacted { get; internal set; }
    }
}
