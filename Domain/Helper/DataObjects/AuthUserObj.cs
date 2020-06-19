using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helper.DataObjects
{
    public class AuthUserObj
    {
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string UserRole { get; set; }

    }
}
