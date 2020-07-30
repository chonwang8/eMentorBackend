using Domain.Models.ChannelModels;
using Domain.Models.CommentModels;
using Domain.Models.EnrollModels;
using System;
using System.Collections.Generic;

namespace Domain.Models.SharingModels
{
    public class SharingModel
    {
        public Guid SharingId { get; set; }
        public string SharingName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public int Maximum { get; set; }
        public double Price { get; set; }
        public Guid ChannelId { get; set; }
        public string imageUrl { get; set; }
        public bool IsDisable { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedTime { get; set; }

        public ChannelViewModel Channel { get; set; }
        public virtual ICollection<CommentViewModel> Comment { get; set; }
        public virtual ICollection<EnrollViewModel> Enroll { get; set; }
    }
}
