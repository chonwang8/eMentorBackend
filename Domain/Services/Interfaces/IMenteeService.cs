using Domain.DTO;
using Domain.DTO.ResponseDtos;
using Domain.Models.MenteeModels;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface IMenteeService
    {
        public BaseResponseDto<MenteeViewModel> GetAll();
        public BaseResponseDto<MenteeModel> GetById(string menteeId);
        public BaseResponseDto GoogleLogin(string menteeId);
        public BaseResponseDto Insert(MenteeInsertModel menteeInsertModel);
        public BaseResponseDto Update(MenteeUpdateModel menteeUpdateModel);
        public BaseResponseDto ChangeStatus(string menteeId, bool status);
        public BaseResponseDto Delete(string menteeId);


        public BaseResponseDto<MenteeSubbedChannelModel> GetSubbedChannels(string menteeId);
    }
}
