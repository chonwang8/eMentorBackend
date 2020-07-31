﻿using Domain.DTO.QueryAttributesDtos;
using Domain.DTO.ResponseDtos;
using Domain.Models.SharingModels;

namespace Domain.Services.Interfaces
{
    public interface ISharingService
    {
        public BaseResponseDto<SharingViewModel> GetAll(FilterDto filterRequest);
        public BaseResponseDto<SharingViewModel> GetByName(string sharingName, FilterDto filterRequest);
        public BaseResponseDto<SharingModel> GetById(string sharingId);
        public BaseResponseDto Insert(SharingInsertModel sharingInsertModel);
        public BaseResponseDto Update(SharingUpdateModel sharingModel);
        public BaseResponseDto ChangeStatus(string sharingId, bool status);
        public BaseResponseDto Delete(string sharingId);
    }
}
