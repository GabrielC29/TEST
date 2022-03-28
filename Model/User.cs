using System;
using System.Collections.Generic;

namespace TEST.Model
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public int? Genre { get; set; }
        public string? Dni { get; set; }
        public string? Phone { get; set; }
        public System.DateTime Datebirth { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }

        public virtual Genre? GenreNavigation { get; set; }
    }
}
