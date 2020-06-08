﻿using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Domain.Services
{
    public class TopicService : ITopicService
    {
        protected readonly IUnitOfWork _uow;

        public TopicService(IUnitOfWork uow)
        {
            _uow = uow;
        }




        public IEnumerable<TopicViewModel> GetAll()
        {
            IEnumerable<TopicViewModel> result = _uow
                .GetRepository<Topic>()
                .GetAll()
                .Select(u => new TopicViewModel
                {
                    TopicId = u.TopicId,
                    TopicName = u.TopicName,
                    MajorId = u.MajorId,
                    CreatedBy = u.CreatedBy
                });

            return result;
        }

        public IEnumerable<TopicViewModel> GetById(string topicId)
        {
            if (topicId == null)
            {
                return null;
            }

            IEnumerable<TopicViewModel> result = _uow
                .GetRepository<Topic>()
                .GetAll()
                .Where(u => u.TopicId.Equals(new Guid(topicId)))
                .Select(u => new TopicViewModel
                {
                    TopicId = u.TopicId,
                    TopicName = u.TopicName,
                    MajorId = u.MajorId,
                    CreatedBy = u.CreatedBy
                });

            return result;
        }

        public int Insert(TopicViewModel topicInput)
        {
            int result = 0;

            if (topicInput == null)
            {
                result = 0;
                return result;
            }


            Topic topicInDb = _uow
                .GetRepository<Topic>()
                .GetAll()
                .SingleOrDefault(u => u.TopicName == topicInput.TopicName);
            if (topicInDb != null)
            {
                result = 1;
                return result;
            }


            Topic topicInsert = new Topic
            {
                TopicId = topicInput.TopicId,
                TopicName = topicInput.TopicName,
                MajorId = topicInput.MajorId,
                CreatedBy = topicInput.CreatedBy
            };

            try
            {
                _uow.GetRepository<Topic>().Insert(topicInsert);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }

        public int Update(TopicViewModel topicInput)
        {
            int result = 0;

            if (topicInput == null)
            {
                result = 0;
                return result;
            }


            Topic topicInDb = _uow
                .GetRepository<Topic>()
                .GetAll()
                .SingleOrDefault(u => u.TopicId == topicInput.TopicId);
            if (topicInDb == null)
            {
                result = 1;
                return result;
            }


            Topic topicUpdate = new Topic
            {
                TopicId = topicInDb.TopicId,
                TopicName = topicInput.TopicName,
                MajorId = topicInput.MajorId,
                CreatedBy = topicInput.CreatedBy
            };

            try
            {
                _uow.GetRepository<Topic>().Update(topicUpdate);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }

            return result;
        }
        public int Delete(string topicId)
        {
            int result = 0;
            Guid guid = new Guid(topicId);

            if (topicId.Equals(null))
            {
                result = 0;
                return result;
            }

            Topic existingTopic = _uow
                .GetRepository<Topic>()
                .GetAll()
                .FirstOrDefault(t => t.TopicId == guid);
            if (existingTopic == null)
            {
                result = 1;
                return result;
            }

            try
            {
                _uow.GetRepository<Topic>().Delete(existingTopic);
                _uow.Commit();
                result = 2;
            }
            catch (Exception e)
            {
                throw e;
            }


            return result;
        }
    }
}