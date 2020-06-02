using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class UserSharing
    {
        public UserSharing()
        {
            Comment = new HashSet<Comment>();
        }

        public Guid UserSharingId { get; set; }
        public Guid UserId { get; set; }
        public Guid SharingId { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsAttended { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual Sharing Sharing { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
    }
}
