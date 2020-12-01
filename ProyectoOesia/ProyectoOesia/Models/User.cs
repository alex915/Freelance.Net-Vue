using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Models
{
    public partial class User: IdentityUser
    {
        public User()
        {
          
            Contacts = new HashSet<Contact>();
        }

        public virtual Company Company { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
    }
}
