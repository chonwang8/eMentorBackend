using Domain.ViewModels.MajorModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.TopicModels
{
    public class TopicModel
    {
        public Guid TopicId { get; set; }
        public string TopicName { get; set; }
        public Guid CreatedBy { get; set; }
        public bool IsDisable { get; set; }

        public virtual MajorViewModel Major { get; set; }
    }
}
