using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Helper.DataObjects
{
    public class UserAuthModel
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
