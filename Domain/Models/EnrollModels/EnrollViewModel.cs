using System;

namespace Domain.Models.EnrollModels
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
