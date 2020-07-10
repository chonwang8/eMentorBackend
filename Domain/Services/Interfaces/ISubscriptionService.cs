using Domain.DTO;
using Domain.ViewModels.SubscriptionModels;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface ISubscriptionService
    {
        public IEnumerable<SubscriptionModel> GetAll();
        public IEnumerable<SubscriptionModel> GetById(string subscriptionId);


        public int Insert(SubscriptionModel subscriptionViewModel);
        public int Update(SubscriptionModel subscriptionViewModel);


        public int ChangeStatus(string subscriptionId, bool status);
        public int Delete(string subscriptionId);
    }
}
