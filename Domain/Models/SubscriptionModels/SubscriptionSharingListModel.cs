using Domain.Models.EnrollModels;
using Domain.Models.SharingModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.SubscriptionModels
{
    public class SubscriptionSharingListModel
    {
        public Guid SubscriptionId { get; set; }
        public virtual ICollection<SharingViewModel> Sharings { get; set; }
    }
}
