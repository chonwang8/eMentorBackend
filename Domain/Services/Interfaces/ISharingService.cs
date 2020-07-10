using Domain.DTO.QueryAttributesDtos;
using Domain.DTO.ResponseDtos;
using Domain.ViewModels.SharingModels;

namespace Domain.Services.Interfaces
{
    public interface ISharingService
    {
        public BaseResponseDto<SharingViewModel> GetAll(PagingDto pagingRequest, FilterDto filterRequest);
        public BaseResponseDto<SharingModel> GetById(string sharingId);
        public BaseResponseDto Insert(SharingInsertModel sharingInsertModel);
        public BaseResponseDto Update(SharingModel sharingModel);
        public BaseResponseDto ChangeStatus(string sharingId, bool status);
        public BaseResponseDto Delete(string sharingId);
    }
}
