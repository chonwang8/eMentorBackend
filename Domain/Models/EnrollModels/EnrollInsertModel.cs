using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.EnrollModels
{
    public class EnrollInsertModel
    {
        public Guid SubscriptionId { get; set; }
        public Guid SharingId { get; set; }
        public bool IsDisable { get; set; }
    }
}
