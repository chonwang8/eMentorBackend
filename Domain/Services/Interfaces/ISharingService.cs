using Domain.DTO;
using Domain.ViewModels.SharingModels;
using System.Collections.Generic;

namespace Domain.Services.Interfaces
{
    public interface ISharingService
    {
        public IEnumerable<SharingViewModel> GetAll(GetAllDTO request);
        public IEnumerable<SharingModel> GetById(string sharingId);
        public int Insert(SharingModel sharingViewModel);
        public int Update(SharingModel sharingViewModel);
        public int ChangeStatus(string sharingId, bool status);
        public int Delete(string sharingId);
    }
}
