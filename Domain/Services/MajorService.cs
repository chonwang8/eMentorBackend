﻿using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
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

        public List<GetMajorViewModel> GetAllMajor()
        {
            List<GetMajorViewModel> result = new List<GetMajorViewModel>();
            IEnumerable<Major> majors = _uow.GetRepository<Major>().GetAll();
            foreach (var major in majors)
            {
                result.Add(new GetMajorViewModel
                {
                    MajorId = major.MajorId,
                    MajorName = major.MajorName
                });
            }
            return result;
        }

        public GetMajorViewModel GetMajorById(Guid MajorId)
        {
            Major major = _uow.GetRepository<Major>().Get(MajorId);
            if (major != null)
            {
                GetMajorViewModel result = new GetMajorViewModel
                {
                    MajorId = major.MajorId,
                    MajorName = major.MajorName
                };
                return result;
            }
            return null;
        }

        public List<GetTopicGroupByMajorViewModel> GetTopicGroupByMajor()
        {
            List<GetTopicGroupByMajorViewModel> result = new List<GetTopicGroupByMajorViewModel>();
            IEnumerable<Major> majors = _uow.GetRepository<Major>().GetAll().Include(t => t.Topic);
            foreach (var major in majors)
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