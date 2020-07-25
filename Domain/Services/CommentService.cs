using Data.Entities;
using Data.UnitOfWork.Interfaces;
using Domain.DTO;
using Domain.Services.Interfaces;
using Domain.Models.CommentModels;
using System;
using System.Collections.Generic;
using System.Linq;

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
        public List<CommentViewModel> GetAllComment()
        {
            List<CommentViewModel> result = new List<CommentViewModel>();
            IEnumerable<Comment> comments = _uow.GetRepository<Comment>().GetAll();
            foreach (var comment in comments)
            {
                result.Add(new CommentViewModel
                {
                    CommentId = comment.CommentId,
                    ParentCommendId = comment.ParentCommendId,
                    CommentContent = comment.CommentContent,
                    SharingId = comment.SharingId
                });
            }
            return result;
        }

        public CommentViewModel GetCommentById(Guid CommentId)
        {
            Comment comment = _uow.GetRepository<Comment>().Get(CommentId);
            if (comment != null)
            {
                CommentViewModel result = new CommentViewModel
                {
                    CommentId = comment.CommentId,
                    ParentCommendId = comment.ParentCommendId,
                    CommentContent = comment.CommentContent,
                    SharingId = comment.SharingId
                };
                return result;
            }
            return null;
        }
    }
}
