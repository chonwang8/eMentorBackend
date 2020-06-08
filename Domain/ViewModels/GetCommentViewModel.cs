using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels
{
    public class GetCommentViewModel
    {
        public Guid CommentId { get; set; }
        public Guid SharingId { get; set; }
        public string CommentContent { get; set; }
    }
}
