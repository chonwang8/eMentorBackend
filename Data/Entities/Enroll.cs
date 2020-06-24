﻿using System;
using System.Collections.Generic;

namespace Data.Entities
{
    public partial class Enroll
    {
        public Guid EnrollId { get; set; }
        public Guid SubscriptionId { get; set; }
        public Guid SharingId { get; set; }
        public bool IsDisable { get; set; }

        public virtual Sharing Sharing { get; set; }
        public virtual Subscription Subscription { get; set; }
    }
}
