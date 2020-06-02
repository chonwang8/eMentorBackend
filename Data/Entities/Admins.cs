using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Admins
    {
        public Guid AdminId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
