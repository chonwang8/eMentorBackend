using Domain.DTO;
using Domain.DTO.ResponseDtos;
using Domain.Models.EnrollModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface IEnrollService
    {
        public BaseResponseDto<EnrollViewModel> GetAll();
        public BaseResponseDto<EnrollModel> GetById(string enrollId);
        public BaseResponseDto Insert(EnrollInsertModel enrollInsertModel);
        public BaseResponseDto Update(EnrollUpdateModel enrollUpdateModel);
        public BaseResponseDto ChangeStatus(string enrollId, bool status);
        public BaseResponseDto Delete(string enrollId);
    }
}
