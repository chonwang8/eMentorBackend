using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class MentorViewModel
    {
        public Guid MentorId { get; set; }
        public Guid UserId { get; set; }
        public bool IsDisable { get; set; }
    }
}
