using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class MenteeViewModel
    {
        public Guid MenteeId { get; set; }
        public Guid UserId { get; set; }
        public bool IsDisable { get; set; }
    }
}
