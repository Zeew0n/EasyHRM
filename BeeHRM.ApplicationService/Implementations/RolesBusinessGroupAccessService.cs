using BeeHRM.ApplicationService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.RequestFormatters;
using BeeHRM.ApplicationService.ResponseFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
    public class RolesBusinessGroupAccessService : IRolesBusinessGroupAccessService
    {
        private readonly IUnitOfWork _unitOfWork;

        public RolesBusinessGroupAccessService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public int[] GetBusinessGroupByRoleID(int roleId)
        {
            var _listOfBusinessGroup = _unitOfWork.RolesBusinessGroupAccessRepository.All();
            List<RolesBusinessGroupAccessDTO> result = RolesBusinessGroupAccessResponseFormatter.ModelData(from resTable in _listOfBusinessGroup where resTable.RoleId == roleId select resTable).ToList();
            int[] res = new int[result.Count];
            int i = 0;
            foreach (var row in result)
            {
                res[i] = row.BusinssGroupId;
                i++;
            }
            return res;
        }

        public RolesBusinessGroupAccessDTO Insert(RolesBusinessGroupAccessDTO dataForBg)
        {
            RolesBusinessGroupAccess dataToInsert = RolesBusinessGroupAccessRequestFormatter.ConvertRespondentInfoFromDTO(dataForBg);
            return RolesBusinessGroupAccessRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.RolesBusinessGroupAccessRepository.Create(dataToInsert));
        }

        public void DeleteExistingData(int roleId)
        {
            _unitOfWork.RolesBusinessGroupAccessRepository.Delete(new { });
        }

        public void RemoveBg(string roleId, string bgID)
        {
            int RoleId = Convert.ToInt16(roleId);
            int BgId = Convert.ToInt16(bgID);
            RolesBusinessGroupAccess dataTodelete = _unitOfWork.RolesBusinessGroupAccessRepository.Get().Where(c => c.RoleId == RoleId && c.BusinssGroupId == BgId).First();
            _unitOfWork.RolesBusinessGroupAccessRepository.Delete(dataTodelete);
            _unitOfWork.Save();
        }
    }
}
