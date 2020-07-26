﻿using Domain.DTO;
using Domain.Models.CommentModels;
using System;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface ICommentService
    {
        public List<CommentViewModel> GetAllComment();

        public CommentViewModel GetCommentById(Guid CommentId);
    }
}
