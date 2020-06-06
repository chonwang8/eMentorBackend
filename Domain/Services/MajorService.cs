using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class MajorService : IMajorService
    {
        #region Classes and Constructor

        protected readonly IUnitOfWork _uow;

        public MajorService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #endregion Classes and Constructor

        public List<GetTopicGroupByMajorViewModel> GetTopicGroupByMajor()
        {
            List<GetTopicGroupByMajorViewModel> result = new List<GetTopicGroupByMajorViewModel>();
            IEnumerable<Major> majors = _uow.GetRepository<Major>().GetAll().Include(t => t.Topic);
            foreach(var major in majors)
            {
                List<GetTopic> topics = new List<GetTopic>();
                foreach (var topic in major.Topic)
                {
                    topics.Add(new GetTopic
                    {
                        TopicId = topic.TopicId,
                        TopicName = topic.TopicName
                    });
                }
                result.Add(new GetTopicGroupByMajorViewModel
                {
                    MajorId = major.MajorId,
                    MajorName = major.MajorName,
                    Topics = topics
                });
            }
            _uow.Commit();
            return result;
        }
    }
}
