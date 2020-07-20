using Domain.DTO;
using Domain.DTO.ResponseDtos;
using Domain.Models.SubscriptionModels;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface ISubscriptionService
    {
        public IEnumerable<SubscriptionViewModel> GetAll();
        public IEnumerable<SubscriptionModel> GetById(string subscriptionId);


        public int Insert(SubscriptionInsertModel subscriptionInsertModel);
        public int Update(SubscriptionUpdateModel subscriptionUpdateModel);


        public int ChangeStatus(string subscriptionId, bool status);
        public int Delete(string subscriptionId);
        public BaseResponseDto Delete(string menteeId, string channelId);
    }
}
