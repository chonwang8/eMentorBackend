using Domain.Models.SharingModels;
using Domain.Models.SubscriptionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.EnrollModels
{
    public class EnrollModel
    {
        public Guid EnrollId { get; set; }
        public SharingViewModel Sharing { get; set; }
        public SubscriptionViewModel Subscription { get; set; }
        public bool IsDisable { get; set; }
    }
}
