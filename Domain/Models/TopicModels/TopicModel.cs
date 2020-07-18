using Domain.Models.MajorModels;
using System;

namespace Domain.Models.TopicModels
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
