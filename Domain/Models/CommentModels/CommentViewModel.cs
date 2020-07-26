using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.CommentModels
{
    public class CommentViewModel
    {
        public Guid CommentId { get; set; }
        public Guid ParentCommendId { get; set; }
        public Guid SharingId { get; set; }
        public string CommentContent { get; set; }
    }
}
