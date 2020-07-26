﻿using Domain.DTO;
using Domain.Models.TopicModels;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface ITopicService
    {
        public IEnumerable<TopicViewModel> GetAll();
        public IEnumerable<TopicViewModel> GetById(string topicId);
        public int Insert(TopicViewModel topicInput);
        public int Update(TopicViewModel topicInput);


        public int ChangeStatus(string subscriptionId, bool status);
        public int Delete(string topicId);

        public IEnumerable<CountMenteeSubcribeTopicModel> CountMenteeSubcribeTopic();
    }
}
