using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.SharingModels
{
    public class SharingViewModel
    {
        public Guid SharingId { get; set; }
        public string SharingName { get; set; }
        public double Price { get; set; }
        public string MentorName { get; set; }
        public string ImageUrl { get; set; }

        public bool IsApproved { get; set; }
    }
}
