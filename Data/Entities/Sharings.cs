using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Sharings
    {
        public Sharings()
        {
            UserSharings = new HashSet<UserSharings>();
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

        public virtual Topics Topic { get; set; }
        public virtual ICollection<UserSharings> UserSharings { get; set; }
    }
}
