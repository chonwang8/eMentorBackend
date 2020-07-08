using Data.Entities;
using Domain.DTO;
using Domain.DTO.QueryAttributesDtos;
using Domain.DTO.ResponseDtos;
using Domain.ViewModels.SharingModels;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface ISharingService
    {
        public SharingResponseDto<SharingViewModel> GetAll(PagingDto pagingRequest, FilterDto filterRequest);
        public SharingResponseDto<SharingModel> GetById(string sharingId);
        public SharingResponseDto Insert(SharingInsertModel sharingInsertModel);
        public SharingResponseDto Update(SharingModel sharingModel);
        public SharingResponseDto ChangeStatus(string sharingId, bool status);
        public SharingResponseDto Delete(string sharingId);
    }
}
