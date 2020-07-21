using Domain.Models.SharingModels;
using Domain.Models.SubscriptionModels;
using System;

namespace Domain.Models.EnrollModels
{
    public class EnrollViewModel
    {
        public Guid EnrollId { get; set; }
        public Guid SubscriptionId { get; set; }
        public Guid SharingId { get; set; }
        public string SharingName { get; set; }
        public string MenteeName { get; set; }
        public string MentorName { get; set; }
        public bool IsDisable { get; set; }
    }
}
