using System;

namespace Domain.ViewModels.SharingModels
{
    public class SharingModel
    {
        public Guid SharingId { get; set; }
        public string SharingName { get; set; }
        public string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Maximum { get; set; }
        public double Price { get; set; }
        public Guid ChannelId { get; set; }
        public string imageUrl { get; set; }
        public string TopicName { get; set; }
    }
}
