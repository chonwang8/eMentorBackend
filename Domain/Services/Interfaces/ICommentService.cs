using Domain.DTO;
using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface ICommentService
    {
        public List<GetCommentViewModel> GetAllComment(GetAllDTO request);

        public GetCommentViewModel GetCommentById(Guid CommentId);
    }
}
