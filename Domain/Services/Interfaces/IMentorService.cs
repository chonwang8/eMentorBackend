﻿using Domain.DTO.QueryAttributesDtos;
using Domain.DTO.ResponseDtos;
using Domain.Models.MentorModels;

namespace Domain.Services.Interfaces
{
    public interface IMentorService
    {
        public BaseResponseDto<MentorViewModel> GetAll();
        public BaseResponseDto<MentorModel> GetById(string mentorId);
        public BaseResponseDto GoogleLogin(string mentorId);
        public BaseResponseDto Insert(MentorInsertModel mentorInsertModel);
        public BaseResponseDto Update(MentorUpdateModel mentorUpdateModel);
        public BaseResponseDto ChangeStatus(string mentorId, bool status);
        public BaseResponseDto Delete(string mentorId);
    }
}
