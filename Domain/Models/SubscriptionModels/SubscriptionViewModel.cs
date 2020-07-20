using Domain.Models.ChannelModels;
using Domain.Models.MenteeModels;
using System;

namespace Domain.Models.SubscriptionModels
{
    public class SubscriptionViewModel
    {
        public Guid SubscriptionId { get; set; }
        public string MenteeName { get; set; }
        public string ChannelMentor { get; set; }
        public string ChannelTopic { get; set; }
        public bool IsDisable { get; set; }
        public DateTime TimeSubscripted { get; set; }
    }
}
