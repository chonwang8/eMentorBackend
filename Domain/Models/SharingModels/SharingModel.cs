using Domain.Models.ChannelModels;
using System;

namespace Domain.Models.SharingModels
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
        public bool IsDisable { get; set; }
        public bool IsApproved { get; set; }
        public DateTime? ApprovedTime { get; set; }

        public ChannelViewModel Channel { get; set; }
    }
}
