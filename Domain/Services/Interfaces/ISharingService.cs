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
        public IEnumerable<SharingModel> GetById(string sharingId);
        public SharingResponseDto Insert(SharingInsertModel sharingInsertModel);
        public int Update(SharingModel sharingModel);
        public int ChangeStatus(string sharingId, bool status);
        public int Delete(string sharingId);
    }
}
