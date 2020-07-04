using System;

namespace Domain.ViewModels.CommentModels
{
    public class GetCommentViewModel
    {
        public Guid CommentId { get; set; }
        public Guid SharingId { get; set; }
        public string CommentContent { get; set; }
    }
}
