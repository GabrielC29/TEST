using System;
using System.Collections.Generic;

namespace TEST.Model
{
    public partial class Genre
    {
        public Genre()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Sex { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
