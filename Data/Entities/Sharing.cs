using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Sharing
    {
        public Sharing()
        {
            Comment = new HashSet<Comment>();
            Enroll = new HashSet<Enroll>();
        }

        public Guid SharingId { get; set; }
        public string SharingName { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Maximum { get; set; }
        public double Price { get; set; }
        public Guid ChannelId { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDisable { get; set; }
        public bool IsApproved { get; set; }
        public DateTime ApprovedTime { get; set; }

        public virtual Channel Channel { get; set; }
        public virtual ICollection<Comment> Comment { get; set; }
        public virtual ICollection<Enroll> Enroll { get; set; }
    }
}
