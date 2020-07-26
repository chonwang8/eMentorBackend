using Domain.Models.EnrollModels;
using Domain.Models.SharingModels;
using Domain.Models.SubscriptionModels;
using Domain.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.MenteeModels
{
    public class MenteeEnrolledSharingModel
    {
        public Guid MenteeId { get; set; }
        public UserViewModel User { get; set; }
        public ICollection<SubscriptionSharingListModel> Subscriptions { get; set; }
    }
}
