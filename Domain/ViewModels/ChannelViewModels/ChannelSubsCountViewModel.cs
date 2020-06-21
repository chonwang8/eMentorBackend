using Data.Entities;
using Domain.ViewModels.SubscriptionViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Domain.ViewModels.ChannelViewModels
{
    public class ChannelSubsCountViewModel
    {
        public ChannelSubsCountViewModel()
        {
            MonthSubsCount = new List<int>()
            {
                0,  //  Index 0, Month 1
                0,  //  Index 0, Month 2
                0,  //  Index 0, Month 3
                0,  //  Index 0, Month 4
                0,  //  Index 0, Month 5
                0,  //  Index 0, Month 6
                0,  //  Index 0, Month 7
                0,  //  Index 0, Month 8
                0,  //  Index 0, Month 9
                0,  //  Index 0, Month 10
                0,  //  Index 0, Month 11
                0   //  Index 0, Month 12
            };
        }

        public Guid ChannelId { get; set; }
        public string MentorName { get; set; }

        public List<int> MonthSubsCount { get; set; }
    }
}
