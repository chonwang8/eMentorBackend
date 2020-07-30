using System;

namespace Domain.Models.SharingModels
{
    public class SharingInsertModel
    {
        public Guid ChannelId { get; set; }
        public string SharingName { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Maximum { get; set; }
        public double Price { get; set; }
        public string imageUrl { get; set; }
    }
}
