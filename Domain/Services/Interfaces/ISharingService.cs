using Domain.DTO;
using Domain.ViewModels.SharingModels;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface ISharingService
    {
        public IEnumerable<SharingViewModel> GetAll(GetAllDTO request);
        public IEnumerable<SharingViewModel> GetById(string sharingId);
        public int Insert(SharingViewModel sharingViewModel);
        public int Update(SharingViewModel sharingViewModel);
        public int ChangeStatus(string sharingId, bool status);
        public int Delete(string sharingId);
    }
}
