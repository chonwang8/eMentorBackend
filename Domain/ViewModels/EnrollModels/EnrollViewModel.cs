using Domain.ViewModels.SharingModels;
using Domain.ViewModels.SubscriptionModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.EnrollModels
{
    public class EnrollViewModel
    {
        public Guid EnrollId { get; set; }
        public Guid SubscriptionId { get; set; }
        public Guid SharingId { get; set; }
        public string SharingName { get; set; }
        public bool IsDisable { get; set; }
    }
}
