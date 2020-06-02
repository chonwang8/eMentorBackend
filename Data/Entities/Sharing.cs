using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Sharing
    {
        public Sharing()
        {
            UserSharing = new HashSet<UserSharing>();
        }

        public Guid SharingId { get; set; }
        public string SharingName { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Guid TopicId { get; set; }
        public Guid ChannelId { get; set; }
        public bool IsDisable { get; set; }
        public Guid CreatedBy { get; set; }

        public virtual Topic Topic { get; set; }
        public virtual ICollection<UserSharing> UserSharing { get; set; }
    }
}
