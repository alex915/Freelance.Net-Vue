using System.Collections.Generic;

namespace Models
{
    public partial class Activity
    {
        public Activity()
        {
            Companies = new HashSet<Company>();
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Company> Companies { get; set; }
    }
}
