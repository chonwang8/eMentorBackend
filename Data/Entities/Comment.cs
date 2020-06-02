using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Comment
    {
        public Guid CommentId { get; set; }
        public Guid UserSharingId { get; set; }
        public string CommentContent { get; set; }
        public bool IsDisable { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual UserSharing UserSharing { get; set; }
    }
}
