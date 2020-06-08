﻿using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.Services.Interfaces;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services
{
    public class CommentService : ICommentService
    {
        #region Classes and Constructor

        protected readonly IUnitOfWork _uow;

        public CommentService(IUnitOfWork uow)
        {
            _uow = uow;
        }

        #endregion Classes and Constructor
        public List<GetCommentViewModel> GetAllComment()
        {
            List<GetCommentViewModel> result = new List<GetCommentViewModel>();
            IEnumerable<Comment> comments = _uow.GetRepository<Comment>().GetAll();
            foreach (var comment in comments)
            {
                result.Add(new GetCommentViewModel
                {
                    CommentId = comment.CommentId,
                    CommentContent = comment.CommentContent,
                    SharingId = comment.SharingId
                });
            }
            return result;
        }

        public GetCommentViewModel GetCommentById(Guid CommentId)
        {
            Comment comment = _uow.GetRepository<Comment>().Get(CommentId);
            if (comment != null)
            {
                GetCommentViewModel result = new GetCommentViewModel
                {
                    CommentId = comment.CommentId,
                    CommentContent = comment.CommentContent,
                    SharingId = comment.SharingId
                };
                return result;
            }
            return null;
        }
    }
}