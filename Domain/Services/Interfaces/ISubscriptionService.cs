using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface ISubscriptionService
    {
        public IEnumerable<SubscriptionViewModel> GetAll();
        public IEnumerable<SubscriptionViewModel> GetById(string subscriptionId);
        
        
        public int Insert(SubscriptionViewModel subscriptionViewModel);
        public int Update(SubscriptionViewModel subscriptionViewModel);


        public int ChangeStatus(string subscriptionId, bool status);
        public int Delete(string subscriptionId);
    }
}
