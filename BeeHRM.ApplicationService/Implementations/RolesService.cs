using BeeHRM.ApplicationService.Interfaces;
using BeeHRM.Repository.Interfaces;
using BeeHRM.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BeeHRM.ApplicationService.DTOs;
using BeeHRM.Repository;
using BeeHRM.ApplicationService.ResponseFormatters;
using BeeHRM.ApplicationService.RequestFormatters;

namespace BeeHRM.ApplicationService.Implementations
{
    public class RolesService : IRolesService
    {
        private readonly IUnitOfWork _unitOfWork;
        
      
        public RolesService(IUnitOfWork unitOfWork = null)
        {
            _unitOfWork = unitOfWork ?? new UnitOfWork();
        }

        public RolesDTO GetRoleById(int roleId)
        {
            return RolesRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.RoleRepository.GetById(roleId));
        }

        public IEnumerable<RolesDTO> GetRoles()
        {
            IEnumerable<Role> modelDatas = _unitOfWork.RoleRepository.All().ToList();
            return RolesResponseFormatter.ModelData(modelDatas);    
        }

        public RolesDTO InsertRoles(RolesDTO data)
        {
            Role dataToInsert = RolesRequestFormatter.ConvertRespondentInfoFromDTO(data);
            return RolesRequestFormatter.ConvertRespondentInfoToDTO(_unitOfWork.RoleRepository.Create(dataToInsert));
        }

        public int UpdateRole(RolesDTO data)
        {
            Role dataToUpdate = RolesRequestFormatter.ConvertRespondentInfoFromDTO(data);
            var response = _unitOfWork.RoleRepository.Update(dataToUpdate);
            return response;
        }
    }
}
