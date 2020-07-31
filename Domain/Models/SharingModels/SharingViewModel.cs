﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.SharingModels
{
    public class SharingViewModel
    {
        public Guid SharingId { get; set; }
        public string SharingName { get; set; }
        public double Price { get; set; }
        public string MentorName { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public int Maximum { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string ImageUrl { get; set; }
        public bool IsDisable { get; set; }
        public bool IsApproved { get; set; }
    }
}
