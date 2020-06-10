using Domain.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Services.Interfaces
{
    public interface ISharingService
    {
        public IEnumerable<SharingViewModel> GetAll();
        public IEnumerable<SharingViewModel> GetById(string sharingId);
        public int Insert(SharingViewModel sharingViewModel);
        public int Update(SharingViewModel sharingViewModel);
        public int ChangeStatus(string sharingId, bool status);
        public int Delete(string sharingId);
    }
}
