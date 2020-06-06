using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MentorModel
    {
        public Guid MentorId { get; set; }
        public Guid UserId { get; set; }
        public bool IsDisable { get; set; }
    }
}
