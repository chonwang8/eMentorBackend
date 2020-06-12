using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface ISubcriptionService
    {
        public IEnumerable<SubscriptionViewModel> GetAll();
        public IEnumerable<SubscriptionViewModel> GetById(string subscriptionId);
        
        
        public int Insert(SubscriptionViewModel subscriptionViewModel);
        public int Update(SubscriptionViewModel subscriptionViewModel);


        public int Disable(string sharingId, bool status);
        public int Activate(string sharingId, bool status);
        public int Delete(string subscriptionId);
    }
}
