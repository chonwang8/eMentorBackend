﻿using Domain.DTO;
using Domain.ViewModels.CommentModels;
using System;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface ICommentService
    {
        public List<GetCommentViewModel> GetAllComment();

        public GetCommentViewModel GetCommentById(Guid CommentId);
    }
}
