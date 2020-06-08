using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface ITopicService
    {
        public IEnumerable<TopicViewModel> GetAll();
        public IEnumerable<TopicViewModel> GetById(string topicId);
        public int Insert(TopicViewModel topicInput);
        public int Update(TopicViewModel topicInput);
        public int Delete(string topicId);
    }
}
