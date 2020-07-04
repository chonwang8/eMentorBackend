using Domain.DTO;
using Domain.ViewModels.SubscriptionModels;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface ISubscriptionService
    {
        public IEnumerable<SubscriptionViewModel> GetAll(GetAllDTO request);
        public IEnumerable<SubscriptionViewModel> GetById(string subscriptionId);


        public int Insert(SubscriptionViewModel subscriptionViewModel);
        public int Update(SubscriptionViewModel subscriptionViewModel);


        public int ChangeStatus(string subscriptionId, bool status);
        public int Delete(string subscriptionId);
    }
}
