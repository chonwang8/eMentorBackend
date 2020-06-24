using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Comment
    {
        public Guid CommentId { get; set; }
        public Guid ParentCommendId { get; set; }
        public Guid SharingId { get; set; }
        public string CommentContent { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsDisable { get; set; }

        public virtual Sharing Sharing { get; set; }
    }
}
