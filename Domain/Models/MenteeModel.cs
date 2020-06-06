using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models
{
    public class MenteeModel
    {
        public Guid MenteeId { get; set; }
        public Guid UserId { get; set; }
        public bool IsDisable { get; set; }
    }
}
