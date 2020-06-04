using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Admin
    {
        public Guid AdminId { get; set; }
        public string AdminUsername { get; set; }
        public string Password { get; set; }
    }
}
