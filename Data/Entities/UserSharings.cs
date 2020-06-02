using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class UserSharings
    {
        public UserSharings()
        {
            Comments = new HashSet<Comments>();
        }

        public Guid UserSharingId { get; set; }
        public Guid UserId { get; set; }
        public Guid SharingId { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsAttended { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual Sharings Sharing { get; set; }
        public virtual Users User { get; set; }
        public virtual ICollection<Comments> Comments { get; set; }
    }
}
