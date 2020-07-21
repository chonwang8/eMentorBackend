using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.EnrollModels
{
    public class EnrollUpdateModel
    {
        public Guid EnrollId { get; set; }
        public Guid SubscriptionId { get; set; }
        public Guid SharingId { get; set; }
        public bool IsDisable { get; set; }
    }
}
