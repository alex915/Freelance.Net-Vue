using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Models
{
    public partial class User: IdentityUser
    {
        public User()
        {
            Companies = new HashSet<Company>();
            Contacts = new HashSet<Contact>();
        }

        public virtual ICollection<Company> Companies { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
