using System;

namespace Domain.Models.CommentModels
{
    public class GetCommentViewModel
    {
        public Guid CommentId { get; set; }
        public Guid SharingId { get; set; }
        public string CommentContent { get; set; }
    }
}
