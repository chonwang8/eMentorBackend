using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class AdminViewModel
    {
        public Guid AdminId { get; set; }
        public string AdminUsername { get; set; }
    }

    public class AdminLoginViewModel
    {
        public string AdminUsername { get; set; }
        public string Password { get; set; }
    }
}
