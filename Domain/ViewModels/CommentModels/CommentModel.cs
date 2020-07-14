﻿using Domain.ViewModels.SharingModels;
using Domain.ViewModels.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.CommentModels
{
    public class CommentModel
    {
        public Guid CommentId { get; set; }
        public Guid ParentCommendId { get; set; }
        public Guid SharingId { get; set; }
        public string CommentContent { get; set; }
        public DateTime TimeStamp { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsDisable { get; set; }

        public virtual UserViewModel User { get; set; }
        public virtual SharingViewModel Sharing { get; set; }
    }
}
